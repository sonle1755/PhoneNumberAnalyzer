using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using PhoneNumberAnalyzer.Business.Dtos;
using PhoneNumberAnalyzer.Business.Interfaces;

namespace PhoneNumberAnalyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class PhoneAnalysisController : ControllerBase
{
    private readonly IPhoneNumberExtractionService _phoneNumberExtractionService;
    private readonly IPatternTemplateEvaluationService _evaluationService;

    public PhoneAnalysisController([NotNull] IPatternTemplateEvaluationService evaluationService,
                                   [NotNull] IPhoneNumberExtractionService phoneNumberExtractionService)
    {
        _phoneNumberExtractionService = phoneNumberExtractionService ?? throw new ArgumentNullException(nameof(phoneNumberExtractionService));
        _evaluationService = evaluationService ?? throw new ArgumentNullException(nameof(evaluationService));
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<PhoneAnalysisResult>>> AnalyzeAsync([FromBody] string inputText)
    {
        List<PhoneAnalysisResult> result = [];
        foreach (ExtractedPhoneNumber phoneNumber in _phoneNumberExtractionService.ExtractFromText(inputText))
        {
            var matches = await _evaluationService.EvaluateAsync(phoneNumber.NormalizedDigits, null);
            var analysisResult = new PhoneAnalysisResult(OriginalInput: phoneNumber.RawMatchedText,
                                                         NormalizedDigits: phoneNumber.NormalizedDigits,
                                                         MatchedPatterns: matches);
            result.Add(analysisResult);
        }

        return Ok(result);
    }

}
