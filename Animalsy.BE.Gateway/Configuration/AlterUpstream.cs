using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Animalsy.BE.Gateway.Configuration;

public class AlterUpstream
{
    public static string AlterUpstreamSwaggerJson(HttpContext context, string swaggerJson)
    {
        var swagger = JObject.Parse(swaggerJson);
        return swagger.ToString(Formatting.Indented);
    }
}
