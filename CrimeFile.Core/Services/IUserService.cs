﻿using CrimeFile.Core.Common;
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
        Task<RegisterResultDTO> Register(RegisterDTO registerDTO);
        Task<int> Edit(User user);
        Task <ServiceResult<string>> VerifyEmail(EmailVerificationDTO emailVerification);
        Task<ServiceResult<GenerateCodeDTO>> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO);
        Task<ServiceResult<int>> ResetPassword(ResetPasswordDTO resetPasswordDTO);
        //Task<int> Delete(int id);
    }
}