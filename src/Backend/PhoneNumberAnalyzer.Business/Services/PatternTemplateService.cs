using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using PhoneNumberAnalyzer.Business.Dtos;
using PhoneNumberAnalyzer.Business.Interfaces;
using PhoneNumberAnalyzer.Data.Entities;
using PhoneNumberAnalyzer.Data.Interfaces;

namespace PhoneNumberAnalyzer.Business.Services;

public class PatternTemplateService([NotNull] IPatternTemplateRepository patternTemplateRepository)
    : IPatternTemplateService
{
    private readonly IPatternTemplateRepository _patternTemplateRepository = patternTemplateRepository;

    public async Task<PatternTemplateDetail?> GetByIdAsync(Guid patternTemplateId)
    {
        var patternTemplate = await _patternTemplateRepository.GetByIdAsync(patternTemplateId);
        return patternTemplate != null ? MapToDto(patternTemplate) : null;
    }

    public async Task<PatternTemplateDetail> AddAsync(PatternTemplateCreateCommand command)
    {
        if (command.Name == null)
            throw new ArgumentNullException(nameof(command.Name), "PatternTempalte Name cannot be null!");

        if (command.Description == null)
        {
            throw new ArgumentNullException(nameof(command.Description),
                                            "PatternTemplate Description cannot be null!");
        }

        var patternTemplate = await _patternTemplateRepository.AddAsync(command.Name, command.Description);
        var rootGroup = MapToEntity(command: command.PatternRuleGroup,
                                    patternTemplate: patternTemplate,
                                    nestedLevel: 0);
        patternTemplate.RuleGroups.Add(rootGroup);
        await _patternTemplateRepository.SaveChangesAsync();
        return new PatternTemplateDetail(Id: patternTemplate.Id,
                                         Name: patternTemplate.Name,
                                         Description: patternTemplate.Description,
                                         PatternRuleGroup: MaptoDto(rootGroup));
    }

    private static PatternRuleGroup MapToEntity(PatternRuleGroupCreateCommand command,
                                                PatternTemplate patternTemplate,
                                                int nestedLevel)
    {
        if (nestedLevel > 5)
        {
            throw new ArgumentOutOfRangeException(nameof(nestedLevel),
                                                  "Pattern Template Group nested level exceeded 5!");
        }

        return (command.Rules.Length, command.ChildGroups.Length) switch
        {
            ( > 0, > 0) => throw new InvalidOperationException("Pattern Rule Group cannot contain both Rules & Child group!"),
            (0, 0) => throw new InvalidOperationException("Pattern Rule Group must contain either Rules or Child group!"),
            _ => new PatternRuleGroup
            {
                Name = command.Name,
                Level = nestedLevel,
                RuleOperator = command.RuleOperator,
                PatternTemplate = patternTemplate,
                Rules = [.. command.Rules.Select(MapToEntity)],
                ChildGroups = [.. command.ChildGroups.Select(g => MapToEntity(command: g,
                                                                              patternTemplate: patternTemplate,
                                                                              nestedLevel: nestedLevel + 1))]
            },
        };
    }

    private static PatternRule MapToEntity(PatternRuleCreateCommand command)
    {
        return new PatternRule
        {
            Name = command.Name,
            Length = command.Length,
            RuleType = command.RuleType
        };
    }

    public static PatternTemplateDetail MapToDto(PatternTemplate entity)
    {
        var groups = entity.RuleGroups.Where(g => g.ParentId == null && g.Level == 0);
        if (groups.Count() != 1)
        {
            throw new InvalidDataException("PatternTemplate must have only 1 root group");
        }
        return new PatternTemplateDetail(Id: entity.Id,
                                         Name: entity.Name,
                                         Description: entity.Description,
                                         PatternRuleGroup: MaptoDto(groups.First()));
    }

    private static PatternRuleGroupDetail MaptoDto(PatternRuleGroup entity)
    {
        return new PatternRuleGroupDetail(Id: entity.Id,
                                          Name: entity.Name,
                                          Level: entity.Level,
                                          IsRoot: entity.Level == 0 && entity.ParentId == null,
                                          RuleOperator: entity.RuleOperator,
                                          Rules: [.. entity.Rules.Select(MaptoDto)],
                                          ChildGroups: [.. entity.ChildGroups.Select(MaptoDto)]);
    }

    private static PatternRuleDetail MaptoDto(PatternRule entity)
    {
        return new PatternRuleDetail(Id: entity.Id,
                                     Name: entity.Name,
                                     RuleType: entity.RuleType,
                                     Length: entity.Length,
                                     Values: [.. entity.Values],
                                     TargetPositions: [.. entity.TargetPositions],
                                     ReferencePosition: entity.ReferencePosition);
    }

    public Task UpdateAsync(Guid id, PatternTemplateUpdateCommand command)
    {
        throw new NotImplementedException();
    }

    public Task DisableAsync(Guid patternTemplateId)
    {
        throw new NotImplementedException();
    }

    public Task EnableAsync(Guid patternTemplateId)
    {
        throw new NotImplementedException();
    }

    public Task<ImmutableArray<PatternTemplateDetail>> GetVisibleToUserAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<ImmutableArray<PatternTemplateDetail>> GetPublicAsync()
    {
        var patternTemplates = await _patternTemplateRepository.GetPublicAsync();
        return [.. patternTemplates.Select(MapToDto)];
    }
}
