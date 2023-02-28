using MultiTenantApi.Services;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MultiTenantApi.Common
{
    public class DapperContext
    {
        private string _connectionString;
        private readonly IConnectionProvider _connectionProvider;
        private readonly IEncryptDecrypt _encryptDecrypt;
        string[] getUrlAddress = HttpContext.Current.Request.Headers["Host"].Split('.');
      
        public DapperContext(IConnectionProvider connectionProvider, IEncryptDecrypt encryptDecrypt)
        {
            _connectionProvider = connectionProvider;
            _encryptDecrypt = encryptDecrypt;

            //if (tenant.Contains("localhost"))
            //    _connectionString = ConfigurationManager.ConnectionStrings["TestDb"].ToString(); //for localhost IISExpress  
            //else
            //    _connectionString = ConfigurationManager.ConnectionStrings[tenant].ToString();

        }

        public string GetEncryptedValues()
        {
            string tenant = getUrlAddress[0].ToLower().Contains("localhost") ? "localhost" : getUrlAddress[0].ToLower();
            
            if (DbConnection.Tenant != null)  // received from User.Identity
                tenant = DbConnection.Tenant;
            var encryptedValue = _connectionProvider.ReadResourceValue(tenant);
            using (Aes aes = Aes.Create())
            {
              return  _connectionString = _encryptDecrypt.DecryptString(encryptedValue);
            }
        }

        public IDbConnection CreateConnection()
        {
            _connectionString = GetEncryptedValues();
            return new SqlConnection(_connectionString);
        }
    }
}