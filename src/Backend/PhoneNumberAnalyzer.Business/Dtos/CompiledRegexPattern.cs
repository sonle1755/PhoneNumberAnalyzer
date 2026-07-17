using System.Text.RegularExpressions;

namespace PhoneNumberAnalyzer.Business.Dtos;

public class CompiledRegexPatterns(int id, string name, string regexPattern)
{
    public int Id { get; set; } = id;

    public string Name { get; set; } = name ?? throw new ArgumentNullException(nameof(name));

    public Regex RegexPattern { get; set; } = new Regex(regexPattern, RegexOptions.Compiled);
}
