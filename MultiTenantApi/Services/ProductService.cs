using Dapper;
using MultiTenantApi.Common;
using MultiTenantApi.Interfaces;
using MultiTenantApi.Models;
using MultiTenantApi.Models.Dto.ProductDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace MultiTenantApi.Services
{
    public class ProductService : IProductService
    {
        private readonly DapperContext _context;
        public ProductService(DapperContext context)
        {
            _context = context;
           

        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var query = "select * from product";
            var list = await _context.CreateConnection().QueryAsync<Product>(query);
            return list;
        }  
        
        public async Task<IEnumerable<Product>> InsertProduct(ProductRegisterDto productRegisterDto)
        {
            
            int returnId;
            try
            {
                string sql = @"insert into Product (DatabaseName, ProductName, Price, ProductDetails) values (@DatabaseName, @ProductName, @Price, @ProductDetails)
                                SELECT CAST(SCOPE_IDENTITY() as int)";
                returnId = await _context.CreateConnection().ExecuteScalarAsync<int>(sql, productRegisterDto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await GetAllProducts();// await _context.CreateConnection().QueryAsync<Product>("select * from Users");
        }
    }
}