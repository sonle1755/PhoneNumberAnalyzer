using PhoneNumberAnalyzer.Business.Dtos;

namespace PhoneNumberAnalyzer.Business.Interfaces;

public interface IPatternTemplateEvaluationService
{
    /// <summary>
    /// Evaluates a normalized digit string against all templates visible to the
    /// given user (owned + public), returning which ones matched.
    /// </summary>
    Task<IReadOnlyCollection<PatternTemplateMatch>> EvaluateAsync(string digits, int? userId);
}
