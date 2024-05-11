using NewtonsoftJson = Newtonsoft.Json;
using SystemJson = System.Text.Json.Serialization;

namespace ConsoleApp1.BSG.SystemInformation;

public class StorageInfo
{
    [SystemJson.JsonIgnore]
    [NewtonsoftJson.JsonIgnore]
    public uint Index { get; set; }
    
    public string Model { get; set; }
    
    public string SerialNumber { get; set; }
    
    [SystemJson.JsonIgnore]
    [NewtonsoftJson.JsonIgnore]
    public ulong Size { get; set; }
    
    public uint SizeGb { get; set; }
    
    public string MediaType { get; set; }
    
    public bool IsLauncherInstalled { get; set; }
    
    public bool IsSystemDrive { get; set; }
    
    public override string ToString()
    {
        return Model;
    }
}