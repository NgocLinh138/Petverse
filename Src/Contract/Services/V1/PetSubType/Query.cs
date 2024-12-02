//using Contract.Abstractions.Message;
//using Contract.Abstractions.Shared;

//namespace Contract.Services.V1.PetSubType;
//public static class Query
//{
//    public record GetPetSubTypeQuery : IQuery<PagedResult<Responses.PetSubTypeResponse>>
//    {
//        public int? PetTypeId { get; set; }
//        public string? SearchTerm { get; set; }
//        public int PageIndex { get; init; }
//        public int PageSize { get; init; }
//    }

//    public record GetPetSubTypeByIdQuery : IQuery<Responses.PetSubTypeResponse>
//    {
//        public int Id { get; init; }
//    }

//}
