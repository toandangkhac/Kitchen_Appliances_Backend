using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace API_Gateway.Configs
{
    public class AlterUpstream
    {
        public static string AlterUpstreamSwaggerJson(HttpContext context, string swaggerJson)
        {
            var swagger = JObject.Parse(swaggerJson);
            return swagger.ToString(Formatting.Indented);
        }
    }
}
