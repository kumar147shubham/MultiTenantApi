using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MultiTenantApi.Models.Dto
{
    public class UserRegisterDto
    {
        [Required] public string UserName { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string Gender { get; set; }
        public string UserAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
    }
}