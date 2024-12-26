namespace Swizzle.Models.Account
{
    // user can login with either phone or email
    // if using phone, user must have entered otp to continue.
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string ErrorMessage { get; set; }
    }
}
