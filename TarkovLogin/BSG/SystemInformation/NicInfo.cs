using NewtonsoftJson = Newtonsoft.Json;
using SystemJson = System.Text.Json.Serialization;

namespace ConsoleApp1.BSG.SystemInformation;

public class NicInfo
{
    public string Name { get; set; }
    
    public string MacAddress { get; set; }
    
    public bool IsCurrent { get; set; }
    
    [NewtonsoftJson.JsonIgnore]
    [SystemJson.JsonIgnore]
    public bool PhysicalAdapter { get; set; }
    
    [NewtonsoftJson.JsonIgnore]
    [SystemJson.JsonIgnore]
    public uint InterfaceIndex { get; set; }

    public override string ToString()
    {
        return Name;
    }
}