using GenericApp.Domain.Interfaces.Services.Base;

namespace GenericApp.Domain.Interfaces.Services
{
    public interface IMailService : IBaseService
    {
        void SendNewPassword(string newPassword, string destination, string name);
    }
}
