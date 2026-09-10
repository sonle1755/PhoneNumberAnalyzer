using System.Collections.Concurrent;
using PhoneNumberAnalyzer.Business.Interfaces;
using PhoneNumberAnalyzer.Business.Specifications;
using PhoneNumberAnalyzer.Data.Entities;

namespace PhoneNumberAnalyzer.Business.Services;

/// <summary>
/// Caches the built ISpecification tree per template, keyed by template ID +
/// UpdatedAt. If a template is edited, UpdatedAt changes, the cache key no
/// longer matches, and the spec is rebuilt automatically - no explicit
/// invalidation call needed anywhere else in the codebase.
/// </summary>
public sealed class PatternSpecificationCache : IPatternSpecificationCache
{
    private sealed record CacheEntry(DateTimeOffset? Version, ISpecification Specification);

    private readonly ConcurrentDictionary<Guid, CacheEntry> _cache = new();

    public ISpecification GetOrBuild(PatternTemplate template)
    {
        var version = template.UpdatedAt ?? template.CreatedAt;

        if (_cache.TryGetValue(template.Id, out var entry) && entry.Version == version)
        {
            return entry.Specification;
        }

        var rootGroup = template.RuleGroups.FirstOrDefault(g => g.ParentGroup == null && g.Level == 0)
            ?? throw new InvalidOperationException("Cannot find Pattern Template's Root group");
        var spec = PatternRuleGroupSpecificationBuilder.Build(rootGroup);
        _cache[template.Id] = new CacheEntry(version, spec);

        return spec;
    }
}
