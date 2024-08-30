# Domain Models

## Project

```csharp
class Project
{
    Project Create();
    void AddTask(ProjectTask task);
    void UpdateTask(ProjectTask task);
    void RemoveTask(ProjectTask task);
    void ChangeName(string newName);
    void ChangeDescription(string newDescription);
}
```

```json
    "id" : "0000-0000",
    "name": "Great project",
    "description": "A project that will be perfect",
    "createdDateTime": "2020-01-01T00:00:00.000000Z",
    "updatedDateTime": "2020-01-01T00:00:00.000000Z", 
    "projectOwnerId": "0000-0000",
    "tasks": {
        [
            {
                "taskOwnerId": "0000-0000",
                "taskCommentIds": ["0000-0000", "0000-0001", "0000-0002"],
                "projectWorkersId": ["0000-0000", "0000-0001", "0000-0002"]
            }
        ]
    }
```