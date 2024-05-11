namespace ConsoleApp1.BSG.SystemInformation;

public class RamModuleInfo
{
    public string Manufacturer { get; set; }
    
    public string PartNumber { get; set; }
    
    public string SerialNumber { get; set; }
    
    public ulong Capacity { get; set; }
    
    public override string ToString()
    {
        return Manufacturer;
    }
}