using Dapper;
using MultiTenantApi.Common;
using MultiTenantApi.Interfaces;
using MultiTenantApi.Models;
using MultiTenantApi.Models.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiTenantApi.Services
{
    public class UserService : IUserService
    {
        private readonly DapperContext _context;
        public UserService(DapperContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete(string userId)
        {
            int query  = await _context.CreateConnection().ExecuteAsync("delete from Users where userid = " + userId);
            return query > 0;
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _context.CreateConnection().QueryAsync<AppUser>("select * from Users");
        }

        public async Task<AppUser> GetUserByName(string userName)
        {
            return await _context.CreateConnection().QueryFirstOrDefaultAsync<AppUser>("select * from Users where username = " + userName);
        }

        public async Task<bool> IsUserExist(string userName)
        {
            string isExist = await _context.CreateConnection().ExecuteScalarAsync<string>("select top 1 username from Users where username = @UserName", new { UserName = userName });
            return String.Equals(isExist, userName, StringComparison.OrdinalIgnoreCase);
        }

        public async Task<AppUser> Register(UserRegisterDto userRegisterDto)
        {
            int returnId;
            try
            { 
                string sql = @"insert into Users (UserName,Password,Gender,UserAddress, City,Country,PhoneNumber) 
                                values (@UserName,@Password,@Gender,@UserAddress, @City,@Country,@PhoneNumber); 
                               SELECT CAST(SCOPE_IDENTITY() as int)";
                 returnId = await _context.CreateConnection().ExecuteScalarAsync<int>(sql, userRegisterDto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await _context.CreateConnection()
                .QueryFirstOrDefaultAsync<AppUser>("select * from Users where userid = " + returnId);
        }

        public Task<AppUser> Update(AppUser appUser)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> userLogin(string userName, string password)
        {
            var query = "select * from Users where username = @UserName and password = @Password";
            var user = await _context.CreateConnection().QueryFirstOrDefaultAsync<AppUser>(query, new { UserName = userName, Password = password });
            if (user == null) return null;
            return new UserDto
            {
                UserId = user.UserId,
                Token = TokenService.CreateToken(user)
            };
        }
    }
}