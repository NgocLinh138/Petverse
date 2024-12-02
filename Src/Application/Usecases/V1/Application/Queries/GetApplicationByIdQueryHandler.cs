using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Application;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.Application.Queries
{
    public sealed class GetApplicationByIdQueryHandler : IQueryHandler<Query.GetApplicationByIdQuery, Responses.ApplicationResponse>
    {
        private readonly IApplicationRepository applicationRepository;
        private readonly IMapper mapper;

        public GetApplicationByIdQueryHandler(
            IApplicationRepository applicationRepository,
            IMapper mapper)
        {
            this.applicationRepository = applicationRepository;
            this.mapper = mapper;
        }

        public async Task<Result<Responses.ApplicationResponse>> Handle(Query.GetApplicationByIdQuery request, CancellationToken cancellationToken)
        {
            var application = await applicationRepository.FindByIdAsync(request.Id, cancellationToken);
            if (application == null)
                return Result.Failure<Responses.ApplicationResponse>("Không tìm thấy đơn ứng tuyển.", StatusCodes.Status404NotFound);

            var response = mapper.Map<Responses.ApplicationResponse>(application);

            return Result.Success(response);
        }
    }
}
