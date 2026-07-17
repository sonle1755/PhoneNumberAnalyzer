using Microsoft.AspNetCore.Mvc;
using PhoneNumberAnalyzer.Api.Dtos;
using PhoneNumberAnalyzer.Business.Dtos;
using PhoneNumberAnalyzer.Business.Interfaces;

namespace PhoneNumberAnalyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyzeController(IPatternAnalyzerService patternAnalyzerService) : ControllerBase
{
    private readonly IPatternAnalyzerService _patternAnalyzerService = patternAnalyzerService;

    [HttpPost]
    public async Task<PatternAnalyzeResultDto> PostAsync([FromBody] AnalyzeRequestDto param)
    {
        return await _patternAnalyzerService.AnalyzeAsync(param.PhoneNumbers.First());
    }
}
