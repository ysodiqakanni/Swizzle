namespace Swizzle.Models.Account
{
    public class SignupViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string ErrorMessage { get; set; }
    }
}
