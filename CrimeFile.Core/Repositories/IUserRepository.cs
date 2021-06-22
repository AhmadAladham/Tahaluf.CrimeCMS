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
        Task<User> GetById(int id);
        Task<RegisterResultDTO> Register(RegisterDTO registerDTO);
        Task<string> VerifyEmail(EmailVerificationDTO emailVerification);
        Task<string> SignIn(SignInDTO signInDTO);
        Task<GenerateCodeDTO> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO);
        Task<int> ResetPassword(ResetPasswordDTO resetPasswordDTO);
        Task<PagedList<UserDTO>> GetAllPaged(UserParameters userParameters);
    }
}
