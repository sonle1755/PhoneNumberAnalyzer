using Microsoft.EntityFrameworkCore;
using PhoneNumberAnalyzer.Data.Entities;
using PhoneNumberAnalyzer.Data.Interfaces;

namespace PhoneNumberAnalyzer.Data.Repositories;

public class PatternRepository(AppDbContext db) : IPatternRepository
{
    private readonly AppDbContext _db = db;

    public async Task<Pattern> AddAsync(string name,
                                        string description,
                                        string regexString)
    {
        var pattern = Pattern.Create(name, description, regexString);
        await _db.Patterns.AddAsync(pattern);
        await _db.SaveChangesAsync();
        return pattern;
    }

    public async Task<IEnumerable<Pattern>> GetAllAsync()
    {
        return await _db.Patterns.AsNoTracking().ToListAsync();
    }

    public async Task<Pattern?> GetByIdAsync(int id)
    {
        return await _db.Patterns.AsNoTracking()
                                 .Where(u => u.Id == id)
                                 .FirstOrDefaultAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await _db.Patterns.Where(p => p.Id == id).ExecuteDeleteAsync();
        await _db.SaveChangesAsync();
    }
}
