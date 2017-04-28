using System;
using Microsoft.IdentityModel.Tokens;

namespace blogAPI.Options
{
    public class JwtIssuerOptions
    {
        public DateTime IssuedAt => DateTime.Now;
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(60);
        public DateTime Expiration => IssuedAt.Add(ValidFor);
        public SigningCredentials SigningCredentials { get; set; }
    }
}
