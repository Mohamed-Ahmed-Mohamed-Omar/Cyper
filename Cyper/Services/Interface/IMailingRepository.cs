namespace Cyper.Services.Interface
{
    public interface IMailingRepository
    {
        Task<string> SendingMail(string mailTo, string Message, string? reason);
    }
}
