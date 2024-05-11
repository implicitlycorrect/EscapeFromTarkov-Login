using System.Buffers;

namespace ConsoleApp1.BSG.SystemInformation;

public static partial class SystemInfoGenerator
{
    public static SystemInfo Generate()
    {
        // Intel(R) Core(TM) i9-10850K CPU @ 3.60GHz
        string[] cpuNames =
        [
            "Intel(R) Core(TM) i7 processor 14650HX CPU @ 5.20", "Intel(R) Core(TM) i7 processor 14700 CPU @ 5.40",
            "Intel(R) Core(TM) i7 processor 14700F CPU @ 5.40", "Intel(R) Core(TM) i7 processor 14700HX CPU @ 5.50",
            "Intel(R) Core(TM) i7 processor 14700T CPU @ 5.20", "Intel(R) Core(TM) i7 processor 14700K CPU @ 5.60",
            "Intel(R) Core(TM) i7 processor 14700KF CPU @ 5.60",
            "Intel(R) Core(TM) i5 processor 14400 CPU @ 4.70", "Intel(R) Core(TM) i5 processor 14400F CPU @ 4.70",
            "Intel(R) Core(TM) i5 processor 14400T CPU @ 4.50", "Intel(R) Core(TM) i5 processor 14450HX CPU @ 4.80",
            "Intel(R) Core(TM) i5 processor 14500 CPU @ 5.00", "Intel(R) Core(TM) i5 processor 14500HX CPU @ 4.90",
            "Intel(R) Core(TM) i5 processor 14500T CPU @ 4.80", "Intel(R) Core(TM) i5 processor 14600 CPU @ 5.20",
            "Intel(R) Core(TM) i5 processor 14600T CPU @ 5.10", "Intel(R) Core(TM) i5 processor 14600K CPU @ 5.30",
            "Intel(R) Core(TM) i5 processor 14600KF CPU @ 5.30",
            "Intel(R) Core(TM) i9 processor 14900 CPU @ 5.80", "Intel(R) Core(TM) i9 processor 14900F CPU @ 5.80",
            "Intel(R) Core(TM) i9 processor 14900HX CPU @ 5.80", "Intel(R) Core(TM) i9 processor 14900T CPU @ 5.50",
            "Intel(R) Core(TM) i9 processor 14900K CPU @ 6.00", "Intel(R) Core(TM) i9 processor 14900KF CPU @ 6.00"
        ];
        var cpuName = cpuNames[Random.Shared.Next(cpuNames.Length)];
        var machineName = GenerateMachineName();
        var windowsSerialNumber = GenerateWindowsSerialNumber();
        var baseboardInfo = GenerateBaseboardInfo();
        var systemSerialNumber = Guid.NewGuid().ToString("D").ToUpper();

        return new SystemInfo
        {
            MachineName = machineName,
            SmSystemFirmware = new SmSystemFirmwareInfo
            {
                Version = "3.3"
            },
            SmBios = new SmBiosInfo
            {
                Vendor = "American Megatrends Inc.",
                Version = "0605",
                ReleaseDate = "03/11/2021"
            },
            SmSystem = new SmSystemInfo
            {
                Manufacturer = "ASUS",
                ProductName = "System Product Name",
                Version = "System Version",
                SerialNumber = "System Serial Number",
                Uuid = systemSerialNumber,
                SkuNumber = "SKU",
                Family = "To be filled by O.E.M."
            },
            SmBaseboard = new SmBaseboardInfo
            {
                Manufacturer = baseboardInfo["manufacturer"],
                ProductName = baseboardInfo["product"],
                Version = baseboardInfo["version"],
                SerialNumber = baseboardInfo["serialNumber"]
            },
            SmProcessor = new SmProcessorInfo
            {
                Manufacturer = "Intel(R) Corporation",
                Id = GenerateProcessorSerialNumber(),
                Version = cpuName,
                SerialNumber = "To Be Filled By O.E.M.",
                PartNumber = "To Be Filled By O.E.M."
            },
            Baseboard = new BaseboardInfo
            {
                Manufacturer = baseboardInfo["manufacturer"],
                Name = baseboardInfo["name"],
                Product = baseboardInfo["product"],
                SerialNumber = baseboardInfo["serialNumber"]
            },
            Bios = new BiosInfo
            {
                Manufacturer = "American Megatrends Inc.",
                Name = "0605",
                SerialNumber = "System Serial Number",
                Uuid = systemSerialNumber
            },
            Cpu =
            [
                new CpuInfo
                {
                    Manufacturer = "GenuineIntel",
                    Name = cpuName,
                    SerialNumber = "To Be Filled By O.E.M.",
                    UniqueId = null
                }
            ],
            Gpu = GenerateGpuInfo(),
            Nic =
            [
                new NicInfo
                {
                    Name = "Intel(R) Ethernet Controller (3) I225-V",
                    MacAddress = GenerateMacAddress(),
                    IsCurrent = true
                }
            ],
            Os = new OsInfo
            {
                Manufacturer = "Microsoft Corporation",
                Caption = "Microsoft Windows 11 Pro",
                SerialNumber = windowsSerialNumber,
                InstallationTimestamp = DateTimeOffset.FromUnixTimeSeconds(1700324022)
                    .AddDays(-Random.Shared.Next(0, 365)).ToUnixTimeSeconds(),
                Language = "en",
                Version = "10.0.22621"
            },
            Ram = GenerateRamInfo(),
            Storage = GenerateStorageInfo(),
            Audio = new AudioInfo
            {
                Endpoints =
                [
                    new AudioEndpoint
                    {
                        FrendlyName = "Realtek Digital Output (Realtek(R) Audio)",
                        DataFlow = AudioEndpointDataFlow.Render,
                        IsDefault = true
                    }
                ],
                Controllers =
                [
                    new AudioController
                    {
                        Caption = "Realtek High Definition Audio",
                        Manufacturer = "Realtek",
                        Status = "OK"
                    }
                ]
            }
        };
    }
}

