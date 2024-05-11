using System.Text.Json.Serialization;

namespace ConsoleApp1.BSG;

[Newtonsoft.Json.JsonConverter(typeof(ApiContentJsonConverter))]
public record ApiContent
{
    [JsonPropertyName("code")] public ApiStatusCode Code { get; set; }
    
    [JsonPropertyName("message")] public string Message { get; set; } = null!;

    [JsonPropertyName("data")] public object? Data { get; set; }

    [JsonPropertyName("args")] public object[] Args { get; set; } = Array.Empty<object>();
}