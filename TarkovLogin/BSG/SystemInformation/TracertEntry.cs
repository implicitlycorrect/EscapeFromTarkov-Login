using System.Net.NetworkInformation;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ConsoleApp1.BSG.SystemInformation;

[JsonObject]
public class TracertEntry
{
    public int HopId { get; set; }
    
    public string Address { get; set; }
    
    public string Hostname { get; set; }
    
    public long ReplyTime { get; set; }
    
    [JsonConverter(typeof(StringEnumConverter))]
    public IPStatus ReplyStatus { get; set; }
    
    public override string ToString()
    {
        return
            $"{HopId} | {(string.IsNullOrEmpty(Hostname) ? Address : Hostname + "[" + Address + "]")} | {(ReplyStatus == IPStatus.TimedOut ? "Request Timed Out." : ReplyTime + " ms")}";
    }
}