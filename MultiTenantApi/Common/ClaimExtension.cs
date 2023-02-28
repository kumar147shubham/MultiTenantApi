using MultiTenantApi.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace MultiTenantApi.Common
{
    public class ClaimExtension
    {
        public static ClaimedUser IsValidUser(ClaimsIdentity identity)
        {
            if (identity != null)
            {
                ClaimedUser claimedUser = new ClaimedUser();
                IEnumerable<Claim> claims = identity.Claims;
                string name = claims.Where(p => p.Type == "username").FirstOrDefault()?.Value;
                string tenant = claims.Where(p => p.Type == "tenant").FirstOrDefault()?.Value;
                if (name != null && tenant != null)
                {

                    claimedUser.UserName = name;
                    claimedUser.TenantName = tenant;
                    DbConnection.Tenant = tenant;
                }
                else
                    return null;
            }
            return null;

        }
    }
}