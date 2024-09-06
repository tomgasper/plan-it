namespace PlanIt.Domain.UserAggregate;
public sealed class User
{
    public Guid Id {get; private set; }
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;

    private User(
        Guid id,
        string firstName,
        string lastName
    ) 
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }

    #pragma warning disable CS8618
    private User()
    {

    }
    #pragma warning restore CS8618

    public static User Create(
        Guid id,
        string firstName,
        string lastName
        )
    {
        var user = new User( 
            id,
            firstName,
            lastName
        );

        return user;
    }

    public void ChangeName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}