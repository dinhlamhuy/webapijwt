using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.IdentityModel.Tokens;

namespace webapijwt.Auth
{
    public static class JwtManager
    {
        private static string Secret = ConfigurationManager.AppSettings["JwtSecretKey"];
        private static string issuer = ConfigurationManager.AppSettings["JwtIssuer"];
        private static string audience = ConfigurationManager.AppSettings["JwtAudience"];

        private static JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

        public static string GenerateToken(string username, string role)
        {
            byte[] key = Convert.FromBase64String(Secret);
            var descriptor = GenerateTokenDescriptor(username, key, role);


            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }



        private static SecurityTokenDescriptor GenerateTokenDescriptor(string username, byte[] key, string role)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),//<-- User role!
                //new Claim(ClaimTypes.Role, "Role2")
                }),//<-- User role!
                Expires = DateTime.UtcNow.AddMinutes(30),//Token takes only UTC time
                SigningCredentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256Signature)
            };

            return descriptor;
        }


        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                    return null;
                byte[] key = Convert.FromBase64String(Secret);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                      parameters, out securityToken);
                return principal;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