public static partial class SystemInfoGenerator
{
    private static StorageInfo[] GenerateStorageInfo()
    {
        string[] models =
        [
            "KINGSTON SNV2S",
            "NVMe INTEL SSDPEKKW"
        ];
        uint[] sizes =
        [
            1864,
            932,
            476,
            238
        ];

        var isSystemDrive = false;
        var storageDeviceCount = Random.Shared.Next(1, 3);
        var storageDevices = new StorageInfo[storageDeviceCount];
        for (var i = 0; i < storageDeviceCount; i++)
        {
            var model = models[Random.Shared.Next(models.Length)];
            var sizeGb = sizes[Random.Shared.Next(sizes.Length)];
            storageDevices[i] = new StorageInfo
            {
                Model = $"{model}{sizeGb}G",
                SerialNumber = GenerateStorageDeviceSerialNumber(),
                SizeGb = sizeGb,
                MediaType = "Fixed hard disk media",
                IsLauncherInstalled = !isSystemDrive,
                IsSystemDrive = !isSystemDrive
            };
            isSystemDrive = true;
        }

        return storageDevices;

        static string GenerateStorageDeviceSerialNumber()
        {
            const string emptyPart = "0000";
            return string.Join('_', [
                emptyPart,
                emptyPart,
                Random.Shared.NextDouble() > 0.5 ? "0100" : emptyPart,
                emptyPart,
                Random.Shared.Next(0, 10000).ToString("D4"),
                Random.Shared.Next(0, 10000).ToString("D4"),
                Random.Shared.Next(0, 10000).ToString("D4"),
                Random.Shared.Next(0, 10000).ToString("D4")
            ]) + ".";
        }
    }

    private static string GenerateMacAddress()
    {
        var macBytes = new byte[6];
        Random.Shared.NextBytes(macBytes);
        macBytes[0] = (byte)(macBytes[0] & 254);
        var macAddress = string.Join(":", macBytes
            .Select(b => b.ToString("X2")));
        return macAddress;
    }

    private static RamInfo GenerateRamInfo()
    {
        string[] possibleParts =
        [
            "Corsair-CMW16GX4M2E3200C16"
        ];
        var part = possibleParts[Random.Shared.Next(possibleParts.Length)];
        var partSplits = part.Split('-');
        var (manufacturer, partNumber) = (partSplits[0], partSplits[1]);

        var moduleCount = Random.Shared.NextDouble() > 0.6 ? 4 : 2;
        var modules = new RamModuleInfo[moduleCount];
        for (var i = 0; i < moduleCount; i++)
            modules[i] = new RamModuleInfo
            {
                Manufacturer = manufacturer,
                PartNumber = partNumber,
                SerialNumber = "00000000",
                Capacity = 17179869184 / 2
            };

        return new RamInfo
        {
            TotalSizeGb = (uint)moduleCount * 8,
            Modules = modules
        };
    }

