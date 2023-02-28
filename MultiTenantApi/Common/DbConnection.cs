using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiTenantApi.Common
{
    public class DbConnection
    {
        public static string ConnectionName { get; set; }
        public static string Tenant { get; set; }
    }
}