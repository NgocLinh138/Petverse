using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Application;
using Domain.Abstractions.Repositories;

namespace Application.Usecases.V1.Application.Queries
{
    public sealed class GetApplicationStatusByUserIdQueryHandler : IQueryHandler<Query.GetApplicationStatusByUserIdQuery, Responses.ApplicationResponse>
    {
        private readonly IApplicationRepository applicationRepository;
        private readonly IMapper mapper;

        public GetApplicationStatusByUserIdQueryHandler(
            IApplicationRepository applicationRepository,
            IMapper mapper)
        {
            this.applicationRepository = applicationRepository;
            this.mapper = mapper;
        }

        public async Task<Result<Responses.ApplicationResponse>> Handle(Query.GetApplicationStatusByUserIdQuery request, CancellationToken cancellationToken)
        {
            var application = await applicationRepository.FindByUserIdAsync(request.UserId, cancellationToken);
            if (application == null)
                return Result.Failure<Responses.ApplicationResponse>("Không tìm thấy đơn ứng tuyển.", 404);
            

            var response = mapper.Map<Responses.ApplicationResponse>(application);
            return Result.Success(response, 200);
        }
    }
}
