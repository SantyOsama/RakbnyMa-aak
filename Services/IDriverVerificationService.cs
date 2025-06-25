    namespace RakbnyMa_aak.Services
{
    public interface IDriverVerificationService
    {
        Task<bool> MatchFaceAsync(string selfieUrl, string nationalIdUrl);
    }
}
