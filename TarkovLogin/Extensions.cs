using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1;

public static class Extensions
{
    private static readonly DateTime DayOneBaby = new(1970, 1, 1);
    
    public static long ToUnixTimeStamp(this DateTime dateTime)
    {
        return (long)dateTime.Subtract(DayOneBaby).TotalSeconds;
    }
    
    public static void TryRemoveHeaders(this HttpRequestHeaders httpRequestHeaders, IEnumerable<string> headerNames)
    {
        foreach (var headerName in headerNames)
            httpRequestHeaders.TryRemoveHeader(headerName);
    }

    private static void TryRemoveHeader(this HttpRequestHeaders httpRequestHeaders, string name)
    {
        try
        {
            httpRequestHeaders.Remove(name);
        }
        catch
        {
            // ignore.
        }
    }

    public static void Add(this HttpRequestHeaders httpRequestHeaders, Dictionary<string, string> headers)
    {
        foreach (var (name, value) in headers)
            httpRequestHeaders.TryAddWithoutValidation(name, value);
    }

    public static byte[] Sha1(this string str)
    {
        return SHA1.HashData(Encoding.UTF8.GetBytes(str));
    }

    public static string ToHex(this byte[] bytes)
    {
        ArgumentNullException.ThrowIfNull(bytes);
        var sb = new StringBuilder(bytes.Length * 2);
        foreach (var b in bytes) sb.Append(b.ToString("x2"));
        return sb.ToString();
    }
}