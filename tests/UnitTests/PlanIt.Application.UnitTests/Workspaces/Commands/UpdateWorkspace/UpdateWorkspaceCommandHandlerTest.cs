using NSubstitute;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Application.Workspaces.Commands.UpdateWorkspace;
using PlanIt.Application.UnitTests.Workspaces.TestUtils;
using FluentAssertions;
using PlanIt.Application.UnitTests.TestUtils.Workspaces;
using PlanIt.Application.UnitTests.TestUtils.Constants;
using PlanIt.Domain.WorkspaceAggregate.ValueObjects;

namespace PlanIt.Application.UnitTests.Workspaces.Commands.UpdateWorkspace;

public class UpdateWorkspaceCommandHandlerTest
{
    private readonly IWorkspaceRepository _mockWorkspaceRepository;
    private readonly IUserContext _userContext;
    private readonly UpdateWorkspaceCommandHandler _handler;

    public UpdateWorkspaceCommandHandlerTest()
    {
        _mockWorkspaceRepository = Substitute.For<IWorkspaceRepository>();
        _userContext = Substitute.For<IUserContext>();

        _handler = new UpdateWorkspaceCommandHandler( _userContext, _mockWorkspaceRepository);
    }

    [Theory]
    [MemberData(nameof(ValidUpdateWorkspaceCommands))]
    public async Task HandleUpdateWorkspaceCommand_WhenWorkspaceIsValid_ShouldUpdateAndReturnWorkspace(UpdateWorkspaceCommand updateProjectCommand)
    {
        // Arrange
        var mockedWorkspace = UpdateWorkspaceCommandUtils.GetMockedWorkspace();
        _userContext.TryGetUserId().Returns(Constants.User.Id);
        _mockWorkspaceRepository.GetAsync(Arg.Any<WorkspaceId>()).Returns(mockedWorkspace);

        // Act
        var result = await _handler.Handle(updateProjectCommand, default);

        // Assert
        result.IsFailed.Should().BeFalse();
        result.Value.ValidateUpdatedFrom(updateProjectCommand);
        await _mockWorkspaceRepository.Received().SaveChangesAsync();
        await _mockWorkspaceRepository.Received(1).SaveChangesAsync();
    }

    public static IEnumerable<object[]> ValidUpdateWorkspaceCommands()
    {
        yield return new[] { UpdateWorkspaceCommandUtils.CreateCommand() };
    }
}