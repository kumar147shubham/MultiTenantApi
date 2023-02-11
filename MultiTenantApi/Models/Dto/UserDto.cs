using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiTenantApi.Models.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}