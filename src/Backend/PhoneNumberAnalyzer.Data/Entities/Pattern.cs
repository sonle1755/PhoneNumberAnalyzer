namespace PhoneNumberAnalyzer.Data.Entities;

public class Pattern
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string RegexString { get; set; } = null!;

    public DateTimeOffset? DeletedAt { get; set; }

    public static Pattern Create(string name, string description, string regexString)
    {
        return new Pattern
        {
            Id = 0,
            Name = name,
            Description = description,
            RegexString = regexString,
            DeletedAt = null
        };
    }
}
