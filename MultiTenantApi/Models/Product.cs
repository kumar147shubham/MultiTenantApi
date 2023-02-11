using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiTenantApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string DatabaseName { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string ProductDetails { get; set; }
    }
}