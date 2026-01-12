namespace Noser_Fitness.Domain;

public class Member : Entity
{
    public string Firstname { get; init; } = string.Empty;
    public string Lastname { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
}
