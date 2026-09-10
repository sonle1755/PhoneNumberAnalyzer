using System.Diagnostics.CodeAnalysis;
using PhoneNumberAnalyzer.Business.Dtos;
using PhoneNumberAnalyzer.Business.Interfaces;
using PhoneNumberAnalyzer.Data.Interfaces;

namespace PhoneNumberAnalyzer.Business.Services;

public sealed class PatternTemplateEvaluationService(
    [NotNull] IPatternTemplateRepository templateRepository,
    [NotNull] IPatternSpecificationCache specCache) : IPatternTemplateEvaluationService
{
    private readonly IPatternTemplateRepository _templateRepository = templateRepository;
    private readonly IPatternSpecificationCache _specCache = specCache;

    public async Task<IReadOnlyCollection<PatternTemplateMatch>> EvaluateAsync(string digits, int? userId)
    {
        var templates = userId is not null
            ? await _templateRepository.GetVisibleToUserAsync(userId.Value)
            : await _templateRepository.GetPublicAsync();

        var matches = new List<PatternTemplateMatch>();

        foreach (var template in templates)
        {
            PatternRuleGroupTreeBuilder.AttachChildGroups(template.RuleGroups);

            var spec = _specCache.GetOrBuild(template); // cached after first build, until UpdatedAt changes

            if (spec.IsSatisfiedBy(digits))
            {
                matches.Add(new PatternTemplateMatch(template.Id, template.Name));
            }
        }

        return matches;
    }
}
