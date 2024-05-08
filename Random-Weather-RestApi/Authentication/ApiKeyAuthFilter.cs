using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Random_Weather_RestApi.Authentication
{
    public class ApiKeyAuthFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;

        public ApiKeyAuthFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var extractedApiKey))
            {
                string message = "Api key missing";

                var datas = new
                {
                    message
                };

                context.Result = new UnauthorizedObjectResult(datas);

                return;
            }

            var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);

            if (!apiKey.Equals(extractedApiKey))
            {

                string message = "Invalid Api Key";

                var datas = new
                {
                    message
                };

                context.Result = new UnauthorizedObjectResult(datas);

                return;
            }

        }
    }
}
