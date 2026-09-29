using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PhoneNumberAnalyzer.Api.Dtos;
using PhoneNumberAnalyzer.Business.Configurations;
using PhoneNumberAnalyzer.Business.Dtos;
using PhoneNumberAnalyzer.Business.Interfaces;

namespace PhoneNumberAnalyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IOptions<JwtOptions> jwtOptions,
                            ITokenService tokenService,
                            IAuthenticationService registrationService,
                            IUserService userService)
    : ControllerBase
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IAuthenticationService _authenticationService = registrationService;

    private readonly IUserService _userService = userService;

    [HttpPost("[action]")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
    {
        await _authenticationService.Register(request.FirstName,
                                              request.LastName,
                                              request.Username,
                                              request.Password,
                                              request.Email,
                                              request.AvatarUrl);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<LoginResponse>> LoginAsync([FromBody] LoginRequest request)
    {
        var user = await _authenticationService.VerifyUserAsync(request.Username, request.Password);
        if (user is null)
        {
            return Unauthorized(new { message = "Invalid username or password!" });
        }

        var token = _tokenService.GenerateAccessToken(user.Id);
        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user.Id);

        SetRefreshTokenCookie(refreshToken);
        var expiresInSeconds = _jwtOptions.AccessTokenExpirationMinutes * 60;
        return Ok(new LoginResponse(token, expiresInSeconds));
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<LoginResponse>> RefreshAsync()
    {
        var rawRefreshToken = Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(rawRefreshToken))
            return Unauthorized(new { message = "No refresh token provided" });

        var result = await _tokenService.RefreshAsync(rawRefreshToken);
        if (result is null)
            return Unauthorized(new { message = "Invalid or expired refresh token" });

        SetRefreshTokenCookie(result.Value.refreshToken);

        var expiresInSeconds = _jwtOptions.AccessTokenExpirationMinutes * 60;
        return Ok(new LoginResponse(result.Value.accessToken, expiresInSeconds));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> LogoutAsync()
    {
        var rawRefreshToken = Request.Cookies["refreshToken"];
        if (!string.IsNullOrEmpty(rawRefreshToken))
        {
            await _tokenService.RevokeAsync(rawRefreshToken);
            Response.Cookies.Delete("refreshToken", new CookieOptions { Path = "/api/Auth" });
        }

        return Ok();
    }

    [Authorize]
    [HttpGet("[action]")]
    public async Task<ActionResult<UserDetail>> Me()
    {
        var userIdString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (userIdString is null || !Guid.TryParse(userIdString, out var userId))
        {
            return Unauthorized();
        }

        var user = await _userService.GetByIdAsync(userId);
        if (user is null)
        {
            return NotFound();
        }

        return new UserDetail(id: user.Id,
                              firstName: user.FirstName,
                              lastName: user.LastName,
                              username: user.Username,
                              avatarUrl: user.AvatarUrl,
                              emailVerified: user.EmailVerified,
                              email: user.Email,
                              deletedAt: user.DeletedAt,
                              lastLoginAt: user.LastLoginAt);
    }

    private void SetRefreshTokenCookie(string token)
    {
        Response.Cookies.Append("refreshToken", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(_jwtOptions.RefreshTokenExpirationDays),
            Path = "/api/Auth"
        });
    }
}
