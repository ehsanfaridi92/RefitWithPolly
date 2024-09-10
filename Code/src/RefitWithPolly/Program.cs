
var builder = Host.CreateApplicationBuilder(args);

var apiResourceConfiguration = builder.Configuration.GetSection(nameof(ApiResourceConfiguration)).Get<ApiResourceConfiguration>();

builder.Services.AddSingleton(apiResourceConfiguration);

builder.Services.AddTransient<ApiResourceAuthenticationHandler>();

builder.Services.AddRefitClient<ICharactersService>()
    .ConfigureHttpClient((sp, httpClient) =>
    {

        httpClient.BaseAddress = new Uri(apiResourceConfiguration.BaseUrl);
    })
    .AddHttpMessageHandler<ApiResourceAuthenticationHandler>();

builder.Services.AddResiliencePipeline(ApiResourceConfiguration.ResilienceName, x =>
{
    x.AddRetry(new RetryStrategyOptions()
    {
        ShouldHandle = new PredicateBuilder().Handle<Exception>(),
        Delay = TimeSpan.FromSeconds(apiResourceConfiguration.WaitAndRetryConfig.Delay),
        MaxRetryAttempts = apiResourceConfiguration.WaitAndRetryConfig.MaxRetryAttempts,
        BackoffType = DelayBackoffType.Exponential,
        UseJitter = true
    })
        .AddTimeout(TimeSpan.FromSeconds(apiResourceConfiguration.WaitAndRetryConfig.TotalRequestTimeout))
        .Build();
});

builder.Services.Decorate<ICharactersService, CharactersDecorator>();



var host = builder.Build();

var charactersService = host.Services.GetRequiredService<ICharactersService>();

var data = await charactersService.GetCharacters(22, CancellationToken.None);

Console.WriteLine();
Console.WriteLine();
Console.WriteLine("****************data fetched *****************************"); 

Console.WriteLine(data);

Console.WriteLine();
Console.WriteLine();
Console.WriteLine("*********************************************");


host.Run();
