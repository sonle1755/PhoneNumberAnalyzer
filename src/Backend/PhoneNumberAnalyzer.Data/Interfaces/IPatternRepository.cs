using PhoneNumberAnalyzer.Data.Entities;

namespace PhoneNumberAnalyzer.Data.Interfaces;

public interface IPatternRepository
{
    public Task<IEnumerable<Pattern>> GetAllAsync();

    public Task<Pattern?> GetByIdAsync(int id);

    public Task<Pattern> AddAsync(string name,
                                  string description,
                                  string regexString);

    public Task DeleteAsync(int id);
}
