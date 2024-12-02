//using Contract.Abstractions.Message;

//namespace Contract.Services.V1.Breed
//{
//    public class Command
//    {
//        public record CreateBreedCommand : ICommand<Responses.BreedResponse>
//        {
//            public string Name { get; init; } = null!;
//            public string Description { get; init; } = null!;
//            public int PetTypeId { get; init; }
//        }

//        public record UpdateBreedCommand : ICommand<Responses.BreedResponse>
//        {
//            public int Id { get; init; }
//            public string Name { get; init; } = null!;
//            public string Description { get; init; } = null!;
//            public int PetTypeId { get; init; }
//        }

//        public record DeleteBreedCommand : ICommand
//        {
//            public int Id { get; init; }
//        }
//    }
//}
