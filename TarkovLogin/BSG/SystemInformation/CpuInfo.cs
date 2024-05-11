namespace ConsoleApp1.BSG.SystemInformation;

public class CpuInfo
{
    public string Manufacturer { get; set; }
    
    public string Name { get; set; }
    
    public string SerialNumber { get; set; }
    
    public string? UniqueId { get; set; }
    
    public override string ToString()
    {
        return Name;
    }
}