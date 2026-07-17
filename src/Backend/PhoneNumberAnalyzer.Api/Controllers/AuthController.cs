using Microsoft.AspNetCore.Mvc;
using PhoneNumberAnalyzer.Api.Dtos;
using PhoneNumberAnalyzer.Business.Interfaces;

namespace PhoneNumberAnalyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthenticationService registrationService) : ControllerBase {
    private readonly IAuthenticationService _authenticationService = registrationService;

    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDto dto) {
        await _authenticationService.Register(dto.FirstName,
                                            dto.LastName,
                                            dto.UserName,
                                            dto.Password,
                                            dto.Email,
                                            dto.AvatarUrl);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto) {
        await _authenticationService.Login(dto.Username, dto.Password);
        return Ok();
    }
}
