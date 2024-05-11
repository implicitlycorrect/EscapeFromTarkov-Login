using System.Net;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using ConsoleApp1;
using ConsoleApp1.BSG;
using ConsoleApp1.BSG.Compression;
using ConsoleApp1.BSG.SystemInformation;

Console.WriteLine("Hello, World!");

const string launcherVersion = "14.0.1.2319";

using var httpClient = new HttpClient(new ZlibDelegatingHandler(new HttpClientHandler
{
    AutomaticDecompression = DecompressionMethods.None
}), true);
httpClient.DefaultRequestHeaders.Add(new Dictionary<string, string>
{
    ["User-Agent"] = $"BsgLauncher/{launcherVersion}",
    ["Except"] = "100-continue",
    ["Keep-Alive"] = "true"
});

const string email = "";
const string password = "";
var systemInfo = SystemInfoGenerator.Generate().UpdateChecksum();
var captcha = SolveCaptcha();

using var content = JsonContent.Create(new
{
    email,
    pass = MD5.HashData(Encoding.UTF8.GetBytes(password)).ToHex(),
    hwCode = systemInfo.ComputeHardwareId(),
    systemInfo,
    captcha
}, options: new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
});

using var request = new HttpRequestMessage(HttpMethod.Post, $"https://launcher.escapefromtarkov.com/launcher/login?launcherVersion={launcherVersion}");
request.Content = content;
using var apiResponse = await httpClient.SendAsync(request);
apiResponse.EnsureSuccessStatusCode();

var apiContent = await apiResponse.Content.ReadFromJsonAsync<ApiContent>(new JsonSerializerOptions
{
    Converters = { new ApiContentJsonConverter() }
}) ?? throw new JsonException();
Console.WriteLine(apiContent);
return;

string? SolveCaptcha()
{
    return null;
}