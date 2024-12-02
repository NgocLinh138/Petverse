using Application.Usecases.V1.Appointment.Commands;
using Contract.Services.V1.Appointment;
using Domain.Abstractions.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;

namespace Usecases.Tests.V1.Appointment;

public sealed class DeleteBreedAppointmentCommandTests
{
    private readonly IAppointmentRepository appointmentRepository;
    private readonly DeleteAppointmentCommandHandler handler;

    public DeleteBreedAppointmentCommandTests()
    {
        appointmentRepository = A.Fake<IAppointmentRepository>();
        handler = new DeleteAppointmentCommandHandler(appointmentRepository);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_BreedAppointmentNotFound()
    {
        // Arrange
        var request = new Command.DeleteAppointmentCommand(Guid.NewGuid());
        A.CallTo(() => appointmentRepository.FindByIdAsync(request.Id, CancellationToken.None))
            .Returns(Task.FromResult<Domain.Entities.Appointment>(null));

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        result.Message.Should().Be("Không tìm thấy cuộc hẹn.");
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_When_BreedAppointmentDeletedSuccessfully()
    {
        // Arrange
        var appointment = new Domain.Entities.BreedAppointment { Id = Guid.NewGuid() };
        var request = new Command.DeleteAppointmentCommand(Guid.NewGuid());

        A.CallTo(() => appointmentRepository.FindByIdAsync(request.Id, CancellationToken.None))
            .Returns(appointment);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(StatusCodes.Status202Accepted);
        A.CallTo(() => appointmentRepository.Remove(appointment)).MustHaveHappenedOnceExactly();
    }
}
