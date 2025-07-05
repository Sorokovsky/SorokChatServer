using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SorokChatServer.Infrastructure.Filters;

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var fileParams = context.MethodInfo.GetParameters()
            .Where(p => p.ParameterType == typeof(IFormFile))
            .ToDictionary(p => p.Name, _ => new OpenApiSchema
            {
                Type = "string",
                Format = "binary"
            });

        if (!fileParams.Any()) return;

        operation.RequestBody = new OpenApiRequestBody
        {
            Content =
            {
                ["multipart/form-data"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type = "object",
                        Properties = fileParams,
                        Required = new HashSet<string>(fileParams.Keys)
                    }
                }
            }
        };
    }
}