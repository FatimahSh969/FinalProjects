
import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'HomeScreen.dart';
import 'SignUpScreen.dart';

class Loginscreeen1 extends StatefulWidget {
  const Loginscreeen1({super.key});

  @override
  State<Loginscreeen1> createState() => _LoginscreeenState();
}

class _LoginscreeenState extends State<Loginscreeen1> {

  final TextEditingController usernamecontroller =
  TextEditingController();

  final TextEditingController passwordcontroller =
  TextEditingController();

  bool isPasswordVisible = false;

  static const greenColor = Color(0xFF52796F);
  static const darkGreen = Color(0xFF344E41);
  static const backgroundColor = Color(0xFFF7F6F2);

  // Load users from JSON file
  Future<List<dynamic>> loadUsers() async {

    String jsonString =
    await rootBundle.loadString('assets/data/users.json');

    return jsonDecode(jsonString);
  }

  // Login function
  void login() async {

    String username = usernamecontroller.text.trim();
    String password = passwordcontroller.text;

    if (username.isEmpty || password.isEmpty) {

      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text('Please enter username and password'),
        ),
      );

      return;
    }

    try {

      final users = await loadUsers();

      bool userfound = users.any(
            (user) =>
        user['username'] == username &&
            user['password'] == password,
      );

      if (!mounted) return;

      if (userfound) {

        Navigator.pushReplacement(
          context,
          MaterialPageRoute(
            builder: (context) => const Homescreen(),
          ),
        );

      } else {

        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: const Text('Invalid Username or Password'),
            backgroundColor: Colors.redAccent,
            behavior: SnackBarBehavior.floating,
            shape: RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(12),
            ),
          ),
        );

      }

    } catch (e) {

      if (!mounted) return;

      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text('Unable to load users data'),
        ),
      );

    }
  }

  @override
  void dispose() {
    usernamecontroller.dispose();
    passwordcontroller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {

    return Scaffold(
      backgroundColor: backgroundColor,

      appBar: AppBar(
        title: const Text('Login'),
        centerTitle: true,
        backgroundColor: backgroundColor,
        foregroundColor: darkGreen,
        elevation: 0,
      ),

      body: SingleChildScrollView(
        padding: const EdgeInsets.all(24),

        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [

            const SizedBox(height: 50),

            const Icon(
              Icons.shopping_bag_outlined,
              size: 85,
              color: greenColor,
            ),

            const SizedBox(height: 15),

            const Text(
              'Welcome Back!',
              style: TextStyle(
                fontSize: 26,
                fontWeight: FontWeight.bold,
                color: darkGreen,
              ),
            ),

            const SizedBox(height: 8),

            const Text(
              'Login to explore our filters.',
              style: TextStyle(
                color: Colors.grey,
                fontSize: 14,
              ),
            ),

            const SizedBox(height: 35),

            // Username
            TextField(
              controller: usernamecontroller,
              decoration: InputDecoration(
                labelText: 'Username',
                prefixIcon: const Icon(
                  Icons.person_outline,
                  color: greenColor,
                ),
                filled: true,
                fillColor: Colors.white,
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
                focusedBorder: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(12),
                  borderSide: const BorderSide(
                    color: greenColor,
                  ),
                ),
              ),
            ),

            const SizedBox(height: 18),

            // Password
            TextField(
              controller: passwordcontroller,
              obscureText: !isPasswordVisible,
              decoration: InputDecoration(
                labelText: 'Password',
                prefixIcon: const Icon(
                  Icons.lock_outline,
                  color: greenColor,
                ),
                filled: true,
                fillColor: Colors.white,
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
                focusedBorder: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(12),
                  borderSide: const BorderSide(
                    color: greenColor,
                  ),
                ),

                suffixIcon: IconButton(
                  icon: Icon(
                    isPasswordVisible
                        ? Icons.visibility
                        : Icons.visibility_off,
                    color: greenColor,
                  ),
                  onPressed: () {
                    setState(() {
                      isPasswordVisible = !isPasswordVisible;
                    });
                  },
                ),
              ),
            ),

            const SizedBox(height: 28),

            // Login Button
            SizedBox(
              width: double.infinity,
              height: 50,

              child: ElevatedButton(
                onPressed: login,

                style: ElevatedButton.styleFrom(
                  backgroundColor: greenColor,
                  foregroundColor: Colors.white,
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(12),
                  ),
                ),

                child: const Text(
                  'Login',
                  style: TextStyle(
                    fontSize: 17,
                    fontWeight: FontWeight.bold,
                  ),
                ),
              ),
            ),

            const SizedBox(height: 18),

            // Sign Up
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [

                const Text("Don't have an account?"),

                TextButton(
                  onPressed: () {
                    Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) =>
                        const SignUpScreen(),
                      ),
                    );
                  },

                  child: const Text(
                    'Sign Up',
                    style: TextStyle(
                      color: greenColor,
                      fontWeight: FontWeight.w600,
                    ),
                  ),
                ),

              ],
            ),

            // Forget Password
            TextButton(
              onPressed: () {
                ScaffoldMessenger.of(context).showSnackBar(
                  const SnackBar(
                    content: Text(
                      'Forget Password page coming next',
                    ),
                  ),
                );
              },

              child: const Text(
                'Forget Password?',
                style: TextStyle(
                  color: greenColor,
                ),
              ),
            ),

          ],
        ),
      ),
    );
  }
}
