namespace Cyper.Models.Identity
{
    public class ResetPassword
    {
        public string Password { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
