using Newtonsoft.Json.Converters;
using NewtonsoftJson = Newtonsoft.Json;
using SystemJson = System.Text.Json.Serialization;

namespace ConsoleApp1.BSG.SystemInformation;

public class GpuInfo
{
    [NewtonsoftJson.JsonConverter(typeof(StringEnumConverter))]
    [SystemJson.JsonConverter(typeof(SystemJson.JsonStringEnumConverter<GpuControllerAvailability>))]
    public GpuControllerAvailability Availability { get; set; }
    
    public string Name { get; set; }
    
    public string VideoProcessor { get; set; }
    
    public string DriverVersion { get; set; }
    
    [NewtonsoftJson.JsonIgnore]
    [SystemJson.JsonIgnore]
    public uint AdapterRam { get; set; }
    
    public uint AdapterRamGb { get; set; }

    public override string ToString()
    {
        return Name;
    }
}