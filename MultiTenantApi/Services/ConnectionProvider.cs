using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;

namespace MultiTenantApi.Services
{
    public interface IConnectionProvider
    {
        string ReadResourceValue(string val);
    }
    public class ConnectionProvider : IConnectionProvider
    {
        public string ReadResourceValue(string key)
        {

            string resourceValue = string.Empty;
            try
            {
                ResourceManager resourceManager = new ResourceManager("MultiTenantApi.Resources.Resources", Assembly.GetExecutingAssembly());
                // retrieve the value of the specified key
                resourceValue = resourceManager.GetString(key);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                resourceValue = string.Empty;
            }
            return resourceValue;
        }
    }

}