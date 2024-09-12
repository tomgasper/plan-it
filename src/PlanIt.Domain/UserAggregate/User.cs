namespace PlanIt.Domain.UserAggregate;
public sealed class User
{
    public Guid Id {get; private set; }
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string AvatarUrl {get; private set;}

    private User(
        Guid id,
        string firstName,
        string lastName,
        string avatarUrl
    ) 
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        AvatarUrl = avatarUrl;
    }

    #pragma warning disable CS8618
    private User()
    {

    }
    #pragma warning restore CS8618

    public static User Create(
        Guid id,
        string firstName,
        string lastName,
        string avatarUrl = ""
        )
    {
        var user = new User( 
            id,
            firstName,
            lastName,
            avatarUrl
        );

        return user;
    }

    public void ChangeName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}