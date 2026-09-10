using PhoneNumberAnalyzer.Business.Dtos;

namespace PhoneNumberAnalyzer.Business.Interfaces;

public interface IPhoneNumberExtractionService
{
    IReadOnlyCollection<ExtractedPhoneNumber> ExtractFromText(string text);

}
