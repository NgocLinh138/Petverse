using AutoMapper;
using BenchmarkDotNet.Attributes;
using Contract.Services.V1.Species;

namespace Benchmarks;

public class MapperAndManualMappingBenchmark
{
    private IMapper mapper;
    private Domain.Entities.Species species;

    [GlobalSetup]
    public void Setup()
    {
        species = new Domain.Entities.Species
        {
            Id = 1,
            Name = "Dog"
        };

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Domain.Entities.Species, Responses.SpeciesResponse>();
        });
        mapper = config.CreateMapper();
    }

    [Benchmark]
    public async Task<Responses.SpeciesResponse> UsingMapper()
    {
        return await Task.FromResult(mapper.Map<Responses.SpeciesResponse>(species));
    }

    [Benchmark]
    public async Task<Responses.SpeciesResponse> UsingManualMapping()
    {
        var speciesResponse = new Responses.SpeciesResponse
        {
            Id = species.Id,
            Name = species.Name
        };

        return await Task.FromResult(speciesResponse);
    }


    /* Result
    | Method             | Mean     | Error    | StdDev   |
    |------------------- |---------:|---------:|---------:|
    | UsingMapper        | 87.85 ns | 2.246 ns | 6.552 ns |
    | UsingManualMapping | 21.80 ns | 0.437 ns | 0.742 ns |
    */
}
