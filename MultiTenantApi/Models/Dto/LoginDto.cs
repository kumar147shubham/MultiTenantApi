﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiTenantApi.Models.Dto
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}