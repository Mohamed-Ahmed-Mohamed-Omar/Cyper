using System.ComponentModel.DataAnnotations;

namespace Cyper.Models.Identity
{
    public class RegisterModel
    {
        public string FullName { get; set; }

        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
