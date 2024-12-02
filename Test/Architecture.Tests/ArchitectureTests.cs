using Contract.Abstractions.Message;
using FluentAssertions;
using NetArchTest.Rules;

namespace Architecture.Tests;

public class ArchitectureTests
{
    private const string DomainNamespace = "Domain";
    private const string ApplicationNamespace = "Application";
    private const string InfrastructureNamespace = "Infrastructure";
    private const string PersistenceNamespace = "Persistence";
    private const string PresentationNamespace = "Presentation";
    private const string ApiNamespace = "API";

    #region TestReferenceProject
    [Fact]
    public void Domain_Should_Not_HaveDependenceOnOtherProject()
    {
        // Arrange
        var assembly = Domain.AssemblyReference.Assembly;
        var otherProject = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            PersistenceNamespace,
            PresentationNamespace,
            ApiNamespace
        };

        // Act
        var testResult = Types.InAssembly(assembly)
            .ShouldNot().HaveDependencyOnAny(otherProject)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_Not_HaveDependenceOnOtherProject()
    {
        // Arrange
        var assembly = Application.AssemblyReference.Assembly;
        var otherProject = new[]
        {
            //InfrastructureNamespace, // Due to
            //PersistenceNamespace,// Due to Implement sort multi columns by apply RawQuery with EntityFramework
            PresentationNamespace,
            ApiNamespace
        };

        // Act
        var testResult = Types.InAssembly(assembly)
            .ShouldNot().HaveDependencyOnAny(otherProject)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_Should_Not_HaveDependenceOnOtherProject()
    {
        // Arrange
        var assembly = Infrastructure.AssemblyReference.Assembly;
        var otherProject = new[]
        {
            PresentationNamespace,
            ApiNamespace,
            DomainNamespace
        };

        // Act
        var testResult = Types.InAssembly(assembly)
            .ShouldNot().HaveDependencyOnAny(otherProject)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Persistence_Should_Not_HaveDependenceOnOtherProject()
    {
        // Arrange
        var assembly = Persistence.AssemblyReference.Assembly;
        var otherProject = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            PresentationNamespace,
            ApiNamespace
        };

        // Act
        var testResult = Types.InAssembly(assembly)
            .ShouldNot().HaveDependencyOnAny(otherProject)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Presentation_Should_Not_HaveDependenceOnOtherProject()
    {
        // Arrange
        var assembly = Presentation.AssemblyReference.Assembly;
        var otherProject = new[]
        {
            InfrastructureNamespace,
            PersistenceNamespace,
            ApiNamespace,
            DomainNamespace
        };

        // Act
        var testResult = Types.InAssembly(assembly)
            .ShouldNot().HaveDependencyOnAny(otherProject)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }
    #endregion

    #region Test_NameOf_Command

    [Fact]
    public void Command_Should_Have_NamingConventationEndingCommand()
    {
        // Arrage
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types.InAssembly(assembly)
            .That().ImplementInterface(typeof(ICommand))
            .Should().HaveNameEndingWith("Command")
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandT_Should_Have_NamingConventationEndingCommand()
    {
        // Arrage
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types.InAssembly(assembly)
            .That().ImplementInterface(typeof(ICommand<>))
            .Should().HaveNameEndingWith("Command")
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandHandler_Should_Have_NamingConventationEndingCommandHandler()
    {
        // Arrage
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types.InAssembly(assembly)
            .That().ImplementInterface(typeof(ICommandHandler<>))
            .Should().HaveNameEndingWith("CommandHandler")
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandHandlerT_Should_Have_NamingConventationEndingCommandHandler()
    {
        // Arrage
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types.InAssembly(assembly)
            .That().ImplementInterface(typeof(ICommandHandler<,>))
            .Should().HaveNameEndingWith("CommandHandler")
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Command_Should_Have_BeSealed()
    {
        // Arrage
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types.InAssembly(assembly)
            .That().ImplementInterface(typeof(ICommand))
            .Should().BeSealed()
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandT_Should_Have_BeSealed()
    {
        // Arrage
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types.InAssembly(assembly)
            .That().ImplementInterface(typeof(ICommand<>))
            .Should().BeSealed()
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandHandlerT_Should_Have_BeSealed()
    {
        // Arrage
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types.InAssembly(assembly)
            .That().ImplementInterface(typeof(ICommandHandler<,>))
            .Should().BeSealed()
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void CommandHandler_Should_Have_BeSealed()
    {
        // Arrage
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types.InAssembly(assembly)
            .That().ImplementInterface(typeof(ICommandHandler<>))
            .Should().BeSealed()
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    #endregion

    #region Test_NameOf_Query

    [Fact]
    public void QueryT_Should_Have_NamingConventationEndingQuery()
    {
        // Arrage
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types.InAssembly(assembly)
            .That().ImplementInterface(typeof(IQuery<>))
            .Should().HaveNameEndingWith("Query")
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void QueryHandler_Should_Have_NamingConventationEndingQueryHandler()
    {
        // Arrage
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types.InAssembly(assembly)
            .That().ImplementInterface(typeof(IQueryHandler<,>))
            .Should().HaveNameEndingWith("QueryHandler")
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void QueryT_Should_Have_BeSealed()
    {
        // Arrage
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types.InAssembly(assembly)
            .That().ImplementInterface(typeof(IQuery<>))
            .Should().BeSealed()
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void QueryHandlerT_Should_Have_BeSealed()
    {
        // Arrage
        var assembly = Application.AssemblyReference.Assembly;

        // Act
        var testResult = Types.InAssembly(assembly)
            .That().ImplementInterface(typeof(IQueryHandler<,>))
            .Should().BeSealed()
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    #endregion
}