using Contract.Abstractions.Message;

namespace Contract.Services.V1.Species;
public class Command
{
    public record CreateSpeciesCommand(string Name) : ICommand<Responses.SpeciesResponse>;

    public record UpdateSpeciesCommand(
        int? Id,
        string Name) : ICommand<Responses.SpeciesResponse>;

    public record DeleteSpeciesCommand(int Id) : ICommand;

    // =============== Breed ===============

    public record CreateBreedCommand(
        int? SpeciesId,
        string Name,
        string? Description) : ICommand<Responses.BreedResponse>;

    public record UpdateBreedCommand(
        int? Id,
        int? SpeciesId,
        string Name,
        string? Description) : ICommand<Responses.BreedResponse>;

    public record DeleteBreedCommand(
        int SpeciesId,
        int Id) : ICommand;
}
