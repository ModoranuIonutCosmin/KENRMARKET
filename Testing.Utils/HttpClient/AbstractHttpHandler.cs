namespace Testing.Utils.HttpClient;

public abstract class AbstractHttpHandler : HttpClientHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(SendAsync(request.Method, request.RequestUri.AbsoluteUri));
    }

    public abstract HttpResponseMessage SendAsync(HttpMethod method, string url);
}