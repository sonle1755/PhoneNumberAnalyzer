using System.Collections.Immutable;
using Microsoft.AspNetCore.Mvc;
using PhoneNumberAnalyzer.Business.Dtos;
using PhoneNumberAnalyzer.Business.Interfaces;

namespace PhoneNumberAnalyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatternTemplateController(IPatternTemplateService patternTemplateService) : ControllerBase
{
    private readonly IPatternTemplateService _patternTemplateService = patternTemplateService;

    [HttpGet]
    public async Task<ImmutableArray<PatternTemplateDetail>> GetAllAsync()
    {
        return await _patternTemplateService.GetPublicAsync();
    }

    [HttpGet("{id}")]
    public async Task<PatternTemplateDetail?> GetDetailByIdAsync(Guid id)
    {
        return await _patternTemplateService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<PatternTemplateDetail> PostAsync([FromBody] PatternTemplateCreateCommand command)
    {
        return await _patternTemplateService.AddAsync(command);
    }

    [HttpPut("{id}")]
    public async Task PutAsync(Guid id, [FromBody] PatternTemplateUpdateCommand command)
    {
        await _patternTemplateService.UpdateAsync(id, command);
    }

    [HttpPut("[action]/{id}")]
    public async Task DisableAsync(Guid id)
    {
        await _patternTemplateService.DisableAsync(id);
    }

    [HttpPut("[action]/{id}")]
    public async Task EnableAsync(Guid id)
    {
        await _patternTemplateService.EnableAsync(id);
    }
}
