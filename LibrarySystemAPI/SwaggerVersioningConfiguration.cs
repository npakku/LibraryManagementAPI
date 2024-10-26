using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LibrarySystemAPI
{
    public class SwaggerVersioningConfiguration : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        public SwaggerVersioningConfiguration(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            // Create a Swagger document for each discovered API version
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = $"Library System API {description.ApiVersion}",
                        Version = description.ApiVersion.ToString(),
                        Description = description.IsDeprecated
                            ? "This API version has been deprecated."
                            : "Library System API version."
                    });
            }
        }
    }
}
