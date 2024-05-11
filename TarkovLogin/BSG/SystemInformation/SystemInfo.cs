using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApp1.BSG.SystemInformation;

public class SystemInfo
{
    public int Version { get; } = 2;

    public string MachineName { get; set; }

    public string Checksum { get; private set; }

    public SmSystemFirmwareInfo SmSystemFirmware { get; set; }

    public SmBiosInfo SmBios { get; set; }

    public SmSystemInfo SmSystem { get; set; }

    public SmBaseboardInfo SmBaseboard { get; set; }

    public SmProcessorInfo SmProcessor { get; set; }

    public BaseboardInfo Baseboard { get; set; }

    public BiosInfo Bios { get; set; }

    public IEnumerable<CpuInfo> Cpu { get; set; }

    public IEnumerable<GpuInfo> Gpu { get; set; }

    public IEnumerable<NicInfo> Nic { get; set; }

    public OsInfo Os { get; set; }

    public RamInfo Ram { get; set; }

    public IEnumerable<StorageInfo> Storage { get; set; }

    public AudioInfo Audio { get; set; }

    public string ComputeHardwareId()
    {
        var unixTimeSeconds = DateTime.UtcNow.ToUnixTimeStamp() / 1000000L;

        var biosInfo = Bios;
        var baseboardInfo = Baseboard;
        var cpuInfo = Cpu.FirstOrDefault();
        string?[] cpuComponents = [cpuInfo?.Manufacturer, cpuInfo?.Name, cpuInfo?.SerialNumber, cpuInfo?.UniqueId];
        var osInfo = Os;
        string?[] serialNumbers =
            [baseboardInfo.SerialNumber, biosInfo.SerialNumber, cpuInfo?.UniqueId, osInfo.SerialNumber];

        string[] components =
        [
            "#1",
            string.Empty,
            string.Concat(biosInfo.Manufacturer, biosInfo.Name, biosInfo.SerialNumber).Sha1().ToHex(),
            string.Concat(baseboardInfo.Manufacturer, baseboardInfo.Name, baseboardInfo.Product,
                baseboardInfo.SerialNumber).Sha1().ToHex(),
            string.Concat(cpuComponents.Where(component => !string.IsNullOrEmpty(component))).Sha1().ToHex(),
            string.Concat(osInfo.Manufacturer, osInfo.SerialNumber).Sha1().ToHex(),
            string.Concat(serialNumbers.Where(serialNumber => !string.IsNullOrEmpty(serialNumber))).Sha1().ToHex()
        ];
        components[1] = string.Concat(unixTimeSeconds, components).Sha1().ToHex();
        return string.Join('-', components);
    }

    public SystemInfo UpdateChecksum()
    {
        var unixTimeStamp = DateTime.UtcNow.ToUnixTimeStamp() / 1000000L;
        var jobject = JObject.FromObject(this);
        jobject[nameof(Checksum)] = null;
        Checksum = unixTimeStamp + MD5.HashData(Encoding.UTF8.GetBytes(jobject.ToString(Formatting.None))).ToHex();
        return this;
    }
}