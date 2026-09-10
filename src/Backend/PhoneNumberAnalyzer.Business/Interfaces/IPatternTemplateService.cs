using System.Collections.Immutable;
using PhoneNumberAnalyzer.Business.Dtos;

namespace PhoneNumberAnalyzer.Business.Interfaces;

public interface IPatternTemplateService
{
    Task<ImmutableArray<PatternTemplateDetail>> GetVisibleToUserAsync(int userId);

    Task<ImmutableArray<PatternTemplateDetail>> GetPublicAsync();

    Task<PatternTemplateDetail?> GetByIdAsync(Guid id);

    Task<PatternTemplateDetail> AddAsync(PatternTemplateCreateCommand command);

    Task UpdateAsync(Guid id, PatternTemplateUpdateCommand command);

    Task DisableAsync(Guid id);

    Task EnableAsync(Guid id);
}
