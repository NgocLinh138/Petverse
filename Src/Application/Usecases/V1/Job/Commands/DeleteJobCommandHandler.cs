using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Job;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;


namespace Application.Usecases.V1.Job.Commands;
public sealed class DeleteJobCommandHandler : ICommandHandler<Command.DeleteJobCommand>
{
    private readonly IJobRepository jobRepository;
    public DeleteJobCommandHandler(IJobRepository jobRepository)
    {
        this.jobRepository = jobRepository;
    }

    public async Task<Result> Handle(Command.DeleteJobCommand request, CancellationToken cancellationToken)
    {
        var Job = await jobRepository.FindByIdAsync(request.Id);
        if (Job == null)
            return Result.Failure("Không tìm thấy công việc.", StatusCodes.Status404NotFound);

        jobRepository.Remove(Job);
        return Result.Success(202);
    }
}
