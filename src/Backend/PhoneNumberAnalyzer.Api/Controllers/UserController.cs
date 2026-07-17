using Microsoft.AspNetCore.Mvc;
using PhoneNumberAnalyzer.Business.Dtos;
using PhoneNumberAnalyzer.Business.Interfaces;

namespace PhoneNumberAnalyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
  private readonly IUserService _userService;

  public UserController(IUserService userService)
  {
    _userService = userService;
  }

  [HttpGet]
  public async Task<IEnumerable<UserDto>> GetAllAsync()
  {
    return await _userService.GetAllAsync();
  }
}
