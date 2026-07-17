using Microsoft.EntityFrameworkCore;
using PhoneNumberAnalyzer.Data.Entities;
using PhoneNumberAnalyzer.Data.Interfaces;

namespace PhoneNumberAnalyzer.Data.Repositories;

public class UserRepository(AppDbContext db) : IUserRepository {
    private readonly AppDbContext _db = db;

    public async Task<User> AddAsync(string firstName,
                                     string lastName,
                                     string userName,
                                     string? email,
                                     string? avatarUrl) {
        var user = User.Create(firstName, lastName, userName, email, avatarUrl);
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<IEnumerable<User>> GetAllAsync() {
        return await _db.Users.AsNoTracking().ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id) {
        return await _db.Users.AsNoTracking()
                              .Where(u => u.Id == id)
                              .FirstOrDefaultAsync();
    }

    public async Task<User?> GetByUserNameAsync(string username) {
        return await _db.Users.AsNoTracking()
                              .Where(u => u.Username == username)
                              .FirstOrDefaultAsync();
    }
}
