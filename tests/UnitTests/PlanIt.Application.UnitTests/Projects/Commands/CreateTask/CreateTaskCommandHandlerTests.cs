using FluentAssertions;
using NSubstitute;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Application.UnitTests.Projects.Commands.TestUtils;
using PlanIt.Application.UnitTests.TestUtils.Tasks;
using PlanIt.Domain.ProjectAggregate;
using PlanIt.Application.UnitTests.TestUtils.Constants;
using PlanIt.Domain.ProjectAggregate.Entities;
using PlanIt.Domain.Project.ValueObjects;
using PlanIt.Application.Tasks.Commands.CreateTask;

namespace PlanIt.Application.UnitTests.Projects.Commands.CreateTask;

public class CreateTaskCommandHandlerTests
{
    private readonly IProjectRepository _mockProjectRepository;
    private readonly CreateTaskCommandHandler _handler;

    public CreateTaskCommandHandlerTests()
    {
        _mockProjectRepository = Substitute.For<IProjectRepository>();
        _handler = new CreateTaskCommandHandler(_mockProjectRepository);
    }

    [Fact]
    public async Task HandleCreateTaskCommand_WhenTaskIsValid_ShouldCreateAndReturnTask()
    {
        // Arrange
        var createTaskCommand = CreateTaskCommandUtils.CreateCommand();
        var mockProject = Project.Create(
            Constants.Project.Name,
            Constants.Project.Description,
            ProjectOwnerId.Create(Constants.ProjectOwner.Id.Value),
            new List<ProjectTask>()
        );

        _mockProjectRepository.GetAsync(Arg.Any<string>()).Returns(mockProject);

        // Act
        var result = await _handler.Handle(createTaskCommand, default);

        // Assert
        result.IsFailed.Should().BeFalse();
        result.Value.ValidteCreatedFrom(createTaskCommand);

        await _mockProjectRepository.Received().UpdateAsync();
        await _mockProjectRepository.Received(1).UpdateAsync();
    }
}