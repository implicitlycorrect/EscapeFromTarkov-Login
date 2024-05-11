using Ionic.Zlib;

namespace ConsoleApp1.BSG.Compression;

public sealed class ZlibDelegatingHandler(HttpMessageHandler innerHandler) : DelegatingHandler(innerHandler)
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // Compress request content
        if (request.Content != null)
            request.Content = new ByteArrayContent(ZlibStream.CompressBuffer(await request.Content.ReadAsByteArrayAsync(cancellationToken)));

        request.Headers.TryRemoveHeaders([
            "Content-Type",
            "Transfer-Encoding"
        ]);

        // Send the request and get the response
        var response = await base.SendAsync(request, cancellationToken);
        response.Content = new ByteArrayContent(ZlibStream.UncompressBuffer(await response.Content.ReadAsByteArrayAsync(cancellationToken)));
        return response;
    }
}