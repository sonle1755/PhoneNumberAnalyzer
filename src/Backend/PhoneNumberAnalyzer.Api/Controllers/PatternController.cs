using System.Collections.Immutable;
using Microsoft.AspNetCore.Mvc;
using PhoneNumberAnalyzer.Business.Dtos;
using PhoneNumberAnalyzer.Business.Interfaces;

namespace PhoneNumberAnalyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatternController(IPatternService patternService) : ControllerBase
{
    private readonly IPatternService _patternService = patternService;

    [HttpGet]
    public async Task<ImmutableArray<PatternDto>> GetAllAsync()
    {
        return await _patternService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<PatternDto?> GetPatternByIdAsync(int id)
    {
        return await _patternService.GetPatternById(id);
    }

    [HttpPost]
    public async Task<PatternDto> PostAsync([FromBody] PatternDto dto)
    {
        var newPattern = await _patternService.AddAsync(dto.Name, dto.Description, dto.RegexString);
        return newPattern;
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _patternService.DeleteAsync(id);
        return Ok();
    }
}
