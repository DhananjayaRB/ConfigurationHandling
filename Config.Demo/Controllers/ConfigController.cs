using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Config.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigController : ControllerBase
    {
        private readonly string version;
        private readonly ILogger<ConfigController> _logger;
        public readonly IOptions<Custom> Custom;
        public ConfigController(ILogger<ConfigController> logger,string version,IOptions<Custom> custom)
        {
            _logger = logger;
            this.version = version;
            this.Custom = custom;
        }

        //geting the Configuration Values From default app settings file.
        [HttpGet]
        public string Get()
        {
            return " Version : "+Custom.Value.Version + "\n Database : "+Custom.Value.DataBase +"\n Connection String : "+Custom.Value.MySql_ConcetionString;
        }
    }
}
