//using Application.Usecases.V1.Appointment.Queries;
//using AutoMapper;
//using Contract.Enumerations;
//using Contract.Services.V1.Appointment;
//using Domain.Abstractions.Repositories;
//using Domain.Entities;
//using FakeItEasy;
//using FluentAssertions;
//using Microsoft.AspNetCore.Http;

//namespace Usecases.Tests.V1.Appointment
//{
//    public sealed class GetAppointmentByIdQueryTests
//    {
//        private readonly IAppointmentRepository AppointmentRepository;
//        private readonly IMapper mapper;
//        private readonly GetAppointmentByIdQueryHandler handler;
//        private readonly Query.GetAppointmentByIdQuery request;
//        public GetAppointmentByIdQueryTests()
//        {
//            AppointmentRepository = A.Fake<IAppointmentRepository>();
//            mapper = A.Fake<IMapper>();
//            handler = new GetAppointmentByIdQueryHandler(AppointmentRepository, mapper);
//            request = new Query.GetAppointmentByIdQuery(Guid.NewGuid(), AppointmentType.BreedAppointment);
//        }

//        [Fact]
//        public async Task Handle_Should_ReturnFailure_When_AppointmentNotFound()
//        {
//            // Arrange
//            A.CallTo(() => AppointmentRepository.GetAppointmentById(request.AppointmentId, request.Type))
//                .Returns(Task.FromResult<Domain.Entities.Appointment>(null));

//            // Act
//            var result = await handler.Handle(request, CancellationToken.None);

//            // Assert
//            result.IsSuccess.Should().BeFalse();
//            result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
//        }

//        [Fact]
//        public async Task Handle_Should_ReturnSuccess_When_BreedAppointmentFound()
//        {
//            // Arrange
//            var breedAppointment = new BreedAppointment
//            {
//                Id = Guid.NewGuid(),
//                Type = AppointmentType.BreedAppointment,
//            };
//            var request = new Query.GetAppointmentByIdQuery(breedAppointment.Id, AppointmentType.BreedAppointment);

//            A.CallTo(() => AppointmentRepository.GetAppointmentById(request.AppointmentId, request.Type))
//                .Returns(Task.FromResult<Domain.Entities.Appointment>(breedAppointment));

//            var response = new Responses.AppointmentByIdResponse
//            {
//                Id = breedAppointment.Id,
//                Type = breedAppointment.Type,
//            };

//            A.CallTo(() => mapper.Map<Responses.AppointmentByIdResponse>(breedAppointment))
//                .Returns(response);

//            // Act
//            var result = await handler.Handle(request, CancellationToken.None);

//            // Assert
//            result.IsSuccess.Should().BeTrue();
//            result.Data.Should().BeEquivalentTo(response);
//        }

//        [Fact]
//        public async Task Handle_Should_ReturnSuccess_When_ServiceAppointmentFound()
//        {
//            // Arrange
//            var serviceAppointment = new ServiceAppointment
//            {
//                Id = Guid.NewGuid(),
//                Type = AppointmentType.ServiceAppointment,
//            };
//            var request = new Query.GetAppointmentByIdQuery(serviceAppointment.Id, AppointmentType.ServiceAppointment);

//            A.CallTo(() => AppointmentRepository.GetAppointmentById(request.AppointmentId, request.Type))
//                .Returns(Task.FromResult<Domain.Entities.Appointment>(serviceAppointment));

//            var response = new Responses.AppointmentByIdResponse
//            {
//                Id = serviceAppointment.Id,
//                Type = serviceAppointment.Type,
//            };

//            A.CallTo(() => mapper.Map<Responses.AppointmentByIdResponse>(serviceAppointment))
//                .Returns(response);

//            // Act
//            var result = await handler.Handle(request, CancellationToken.None);

//            // Assert
//            result.IsSuccess.Should().BeTrue();
//            result.Data.Should().BeEquivalentTo(response);
//        }
//    }
//}

