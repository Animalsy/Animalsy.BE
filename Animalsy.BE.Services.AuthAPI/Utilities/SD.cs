namespace Animalsy.BE.Services.AuthAPI.Utilities
{
    public static class SD
    {
        public const string RoleAdmin = "Admin";
        public const string RoleCustomer = "Customer";
        public const string RoleVendor = "Vendor";
        public const string RoleAdminAndVendor = $"{RoleAdmin},{RoleVendor}";
    }
}
