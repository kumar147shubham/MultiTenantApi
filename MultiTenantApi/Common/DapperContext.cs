using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MultiTenantApi.Common
{
    public class DapperContext
    {
        private readonly string _connectionString;
        
        public DapperContext()
        {
            var getUrlAddress = HttpContext.Current.Request.Headers["Host"].Split('.');
            var tenant = getUrlAddress[0].ToLower();
            if (tenant.Contains("localhost"))
                _connectionString = ConfigurationManager.ConnectionStrings["TestDb"].ToString(); //for localhost IISExpress  
            else
                _connectionString = ConfigurationManager.ConnectionStrings[tenant].ToString(); ;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}