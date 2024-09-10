namespace RefitWithPolly.Refit.AuthenticationHandler;
public class ApiResourceAuthenticationHandler(
    ApiResourceConfiguration apiResourceConfiguration) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        try
        {
            Console.WriteLine("going to add token");

            var token = GetToken(apiResourceConfiguration.Scope);

            request.Headers.Add("Authorization", $"Bearer {token}");

            return await base.SendAsync(request, cancellationToken);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    private string GetToken(string scope)
    {
        //get token
        return "";
    }
}
