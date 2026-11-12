
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartAirPort.Models.ViewModels;
using SmartAirPort.Data;
using SmartAirPort.Models;
using SmartAirPort.Security;
using System.Security.Claims;

namespace SmartAirPort.Controllers
{
    public class AccountController : Controller
    {
        private readonly AirPortDBContext _context;

        public AccountController(AirPortDBContext context)
        {
            _context = context;
        }


            [AllowAnonymous]
            [HttpGet]
            public IActionResult Login(string? returnUrl = null)
            {
                if (User.Identity?.IsAuthenticated == true)
                {
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

            [AllowAnonymous]
            [HttpPost]
            public IActionResult Login(LoginViewModel model, string? returnUrl = null)
            {

                if (!ModelState.IsValid)
                {
                    ViewBag.ReturnUrl = returnUrl;
                    return View(model);

                }

                User? user = _context.Users.Include(u => u.Roles)
                                             .ThenInclude(r => r.Permissions)
                                             .FirstOrDefault(u => u.Username == model.Username);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    ViewBag.ReturnUrl = returnUrl;
                    return View(model);
                }
                if (user.Password != model.Password)
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    ViewBag.ReturnUrl = returnUrl;
                    return View(model);
                }
                List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserEmail", user.Username)
            };

                foreach (Role role in user.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                }
                List<String> permissions = user.Roles.SelectMany(r => r.Permissions)
                                                     .Select(permission => permission.Name)
                                                     .Distinct().ToList();
                foreach (string permission in permissions)
                {
                    claims.Add(new Claim(PermissionsNames.ClaimType, permission));
                }
                ClaimsIdentity identity = new ClaimsIdentity(
                                            claims, CookieAuthenticationDefaults.AuthenticationScheme,
                                            ClaimTypes.Name,
                                            ClaimTypes.Role);

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                AuthenticationProperties properties = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);

                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");

            }

            [Authorize]
            [HttpPost]
            public IActionResult Logout()
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login");
            }

            [AllowAnonymous]
            [HttpGet]
            public IActionResult AccessDenied()
            {
                return View();
            }

            [Authorize]
            [HttpGet]
            public IActionResult MyAccess()
            {
                return View();
            }


        }
    }

