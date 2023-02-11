using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MultiTenantApi.Models.Dto.ProductDto
{
    public class ProductRegisterDto
    {
        [Required] public string DatabaseName { get; set; }
        [Required] public string ProductName { get; set; }
        [Required] public decimal Price { get; set; }
        [Required] public string ProductDetails { get; set; }
    }
}