using MultiTenantApi.Models;
using MultiTenantApi.Models.Dto.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantApi.Interfaces
{
    public interface IProductService
    {
       Task<IEnumerable<Product>>GetAllProducts();
        Task<IEnumerable<Product>> InsertProduct(ProductRegisterDto productRegisterDto);
    }
}
