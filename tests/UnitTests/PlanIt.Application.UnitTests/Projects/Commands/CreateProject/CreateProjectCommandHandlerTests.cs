using FluentAssertions;
using NSubstitute;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Application.Projects.Commands.CreateProject;
using PlanIt.Application.UnitTests.Projects.Commands.TestUtils;
using PlanIt.Application.UnitTests.TestUtils.Projects;

namespace PlanIt.Application.UnitTests.Projects.Commands.CreateProject;

public class CreateProjectCommandHandlerTest
{
    private readonly IProjectRepository _mockProjectRepository;
    private readonly CreateProjectCommandHandler _handler;

    public CreateProjectCommandHandlerTest()
    {
        _mockProjectRepository = Substitute.For<IProjectRepository>();
        _handler = new CreateProjectCommandHandler(_mockProjectRepository);
    }

    // T1: SUT - component what we're testing
    // T2: Scenario - what we're testing
    // T3: Expected outcome - what we expect the outcome should be

    // public void T1_T2_T3() {}
    [Theory]
    [MemberData(nameof(ValidCreateProjectCommands))]
    public async Task HandleCreateProjectCommand_WhenProjectIsValid_ShouldCreateAndReturnProject(CreateProjectCommand createProjectCommand )
    {
        // Arrange
        // The hold of a valid project
        // var createProjectCommand = CreateProjectCommandUtils.CreateCommand();

        // Act
        // Invoke the handler
        var result = await _handler.Handle(createProjectCommand, default);

        // Assert
        // 1. Validate correct project created based on command
        // 2. Project added to repository
        result.IsFailed.Should().BeFalse();
        result.Value.ValidateCreatedFrom(createProjectCommand);
        _mockProjectRepository.Received().Add(result.Value);
        _mockProjectRepository.Received(1).Add(result.Value);
    }

    public static IEnumerable<object[]> ValidCreateProjectCommands()
    {
        yield return new[] { CreateProjectCommandUtils.CreateCommand()};
        yield return new[] { CreateProjectCommandUtils.CreateCommand(
            tasks: CreateProjectCommandUtils.CreateTasksCommand(tasksCount: 3)
        )};
        yield return new[] { CreateProjectCommandUtils.CreateCommand(
            tasks: CreateProjectCommandUtils.CreateTasksCommand(tasksCount: 5)
        )};
    }
}