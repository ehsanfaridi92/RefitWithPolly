namespace RefitWithPolly.Refit;
public sealed class CharactersDecorator(ICharactersService charactersService,
    ResiliencePipelineProvider<string> resiliencePipeLineProvider) : ICharactersService
{
    public async Task<string> GetCharacters(long id, CancellationToken cancellationToken)
    {
        try
        {

            var pipeLine = resiliencePipeLineProvider.GetPipeline(ApiResourceConfiguration.ResilienceName);

            var getCharacters = await pipeLine.ExecuteAsync(async ct =>
                await charactersService.GetCharacters(id, ct), cancellationToken);

            return getCharacters;

        }
        catch (Exception e)
        {
            throw;
        }
    }
}