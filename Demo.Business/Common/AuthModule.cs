using Demo.Common.Constants;
using Demo.Common.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Demo.Business.Common
{
    public class AuthModule
    {
        private static readonly string communicationKey = "GQDstc21ewfffffffffffFiwDffVvVBrk";

        public static string GenerateToken(AuthUserOutputModel user)
        {
            try
            {
                var tokenExpiryTime = 20;
                var now = DateTime.UtcNow;

                var securityTokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new List<Claim>()
                    {                        
                        new Claim(ClaimType.Id, user.Id.ToString()),
                       
                    }),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(communicationKey)),
                    SecurityAlgorithms.HmacSha256Signature),
                    Expires = now.AddMinutes(tokenExpiryTime)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var plainToken = tokenHandler.CreateToken(securityTokenDescriptor);
                return tokenHandler.WriteToken(plainToken);
            }
            catch (Exception ex)
            {
                //throw ex;
                return ex.Message;
            }

        }
    }
}