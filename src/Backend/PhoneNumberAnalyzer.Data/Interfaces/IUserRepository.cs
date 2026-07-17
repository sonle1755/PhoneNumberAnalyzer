using PhoneNumberAnalyzer.Data.Entities;

namespace PhoneNumberAnalyzer.Data.Interfaces;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetAllAsync();

    public Task<User?> GetByUserNameAsync(string username);

    public Task<User?> GetByIdAsync(int id);

    public Task<User> AddAsync(string firstName,
                               string lastName,
                               string userName,
                               string? email,
                               string? avatarUrl);
}
