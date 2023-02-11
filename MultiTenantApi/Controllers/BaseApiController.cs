using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MultiTenantApi.Controllers
{
    [RoutePrefix("api/[Controller]")]
    public class BaseApiController : ApiController
    {
    }
}
