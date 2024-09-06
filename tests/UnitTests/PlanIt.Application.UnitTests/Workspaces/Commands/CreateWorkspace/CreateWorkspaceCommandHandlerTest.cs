using FluentAssertions;
using NSubstitute;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Application.UnitTests.TestUtils.Projects;
using PlanIt.Application.UnitTests.TestUtils.Workspaces;
using PlanIt.Application.UnitTests.Workspaces.TestUtils;
using PlanIt.Application.Workspaces.Commands.CreateWorkspace;
using PlanIt.Application.UnitTests.TestUtils.Constants;

namespace PlanIt.Application.UnitTests.Workspaces.Commands.CreateWorkspace;

public class CreateProjectCommandHandlerTest
{
    private readonly IWorkspaceRepository _mockWorkspaceRepository;
    private readonly IUserContext _userContext;
    private readonly CreateWorkspaceCommandHandler _handler;

    public CreateProjectCommandHandlerTest()
    {
        _mockWorkspaceRepository = Substitute.For<IWorkspaceRepository>();
        _userContext = Substitute.For<IUserContext>();

        _handler = new CreateWorkspaceCommandHandler(_userContext, _mockWorkspaceRepository);
    }

    [Fact]
    public async Task HandleCreateWorkspaceCommand_WhenWorkspaceIsValid_ShouldCreateAndReturnWorkspace()
    {
        // Arrange
        _userContext.TryGetUserId().Returns(Constants.User.Id);
        var createWorkspaceCommand = CreateWorkspaceCommandUtils.CreateCommand();

        // Act
        var result = await _handler.Handle(createWorkspaceCommand, default);

        // Assert
        result.IsFailed.Should().BeFalse();
        result.Value.ValidateCreatedFrom(createWorkspaceCommand);
        await _mockWorkspaceRepository.Received().AddAsync(result.Value);
        await _mockWorkspaceRepository.Received(1).AddAsync(result.Value);
    }
}