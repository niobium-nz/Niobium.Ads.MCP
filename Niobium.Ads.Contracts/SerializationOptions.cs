using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Niobium.Ads
{
    public class SerializationOptions
    {
        public static JsonSerializerOptions SnakeCase = new()
        { 
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            TypeInfoResolver = new DefaultJsonTypeInfoResolver(),
        };
    }
}
