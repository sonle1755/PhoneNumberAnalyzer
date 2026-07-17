namespace PhoneNumberAnalyzer.Business.Interfaces;

public interface IAuthenticationService
{
  public Task Login(string username, string password);

  public Task Register(string firstName,
                       string lastName,
                       string username,
                       string password,
                       string? email,
                       string? avatarUrl);
}
