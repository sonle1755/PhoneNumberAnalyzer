using PhoneNumberAnalyzer.Business.Dtos;

namespace PhoneNumberAnalyzer.Business.Interfaces;

public interface IUserService
{
  public Task<IEnumerable<UserDto>> GetAllAsync();
}
