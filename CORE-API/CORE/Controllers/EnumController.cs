using AutoMapper;
using CORE_API.CORE.Contexts;
using CORE_API.CORE.Helpers.Attributes;
using CORE_API.CORE.Models.Configuration;
using CORE_API.CORE.Models.Entities;
using CORE_API.CORE.Models.Views;
using CORE_API.CORE.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_API.CORE
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnumController : ControllerBase
    {
        private readonly IMapper _mapper;

        public EnumController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerSummary("List all enums and their values")]
        public IEnumerable<EnumResource> ListEnumsAsync()
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies().First(a => a.FullName.StartsWith("CORE-API"));

            //var types = assembly.GetTypes().Where(t => String.Equals(t.Namespace, "Domain.Models.Enums", StringComparison.Ordinal)).ToList();

            var types = assembly.GetTypes().Where(t => t.IsEnum);

            var result = new List<EnumResource>();
            foreach (Type enumType in types)
            {

                //var etype = typeof(EActivityType);
                var name = enumType.Name;

                result.Add(new EnumResource()
                {
                    EnumName = name,
                    EnumValues = GetValues(enumType)
                });
            }

            return result;
        }


        private List<string> GetValues(Type enumType)
        {
            List<string> values = new List<string>();
            foreach (var itemType in enumType.GetEnumValues())
            {
                //For each value of this enumeration, add a new EnumValue instance
                values.Add(itemType.ToString());
            }
            return values;
        }
    }

}
