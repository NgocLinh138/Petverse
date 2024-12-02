//using Contract.Abstractions.Message;

//namespace Contract.Services.V1.PetSubType;
//public class Command
//{
//    public record CreatePetSubTypeCommand : ICommand<Responses.PetSubTypeResponse>
//    {
//        public int PetTypeId { get; set; }
//        public string SubName { get; set; } = null!;
//        public string? Description { get; set; }
//    }


//    public record UpdatePetSubTypeCommand : ICommand<Responses.PetSubTypeResponse>
//    {
//        public int? Id { get; set; }
//        public int PetTypeId { get; set; }
//        public string SubName { get; set; } = null!;
//        public string? Description { get; set; }
//    }

//    public record DeletePetSubTypeCommand : ICommand
//    {
//        public int Id { get; init; }
//    }
//}
