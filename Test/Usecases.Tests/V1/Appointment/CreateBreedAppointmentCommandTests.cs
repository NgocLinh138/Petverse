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

//public sealed class CreateBreedAppointmentCommandTests
//{
//    private readonly IBreedAppointmentRepository breedAppointmentRepository;
//    private readonly UserManager<AppUser> userManager;
//    private readonly ICenterBreedRepository centerBreedRepository;
//    private readonly IPetRepository petRepository;
//    private readonly IUnitOfWork unitOfWork;
//    private readonly IMapper mapper;
//    private readonly CreateBreedAppointmentCommandHandler handler;
//    private readonly Command.CreateBreedAppointmentCommand request;
//    private const string DateFormat = "dd/MM/yyyy HH:mm";


//    public CreateBreedAppointmentCommandTests()
//    {
//        breedAppointmentRepository = A.Fake<IBreedAppointmentRepository>();
//        userManager = A.Fake<UserManager<AppUser>>();
//        centerBreedRepository = A.Fake<ICenterBreedRepository>();
//        petRepository = A.Fake<IPetRepository>();
//        unitOfWork = A.Fake<IUnitOfWork>();
//        mapper = A.Fake<IMapper>();

//        handler = new CreateBreedAppointmentCommandHandler(
//            breedAppointmentRepository,
//            mapper,
//            unitOfWork,
//            userManager,
//            petRepository,
//            centerBreedRepository
//        );

//        request = new Command.CreateBreedAppointmentCommand
//        {
//            UserId = Guid.NewGuid(),
//            CenterBreedId = 1,
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
//        A.CallTo(() => userManager.FindByIdAsync(request.UserId.ToString())).Returns(Task.FromResult<AppUser>(null));

//        // Act
//        var result = await handler.Handle(request, CancellationToken.None);

//        // Assert
//        result.IsSuccess.Should().BeFalse();
//        result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
//    }

//    [Fact]
//    public async Task Handle_Should_ReturnFailure_When_InsufficientBalance()
//    {
//        // Arrange
//        var user = new AppUser { Balance = 500 };
//        A.CallTo(() => userManager.FindByIdAsync(request.UserId.ToString())).Returns(user);
//        A.CallTo(() => centerBreedRepository.FindByIdAsync(request.CenterBreedId, CancellationToken.None))
//            .Returns(new CenterBreed());
//        A.CallTo(() => petRepository.FindSingleAsync(A<Expression<Func<Pet, bool>>>.Ignored, CancellationToken.None))
//            .Returns(new Pet());

//        // Act
//        var result = await handler.Handle(request, CancellationToken.None);

//        // Assert
//        result.IsSuccess.Should().BeFalse();
//        result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
//    }


//    [Fact]
//    public async Task Handle_Should_ReturnFailure_When_CenterBreedNotFound()
//    {
//        // Arrange
//        A.CallTo(() => userManager.FindByIdAsync(request.UserId.ToString())).Returns(new AppUser());
//        A.CallTo(() => centerBreedRepository.FindByIdAsync(request.CenterBreedId, CancellationToken.None))
//            .Returns(Task.FromResult<CenterBreed>(null));

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
//        A.CallTo(() => centerBreedRepository.FindByIdAsync(request.CenterBreedId, CancellationToken.None))
//            .Returns(new CenterBreed());
//        A.CallTo(() => petRepository.FindSingleAsync(A<Expression<Func<Pet, bool>>>.Ignored, CancellationToken.None)).Returns(Task.FromResult<Pet>(null));

//        // Act
//        var result = await handler.Handle(request, CancellationToken.None);

//        // Assert
//        result.IsSuccess.Should().BeFalse();
//        result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
//    }

//    [Fact]
//    public async Task Handle_Should_ReturnSuccess_When_BreedAppointmentCreatedSuccessfully()
//    {
//        // Arrange
//        var user = new AppUser();
//        var centerBreed = new CenterBreed { Id = request.CenterBreedId, PetCenterId = Guid.NewGuid() };
//        var pet = new Pet { Id = request.PetId, UserId = request.UserId };

//        A.CallTo(() => userManager.FindByIdAsync(request.UserId.ToString())).Returns(user);
//        A.CallTo(() => centerBreedRepository.FindByIdAsync(request.CenterBreedId, CancellationToken.None)).Returns(centerBreed);
//        A.CallTo(() => petRepository.FindSingleAsync(A<Expression<Func<Pet, bool>>>.Ignored, CancellationToken.None)).Returns(pet);

//        var newBreedAppointment = new BreedAppointment
//        {
//            Id = Guid.NewGuid(),
//            UserId = request.UserId,
//            CenterBreedId = centerBreed.Id,
//            PetCenterId = centerBreed.PetCenterId,
//            StartTime = DateTimeConverters.StringToDate(request.StartTime, DateFormat).Value,
//            EndTime = DateTimeConverters.StringToDate(request.EndTime, DateFormat).Value,
//            Amount = request.Amount,
//            Status = AppointmentStatus.Waiting
//        };

//        A.CallTo(() => breedAppointmentRepository.AddAsync(A<BreedAppointment>.Ignored)).Returns(Task.CompletedTask);
//        A.CallTo(() => unitOfWork.SaveChangesAsync(A<CancellationToken>._))
//                .Returns(Task.CompletedTask);

//        var response = new Responses.BreedAppointmentResponse { Id = newBreedAppointment.Id, Status = nameof(newBreedAppointment.Status) };

//        A.CallTo(() => mapper.Map<Responses.BreedAppointmentResponse>(A<BreedAppointment>.Ignored)).Returns(response);

//        // Act
//        var result = await handler.Handle(request, CancellationToken.None);

//        // Assert
//        result.IsSuccess.Should().BeTrue();
//        result.StatusCode.Should().Be(StatusCodes.Status201Created);
//        result.Data.Should().BeEquivalentTo(response);
//    }
//}

