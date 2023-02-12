using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Web;
using Microsoft.IdentityModel.Tokens;
using MultiTenantApi.Interfaces;
using MultiTenantApi.Models;

namespace MultiTenantApi.Services
{
    public static class TokenService
    {
        static string[] getUrlAddress = HttpContext.Current.Request.Headers["Host"].Split('.');
        static string tenant = getUrlAddress[0].ToLower().Contains("localhost") ? "localhost" : getUrlAddress[0].ToLower();

        public static readonly byte[] _key = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings.Get("TokenKey").ToString());
        public static string CreateToken(AppUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("tenant", tenant)
            };
            var creds = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static IPrincipal ValidateToken(string token)
        {
            AppUser user = new AppUser();

            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, new TokenValidationParameters()
            {
                ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },
                ValidateAudience = false, 
                ValidateIssuer = false,  // if true then need to give issuer  as  =>  ValidIssuers = new List<string>{ "host1", "host2" };
                IssuerSigningKey = new SymmetricSecurityKey(_key),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true
            }, out var securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken != null)
            {
                IEnumerable<Claim> claims = jwtSecurityToken.Claims;

                foreach (Claim claim in claims)
                {
                    if(claim.Type == "tenant" && claim.Value != tenant)
                        return new ClaimsPrincipal(new ClaimsIdentity());
                }
            }


            var identity = new ClaimsIdentity(jwtSecurityToken.Claims.ToString(), "Name", "tenant");

            return new ClaimsPrincipal(identity);
        }

        public static void AuthenticateRequst()
        {
            try
            {
                var token = HttpContext.Current.Request.Headers.Get("Authorization");
                if (token != null)
                {
                    IPrincipal principle = ValidateToken(token);
                    HttpContext.Current.User = principle;
                }
            }
            catch {}
            
        }
    }
}