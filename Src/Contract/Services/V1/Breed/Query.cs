//using Contract.Abstractions.Message;
//using Contract.Abstractions.Shared;
//using Contract.Enumerations;

//namespace Contract.Services.V1.Breed
//{
//    public static class Query
//    {
//        public record GetBreedQuery : IQuery<PagedResult<Responses.BreedResponse>>
//        {
//            public GetBreedQuery() : this(null, null, SortOrder.Ascending, 1, 10) { }

//            public GetBreedQuery(string? searchTerm, string? sortColumn, SortOrder sortOrder, int pageIndex, int pageSize)
//            {
//                SearchTerm = searchTerm;
//                SortColumn = sortColumn;
//                SortOrder = sortOrder;
//                PageIndex = pageIndex;
//                PageSize = pageSize;
//            }

//            public string? SearchTerm { get; init; }
//            public string? SortColumn { get; init; }
//            public SortOrder SortOrder { get; init; }
//            public int PageIndex { get; init; } = 1;
//            public int PageSize { get; init; } = 10;
//        }

//        public record GetBreedByIdQuery : IQuery<Responses.BreedResponse>
//        {
//            public int Id { get; init; }
//        }
//    }
//}
