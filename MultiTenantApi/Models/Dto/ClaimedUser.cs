using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiTenantApi.Models.Dto
{
    public class ClaimedUser
    {
        public string UserName { get; set; }
        public string TenantName { get; set; }
    }
}