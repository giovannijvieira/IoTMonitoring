using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace IoTMonitoring.Filters
{
    public class AddTagsDescriptionFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = new List<OpenApiTag>
            {
                new OpenApiTag { Name = "Auth", Description = "Gerenciar a autenticação na plataforma." },
                 new OpenApiTag { Name = "Devices", Description = "Provêr operações para cadastramento e gerenciamento de dispositivos" }
            };
        }
    }
}
