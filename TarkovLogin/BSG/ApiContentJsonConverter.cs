using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp1.BSG;

public sealed class ApiContentJsonConverter : JsonConverter<ApiContent>
{
    public override ApiContent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var document = JsonDocument.ParseValue(ref reader);
        var root = document.RootElement;

        if (!root.TryGetProperty("err", out var errorCodeProperty) ||
            !root.TryGetProperty("errmsg", out var errorMessageProperty))
            return root.Deserialize<ApiContent>();
        
        var statusCode = (ApiStatusCode) errorCodeProperty.GetInt32();
        var message = errorMessageProperty.GetString() ?? throw new Exception("error message shouldn't be null");
        var data = root.GetProperty("data").Deserialize<object>();
        var args = root.TryGetProperty("args", out var argsProperty)
            ? argsProperty.Deserialize<object[]>() ?? Array.Empty<object>()
            : Array.Empty<object>();
        
        return new ApiContent
        {
            Code = statusCode,
            Message = message,
            Data = data,
            Args = args
        };
    }

    public override void Write(Utf8JsonWriter writer, ApiContent value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}