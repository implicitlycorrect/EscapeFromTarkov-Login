namespace ConsoleApp1.BSG.SystemInformation;

public class RamInfo
{
    public uint TotalSizeGb { get; set; }
    
    public IEnumerable<RamModuleInfo> Modules { get; set; }
}