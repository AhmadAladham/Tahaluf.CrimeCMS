using CrimeFile.Core.Common;
using CrimeFile.Core.DTOs;
using CrimeFile.Core.Entities;
using System.Threading.Tasks;

namespace CrimeFile.Core.Services
{
    public interface IUserService : IBaseService
    {
        //Task<User> GetById(int id);
        //Task<List<User>> GetAll();
        //Task<int> Create(User user);
        Task<ServiceResult<PagedList<UserDTO>>> GetAllPaged(UserParameters userParameters);
        Task<ServiceResult<string>> SignIn(SignInDTO signInDTO);
        Task<ServiceResult<string>> RefreshToken(int userId);
        Task<ServiceResult<RegisterResultDTO>> Register(RegisterDTO registerDTO);
        Task<ServiceResult<int>> Edit(EditUserDTO editUserDTO);
        Task <ServiceResult<string>> VerifyEmail(EmailVerificationDTO emailVerification);
        Task<ServiceResult<GenerateCodeDTO>> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO);
        Task<ServiceResult<int>> ResetPassword(ResetPasswordDTO resetPasswordDTO);
        Task<ServiceResult<int>> ChangePassword(ChangePasswordDTO changePasswordDTO);
        Task<ServiceResult<int>> Delete(int id);
        Task<ServiceResult<PagedList<UserDTO>>> Search(SearchUserDTO searchUserDTO);
    }
}
