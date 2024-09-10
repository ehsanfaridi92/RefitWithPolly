namespace RefitWithPolly.Configuration;

public record WaitAndRetryConfig
{
    public int MaxRetryAttempts { get; init; }
    public int Delay { get; init; }
    public int TotalRequestTimeout { get; init; }
}