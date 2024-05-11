using NewtonsoftJson = Newtonsoft.Json;
using SystemJson = System.Text.Json.Serialization;

namespace ConsoleApp1.BSG.SystemInformation;

[NewtonsoftJson.JsonObject(NewtonsoftJson.MemberSerialization.OptOut)]
public class ServerAvailabilityInfo(string hostNameOrIpAddress, int ping, TracertEntry[] traceRoute)
{
    [NewtonsoftJson.JsonProperty(PropertyName = "Address")]
    [SystemJson.JsonPropertyName("Address")]
    public string HostNameOrIpAddress { get; } = hostNameOrIpAddress;

    public int Ping { get; } = ping;
    
    public TracertEntry[] TraceRoute { get; } = traceRoute;
    
    public override string ToString()
    {
        return NewtonsoftJson.JsonConvert.SerializeObject(this);
    }
}