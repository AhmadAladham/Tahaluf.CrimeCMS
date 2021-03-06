using CrimeFile.Core.DTOs;
using CrimeFile.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrimeFile.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<int> EditUser(EditUserDTO editUserDTO);
        Task<User> GetById(int id);
        Task<RegisterResultDTO> Register(RegisterDTO registerDTO);
        Task<string> VerifyEmail(EmailVerificationDTO emailVerification);
        Task<string> SignIn(SignInDTO signInDTO);
        Task<GenerateCodeDTO> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO);
        Task<int> ResetPassword(ResetPasswordDTO resetPasswordDTO);
        Task<int> ChangePassword(ChangePasswordDTO changePasswordDTO);
        Task<PagedList<UserDTO>> GetAllPaged(UserParameters userParameters);
        Task<PagedList<UserDTO>> Search(SearchUserDTO searchUserDTO);
        Task<string> RefreshToken(int userId);
    }
}
