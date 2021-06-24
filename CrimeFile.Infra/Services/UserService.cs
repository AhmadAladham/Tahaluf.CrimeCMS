using CrimeFile.Core.Common;
using CrimeFile.Core.DTOs;
using CrimeFile.Core.Entities;
using CrimeFile.Core.Repositories;
using CrimeFile.Core.Security;
using CrimeFile.Core.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CrimeFile.Infra.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfigManager _configManager;
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IConfigManager configManager) : base(unitOfWork)
        {
            _configManager = configManager;
            _userRepository = userRepository;
        }

        public async Task<ServiceResult<PagedList<UserDTO>>> GetAllPaged(UserParameters userParameters)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<PagedList<UserDTO>>(ResultCode.BadRequest);
                try
                {
                    var result = await _userRepository.GetAllPaged(userParameters);
                    serviceResult.Data = result;
                    serviceResult.Status = ResultCode.Ok;
                }
                catch (Exception ex)
                {

                }

                return serviceResult;
            });
        }

        public async Task<ServiceResult<string>> VerifyEmail(EmailVerificationDTO emailVerification)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<string>(ResultCode.BadRequest);
                try
                {
                    var result = await _userRepository.VerifyEmail(emailVerification);
                    serviceResult.Data = result;
                    serviceResult.Status = ResultCode.Created;
                }
                catch (Exception ex)
                {
                    serviceResult.Data = ex.Message;
                    serviceResult.Status = ResultCode.Forbidden;
                    return serviceResult;
                }


                return serviceResult;
            });
        }

        public async Task<RegisterResultDTO> Register(RegisterDTO registerDTO)
        {
            return await _userRepository.Register(registerDTO);
        }

        public async Task<ServiceResult<string>> SignIn(SignInDTO signInDTO)
        {
            return  await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<string>(ResultCode.BadRequest);
                try
                {
                    var result = await _userRepository.SignIn(signInDTO);
                    serviceResult.Data = result;
                    serviceResult.Status = ResultCode.Ok;
                }
                catch (Exception ex)
                {
                    serviceResult.Status = ResultCode.Unauthorized;
                    serviceResult.Data = ex.Message;
                    return serviceResult;
                }
                return serviceResult;
            });
        }

        public async Task<ServiceResult<GenerateCodeDTO>> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<GenerateCodeDTO>(ResultCode.BadRequest);
                try
                {
                    var result = await _userRepository.ForgotPassword(forgotPasswordDTO);
                    serviceResult.Data = result;
                    serviceResult.Status = ResultCode.Ok;
                }
                catch (Exception ex)
                {
                    serviceResult.AddErrors(ex.Message);
                    serviceResult.Status = ResultCode.Unauthorized;
                    return serviceResult;
                }
                return serviceResult;
            });
        }

        public async Task<ServiceResult<int>> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<int>(ResultCode.BadRequest);
                try
                {
                    var result = await _userRepository.ResetPassword(resetPasswordDTO);
                    serviceResult.Data = result;
                    serviceResult.Status = ResultCode.Ok;
                }
                catch (Exception ex)
                {
                    serviceResult.AddErrors(ex.Message);
                    serviceResult.Status = ResultCode.Unauthorized;
                    return serviceResult;
                }
                return serviceResult;
            });
        }
        public async Task<ServiceResult<int>> Edit(EditUserDTO editUserDTO)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<int>(ResultCode.BadRequest);
                try
                {
                    var result = await _userRepository.EditUser(editUserDTO);
                    serviceResult.Data = result;
                    serviceResult.Status = ResultCode.Ok;
                }
                catch (Exception ex)
                {
                    serviceResult.AddErrors(ex.Message);
                    serviceResult.Status = ResultCode.Forbidden;
                    return serviceResult;
                }
                return serviceResult;
            });
        }

        public async Task<ServiceResult<int>> Delete(int userId)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<int>(ResultCode.BadRequest);
                try
                {
                    var result = await _userRepository.Delete(userId);
                    serviceResult.Data = result;
                    serviceResult.Status = ResultCode.Ok;
                }
                catch (Exception ex)
                {
                    serviceResult.AddErrors(ex.Message);
                    serviceResult.Status = ResultCode.Forbidden;
                    return serviceResult;
                }
                return serviceResult;
            });
        }
    }

   
}
