using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CORE_API.CORE.Helpers.Attributes
{
    public class SwaggerSummary : Attribute
    {
        public SwaggerSummary(string summary)
        {
            Summary = summary;
        }

        public string Summary { get; set; }
    }

    public class ApplySwaggerSummaryAttribute : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.TryGetMethodInfo(out MethodInfo methodInfo))
            {
                var swaggerSummaryAttribute = methodInfo.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(SwaggerSummary));

                if (swaggerSummaryAttribute != null)
                {
                    var swaggerSummary = new SwaggerSummary(swaggerSummaryAttribute.ConstructorArguments[0].Value.ToString());
                    operation.Summary = swaggerSummary.Summary;
                }
            };
        }
    }
}
