using MultiTenantApi.Interfaces;
using MultiTenantApi.Models.Dto;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MultiTenantApi.Controllers
{
    [Authorize]
    public class AccountController : ApiController
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Login(LoginDto loginDto)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest);
            if (string.IsNullOrEmpty(loginDto.Username) || string.IsNullOrEmpty(loginDto.Password))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User Name or Passwor should not be empty");

            var result = await _userService.userLogin(loginDto.Username, loginDto.Password);
            if(result == null) return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid username or password");
            return Request.CreateResponse(HttpStatusCode.OK, result); 
        }

        [HttpPost]
        public async Task<HttpResponseMessage> register(UserRegisterDto registerDto)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest);
            if (await UserExists(registerDto.UserName)) 
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Username already taken.");
            
            try
            {
                var obj = await _userService.Register(registerDto);
                response = Request.CreateResponse(HttpStatusCode.OK, obj);
                return response;
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Error while Register: " + ex.Message);
            }
            return response;
        }

        private async Task<bool> UserExists(string userName)
        {
            return  await _userService.IsUserExist(userName);
        }
    }
}
