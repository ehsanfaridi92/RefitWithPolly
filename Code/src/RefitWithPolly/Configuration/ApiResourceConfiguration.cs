namespace RefitWithPolly.Configuration;

public record ApiResourceConfiguration
{
    public static string ResilienceName = "Api_ResilienceName";
    public string Scope { get; set; }
    public string BaseUrl { get; init; }
    public WaitAndRetryConfig WaitAndRetryConfig { get; init; }
}