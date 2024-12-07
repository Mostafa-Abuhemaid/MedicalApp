namespace MedicalApp.Service
{
    public interface IMailService
    {
        Task SendEmailAsync(string mailTo, string subject, string body, IList<IFormFile>? attachedFiles = null);
    }
}
