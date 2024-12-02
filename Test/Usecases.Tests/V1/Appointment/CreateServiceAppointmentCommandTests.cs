//using Application.Usecases.V1.Appointment.Commands;
//using AutoMapper;
//using Contract.Enumerations;
//using Contract.JsonConverters;
//using Contract.Services.V1.Appointment;
//using Domain.Abstractions;
//using Domain.Abstractions.Repositories;
//using Domain.Entities;
//using Domain.Entities.Identity;
//using FakeItEasy;
//using FluentAssertions;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using System.Linq.Expressions;

//namespace Usecases.Tests.V1.Appointment;

//public sealed class CreateServiceAppointmentCommandTests
//{
//    private readonly IServiceAppointmentRepository serviceAppointmentRepository;
//    private readonly UserManager<AppUser> userManager;
//    private readonly IPetCenterServiceRepository petCenterServiceRepository;
//    private readonly IPetRepository petRepository;
//    private readonly IUnitOfWork unitOfWork;
//    private readonly IMapper mapper;
//    private readonly CreateServiceAppointmentCommandHandler handler;
//    private readonly Command.CreateServiceAppointmentCommand request;
//    private const string DateFormat = "dd/MM/yyyy HH:mm";

//    public CreateServiceAppointmentCommandTests()
//    {
//        serviceAppointmentRepository = A.Fake<IServiceAppointmentRepository>();
//        userManager = A.Fake<UserManager<AppUser>>();
//        petCenterServiceRepository = A.Fake<IPetCenterServiceRepository>();
//        petRepository = A.Fake<IPetRepository>();
//        unitOfWork = A.Fake<IUnitOfWork>();
//        mapper = A.Fake<IMapper>();

//        handler = new CreateServiceAppointmentCommandHandler(
//            serviceAppointmentRepository,
//            mapper,
//            unitOfWork,
//            userManager,
//            petCenterServiceRepository,
//            petRepository
//        );

//        request = new Command.CreateServiceAppointmentCommand
//        {
//            UserId = Guid.NewGuid(),
//            PetCenterServiceId = 1,
//            PetId = 1,
//            StartTime = "01/01/2024 09:00",
//            EndTime = "01/01/2024 11:00",
//            Amount = 1000
//        };
//    }

//    [Fact]
//    public async Task Handle_Should_ReturnFailure_When_UserNotFound()
//    {
//        // Arrange
//        var request = new Command.CreateServiceAppointmentCommand { UserId = Guid.NewGuid() };
//        A.CallTo(() => userManager.FindByIdAsync(request.UserId.ToString())).Returns(Task.FromResult<AppUser>(null));

//        // Act
//        var result = await handler.Handle(request, CancellationToken.None);

//        // Assert
//        result.IsSuccess.Should().BeFalse();
//        result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
//    }

//    [Fact]
//    public async Task Handle_Should_ReturnFailure_When_PetCenterServiceNotFound()
//    {
//        // Arrange
//        A.CallTo(() => userManager.FindByIdAsync(request.UserId.ToString())).Returns(new AppUser());
//        A.CallTo(() => petCenterServiceRepository.FindByIdAsync(request.PetCenterServiceId, CancellationToken.None))
//            .Returns(Task.FromResult<PetCenterService>(null));

//        // Act
//        var result = await handler.Handle(request, CancellationToken.None);

//        // Assert
//        result.IsSuccess.Should().BeFalse();
//        result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
//    }

//    [Fact]
//    public async Task Handle_Should_ReturnFailure_When_PetNotFound()
//    {
//        // Arrange
//        A.CallTo(() => userManager.FindByIdAsync(request.UserId.ToString())).Returns(new AppUser());
//        A.CallTo(() => petCenterServiceRepository.FindByIdAsync(request.PetCenterServiceId, CancellationToken.None))
//            .Returns(new PetCenterService());
//        A.CallTo(() => petRepository.FindSingleAsync(A<Expression<Func<Pet, bool>>>.Ignored, CancellationToken.None)).Returns(Task.FromResult<Pet>(null));

//        // Act
//        var result = await handler.Handle(request, CancellationToken.None);

//        // Assert
//        result.IsSuccess.Should().BeFalse();
//        result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
//    }

//    [Fact]
//    public async Task Handle_Should_ReturnSuccess_When_ServiceAppointmentCreatedSuccessfully()
//    {
//        // Arrange
//        var user = new AppUser();
//        var petCenterService = new PetCenterService { Id = request.PetCenterServiceId, PetCenterId = Guid.NewGuid() };
//        var pet = new Pet { Id = request.PetId, UserId = request.UserId };

//        A.CallTo(() => userManager.FindByIdAsync(request.UserId.ToString())).Returns(user);
//        A.CallTo(() => petCenterServiceRepository.FindByIdAsync(request.PetCenterServiceId, CancellationToken.None)).Returns(petCenterService);
//        A.CallTo(() => petRepository.FindSingleAsync(A<Expression<Func<Pet, bool>>>.Ignored, CancellationToken.None)).Returns(pet);

//        var newServiceAppointment = new ServiceAppointment
//        {
//            Id = Guid.NewGuid(),
//            UserId = request.UserId,
//            PetCenterServiceId = petCenterService.Id,
//            PetCenterId = petCenterService.PetCenterId,
//            StartTime = DateTimeConverters.StringToDate(request.StartTime, DateFormat).Value,
//            EndTime = DateTimeConverters.StringToDate(request.EndTime, DateFormat).Value,
//            Amount = request.Amount,
//            Status = AppointmentStatus.Waiting
//        };

//        A.CallTo(() => serviceAppointmentRepository.AddAsync(A<ServiceAppointment>.Ignored)).Returns(Task.CompletedTask);
//        A.CallTo(() => unitOfWork.SaveChangesAsync(A<CancellationToken>._)).Returns(Task.CompletedTask);

//        var response = new Responses.ServiceAppointmentResponse { Id = newServiceAppointment.Id, Status = nameof(newServiceAppointment.Status) };

//        A.CallTo(() => mapper.Map<Responses.ServiceAppointmentResponse>(A<ServiceAppointment>.Ignored)).Returns(response);

//        // Act
//        var result = await handler.Handle(request, CancellationToken.None);

//        // Assert
//        result.IsSuccess.Should().BeTrue();
//        result.StatusCode.Should().Be(StatusCodes.Status201Created);
//        result.Data.Should().BeEquivalentTo(response);
//    }
//}