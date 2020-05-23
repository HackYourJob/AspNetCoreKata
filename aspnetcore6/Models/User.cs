using System.Collections.Generic;
using System.Security.Claims;

namespace aspnetcore6.Models
{
    public class User : ClaimsIdentity
    {
        public const string DefaultAuthenticationType = "Cookie";

        public User(int id, string name, params Permissions[] permissions)
        : base(GenerateClaims(id, name, permissions), DefaultAuthenticationType)
        {
            Id = id;
        }

        private static IEnumerable<Claim> GenerateClaims(int id, string name, params Permissions[] permissions)
        {
            yield return new Claim(ClaimTypes.NameIdentifier, id.ToString());
            yield return new Claim(ClaimTypes.Name, name);
            yield return new Claim(ClaimTypes.Role, "Member");

            foreach (var permission in permissions)
            {
                yield return new Claim(nameof(Permissions), permission.ToString());
            }
        }

        public int Id { get; }
    }

    public enum Permissions
    {
        CanViewPublicPage,
        CanViewPrivatePage
    }
}
