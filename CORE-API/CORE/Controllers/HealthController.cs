using CORE_API.CORE.Contexts;
using CORE_API.CORE.Exceptions;
using CORE_API.CORE.Helpers.Attributes;
using CORE_API.CORE.Models.Configuration;
using CORE_API.CORE.Models.Entities;
using CORE_API.CORE.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CORE_API.CORE
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        public readonly CoreContext _context;
        private readonly IConfiguration _config;

        public HealthController(CoreContext context, IConfiguration config )
        {
            _context = context;
            _config = config;
        }

        [HttpGet]
        public IActionResult HealthCheck()
        {

            var timestamp = DateTime.UtcNow;

            var name = _config.GetSection("Application")["Name"];
            var version = _config.GetSection("Application")["Version"];
            var commit = _config.GetSection("Application")["Commit"];
            var branch = _config.GetSection("Application")["Branch"];
            var pipeline = _config.GetSection("Application")["PipelineNumber"];

            var result = new Dictionary<String, String>() { 
                { "Timestamp", timestamp.ToString("u") }, 
                { "Name", name }, 
                { "Version", version }, 
                { "Commit", commit },
                { "Branch", branch },
                { "Build Number", pipeline} 
            };

            return Ok(result);
        }
    }
}
