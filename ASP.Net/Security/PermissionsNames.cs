namespace SmartAirPort.Security
{
    public class PermissionsNames
    {
        public const string ClaimType = "Permission";

        // Employees
        public const string EmployeeManagement = "Employees.Management";

        // Departments
        public const string DepartmentManagement = "Departments.Management";

        // Roles
        public const string RoleManagement = "Roles.Management";

        // Permissions
        public const string PermissionManagement = "Permissions.Management";

        public static readonly string[] All =
        {
            EmployeeManagement,
            DepartmentManagement,
            RoleManagement,
            PermissionManagement
        };
    }
}