    private static GpuInfo[] GenerateGpuInfo()
    {
        const string driverVersion = "31.0.15.4665";
        string[] graphicCardNames =
        [
            "NVIDIA GeForce RTX 3070",
            "NVIDIA GeForce RTX 3070ti",
            "NVIDIA GeForce RTX 3080",
            "NVIDIA GeForce RTX 3080ti",
            "NVIDIA GeForce RTX 3090",
            "NVIDIA GeForce RTX 4060",
            "NVIDIA GeForce RTX 4060ti",
            "NVIDIA GeForce RTX 4070",
            "NVIDIA GeForce RTX 4070ti",
            "NVIDIA GeForce RTX 4080",
            "NVIDIA GeForce RTX 4090",
        ];

        var graphicsCardName = graphicCardNames[Random.Shared.Next(graphicCardNames.Length)];

        if (Random.Shared.NextDouble() < 0.15)
            return
            [
                new GpuInfo
                {
                    Availability = GpuControllerAvailability.RunningFullPower,
                    Name = graphicsCardName,
                    VideoProcessor = graphicsCardName,
                    DriverVersion = driverVersion,
                    AdapterRamGb = 4
                },
                new GpuInfo
                {
                    Availability = GpuControllerAvailability.RunningFullPower,
                    Name = graphicsCardName,
                    VideoProcessor = graphicsCardName,
                    DriverVersion = driverVersion,
                    AdapterRamGb = 4
                }
            ];
        return
        [
            new GpuInfo
            {
                Availability = GpuControllerAvailability.RunningFullPower,
                Name = graphicsCardName,
                VideoProcessor = graphicsCardName,
                DriverVersion = "31.0.15.4665",
                AdapterRamGb = 4
            }
        ];
    }

    private static string GenerateBaseboardSerialNumber()
    {
        const int serialNumberLength = 15;
        Span<char> serialNumberSpan = stackalloc char[serialNumberLength];
        for (var i = 0; i < serialNumberLength; i++)
            if (i == 0)
                serialNumberSpan[i] = (char)Math.Max(1, '0' + Random.Shared.Next(10));
            else
                serialNumberSpan[i] = (char)('0' + Random.Shared.Next(10));
        return new string(serialNumberSpan);
    }

    private static ulong GenerateProcessorSerialNumber()
    {
        var buffer = ArrayPool<byte>.Shared.Rent(sizeof(ulong));
        try
        {
            Random.Shared.NextBytes(buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }

    private static Dictionary<string, string> GenerateBaseboardInfo()
    {
        string[] productNames =
        [
            "PRIME Z790-A",
            "PRIME Z790-P",
            "TUF Z790",
        ];
        var productName = productNames[Random.Shared.Next(productNames.Length)];
        
        return new Dictionary<string, string>
        {
            ["product"] = productName,
            ["manufacturer"] = "ASUSTeK COMPUTER INC.",
            ["version"] = "Rev 1.xx",
            ["name"] = "Base Board",
            ["serialNumber"] = GenerateBaseboardSerialNumber()
        };
    }

    private static string GenerateWindowsSerialNumber()
    {
        var identifier = Random.Shared.Next(1000, 10000);
        var version = Random.Shared.Next(10000, 100000);
        var revision = Random.Shared.Next(0, 100000);
        var checksum = Guid.NewGuid().ToString("N")[..5].ToUpper();
        return $"{identifier:D5}-{version:D5}-{revision:D5}-{checksum}";
    }

    private static string GenerateMachineName()
    {
        const int machineNameLength = 15;
        const string prefix = "DESKTOP-";
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        return string.Create(machineNameLength, (chars, prefix), (span, state) =>
        {
            state.prefix.AsSpan().CopyTo(span);
            for (var i = state.prefix.Length; i < span.Length; i++)
                span[i] = state.chars[Random.Shared.Next(state.chars.Length)];
        });
    }
}