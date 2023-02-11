using MultiTenantApi.Models;
using MultiTenantApi.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantApi.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<AppUser> Register(UserRegisterDto userRegisterDto);
        Task<AppUser> Update(AppUser appUser);
        Task<bool> Delete(string userId);
        Task<AppUser> GetUserByName(string userName);
        Task<bool> IsUserExist(string userName);
        Task<UserDto> userLogin(string username, string password);
    }
}
