using System.Net;
using System.Text.Json;
using FakeItEasy;

namespace Testing.Utils.HttpClient;

public class StubHttpClientHelpers
{
    public static System.Net.Http.HttpClient CreateStubHttpClient<TResponse>(TResponse responseContent, HttpStatusCode statusCode)
    {
        //sereliaze response content
        var responseContentString = JsonSerializer.Serialize<TResponse>(responseContent);
        
        var httpMessageHandler = A.Fake<AbstractHttpHandler>(options => options.CallsBaseMethods());
        
        A.CallTo(() => httpMessageHandler.SendAsync(A<HttpMethod>._, A<string>._))
            .Returns(new HttpResponseMessage(statusCode) { Content = new StringContent(responseContentString)});
        
        return new System.Net.Http.HttpClient(httpMessageHandler);
    }
}