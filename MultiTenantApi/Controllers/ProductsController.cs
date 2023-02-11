using MultiTenantApi.Interfaces;
using MultiTenantApi.Models;
using MultiTenantApi.Models.Dto.ProductDto;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MultiTenantApi.Controllers
{
    [Authorize]
    public class ProductsController : ApiController
    {
        // GET: api/Products

        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productService.GetAllProducts();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<HttpResponseMessage> InsertProduct(ProductRegisterDto productRegisterDto)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest);
            try
            {
                var obj = await _productService.InsertProduct(productRegisterDto);
                response = Request.CreateResponse(HttpStatusCode.OK, obj);
                return response;
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.ExpectationFailed, "Error while Register: " + ex.Message);
            }
            return response;
        }
    }
}
