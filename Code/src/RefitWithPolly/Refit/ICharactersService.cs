namespace RefitWithPolly.Refit;
public interface ICharactersService
{
    [Get("/characters/{id}")]
    Task<string> GetCharacters(long id, CancellationToken cancellationToken);
}