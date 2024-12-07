using MedicalApp.Identity;

namespace MedicalApp.Service
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserApp user);
    }
}
