using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiTenantApi.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string CustomerName { get; set; }
        public bool IsActive { get; set; }
    }
}