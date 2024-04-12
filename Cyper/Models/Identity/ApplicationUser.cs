using EntityFrameworkCore.EncryptColumn.Attribute;
using Microsoft.AspNetCore.Identity;

namespace Cyper.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public List<RefreshToken>? RefreshTokens { get; set; }

        [EncryptColumn]
        public string? Code { get; set; }
    }
}
