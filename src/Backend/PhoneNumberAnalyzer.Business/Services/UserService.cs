using PhoneNumberAnalyzer.Business.Dtos;
using PhoneNumberAnalyzer.Business.Interfaces;
using PhoneNumberAnalyzer.Data.Interfaces;

namespace PhoneNumberAnalyzer.Business.Services;

public class UserService : IUserService
{
  private readonly IUserRepository _userRepository;

  public UserService(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<IEnumerable<UserDto>> GetAllAsync()
  {
    var users = await _userRepository.GetAllAsync();
    return users.Select(u => new UserDto(id: u.Id,
                                         firstName: u.FirstName,
                                         lastName: u.LastName,
                                         username: u.Username,
                                         avatarUrl: u.AvatarUrl,
                                         email: u.Email,
                                         emailVerified: u.EmailVerified,
                                         lastLoginAt: u.LastLoginAt,
                                         deletedAt: u.DeletedAt));
  }
}
