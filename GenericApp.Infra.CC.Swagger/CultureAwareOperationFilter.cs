using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace GenericApp.Infra.CC.Swagger
{
    public class CultureAwareOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {


            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            var acceptedLanguages = new Dictionary<string, OpenApiExample>
                {
                    {
                        "English",
                        new OpenApiExample
                        {
                            Value = new OpenApiString("en-US"),
                            Description = "English"
                        }
                    },
                    {
                        "Portuguese",
                        new OpenApiExample
                        {
                            Value = new OpenApiString("pt-BR"),
                            Description = "Português"
                        }
                    },
                    {
                        "Spanish",
                        new OpenApiExample
                        {
                            Value = new OpenApiString("es"),
                            Description = "Espanhol"
                        }
                    }
                };

            var existingParam = operation.Parameters.FirstOrDefault(p =>
            p.In == ParameterLocation.Header && p.Name == "Accept-Language");
            if (existingParam != null) // remove description from [FromHeader] argument attribute
            {
                operation.Parameters.Remove(existingParam);
            }

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Accept-Language",
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema
                {
                    Type = "String",
                    Default = new OpenApiString("en-US")
                },
                Required = false,
                Examples = acceptedLanguages
            });

        }
    }
}
