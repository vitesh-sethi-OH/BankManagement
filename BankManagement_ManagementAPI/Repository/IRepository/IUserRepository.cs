using BankManagement_ManagementAPI.Models;
using BankManagement_ManagementAPI.Models.DTO;

namespace BankManagement_ManagementAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO);
    }
}
