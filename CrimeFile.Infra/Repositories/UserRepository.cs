using CrimeFile.Core.Common;
using CrimeFile.Core.DTOs;
using CrimeFile.Core.Entities;
using CrimeFile.Core.Repositories;
using CrimeFile.Core.Security;
using CrimeFile.Infra.Common;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CrimeFile.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDBContext _dbContext;
        private readonly DynamicParameters _queryParameters;
        private readonly PasswordHasher _passwordHasher;
        private readonly IConfigManager _configManager;
        public UserRepository(IDBContext dbContext, IConfigManager configManager)
        {
            _dbContext = dbContext;
            _queryParameters = new DynamicParameters();
            _passwordHasher = new PasswordHasher();
            _configManager = configManager;
        }

        public async Task<string> RefreshToken(int userId)
        {
            _queryParameters.Add("@UserId", userId, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<User, UserRole, Permission, User>("GetUserInfo"
            , (user, userRole, permission) => {
                user.Role = userRole;
                user.Role.Permissions = user.Role.Permissions ?? new List<Permission>();
                if (permission != null)
                {
                    user.Role.Permissions.Add(permission);
                }
                return user;
            }
            , splitOn: "RoleId,PermissionId"
            , transaction: _dbContext.Transaction
            , param: _queryParameters
            , commandType: CommandType.StoredProcedure);

            User user = new User
            {
                FirstName = result.FirstOrDefault().FirstName,
                LastName = result.FirstOrDefault().LastName,
                Email = result.FirstOrDefault().Email,
                Gender = result.FirstOrDefault().Gender,
                EmailIsConfirmed = result.FirstOrDefault().EmailIsConfirmed,
                DateOfBirth = result.FirstOrDefault().DateOfBirth,
                PhoneNumber = result.FirstOrDefault().PhoneNumber,
                UserId = result.FirstOrDefault().UserId,
                Role = new UserRole()
                {
                    RoleName = result.FirstOrDefault().Role.RoleName,
                    RoleId = result.FirstOrDefault().Role.RoleId,
                    Permissions = new HashSet<Permission>()
                },

            };
            foreach (var item in result)
            {
                user.Role.Permissions.Add(item.Role.Permissions.FirstOrDefault());
            }

            var userPermissions = String.Join(",", user.Role.Permissions.Select(r => r.PermissionName));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_configManager.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(CustomClaimTypes.FirstName, user.FirstName),
                    new Claim(CustomClaimTypes.LastName, user.LastName),

                    new Claim(CustomClaimTypes.Email, user.Email),
                    new Claim(CustomClaimTypes.PhoneNumber, user.PhoneNumber),
                    new Claim(CustomClaimTypes.DateOfBirth, (user.DateOfBirth).ToShortDateString()),
                     new Claim(CustomClaimTypes.EmailIsConfirmed, user.EmailIsConfirmed.ToString()),
                    new Claim(CustomClaimTypes.UserId,Convert.ToString(user.UserId)),
                    new Claim(CustomClaimTypes.Role, user.Role.RoleName),
                    new Claim(CustomClaimTypes.Permission, userPermissions)
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(tokenKey),
            SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<RegisterResultDTO> Register(RegisterDTO registerDTO)
        {
            registerDTO.Password = _passwordHasher.HashPassword(registerDTO.Password);
            _queryParameters.Add("@Email", registerDTO.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            _queryParameters.Add("@PhoneNumber", registerDTO.PhoneNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            _queryParameters.Add("@DateOfBirth", registerDTO.DateOfBirth, dbType: DbType.Date, direction: ParameterDirection.Input);
            _queryParameters.Add("@FirstName", registerDTO.FirstName, dbType: DbType.String, direction: ParameterDirection.Input);
            _queryParameters.Add("@LastName", registerDTO.LastName, dbType: DbType.String, direction: ParameterDirection.Input);
            _queryParameters.Add("@HashedPassword", registerDTO.Password, dbType: DbType.String, direction: ParameterDirection.Input);
            _queryParameters.Add("@RoleId", 2, dbType: DbType.Int32, direction: ParameterDirection.Input);
            _queryParameters.Add("@Gender", registerDTO.Gender, dbType: DbType.AnsiStringFixedLength, direction: ParameterDirection.Input);
            _queryParameters.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
            var result = await  _dbContext.Connection.QueryFirstOrDefaultAsync<RegisterResultDTO>("RegisterUser", _queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            var registerResult = new RegisterResultDTO
            {
                UserId = result.UserId,
                CodeGenerated = result.CodeGenerated,
                Email = result.Email
            };
            return registerResult;
        }

        public async Task<string> VerifyEmail(EmailVerificationDTO emailVerification)
        {
            _queryParameters.Add("@Email", emailVerification.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            _queryParameters.Add("@Code", emailVerification.Code, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.ExecuteAsync("VerifyEmail",  _queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return "Email verified successfully";
        }

        public async Task<string> SignIn(SignInDTO signInDTO)
        {
            signInDTO.Password = _passwordHasher.HashPassword(signInDTO.Password);
            _queryParameters.Add("@Email", signInDTO.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            _queryParameters.Add("@Password", signInDTO.Password, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<User, UserRole, Permission, User>("SignIn"
            , (user, userRole, permission) => {
                user.Role = userRole;
                user.Role.Permissions = user.Role.Permissions ?? new List<Permission>();
                if (permission != null)
                {
                    user.Role.Permissions.Add(permission);
                }
                return user;
            }
            , splitOn: "RoleId,PermissionId"
            , transaction: _dbContext.Transaction
            , param: _queryParameters
            , commandType: CommandType.StoredProcedure);

            User user = new User
            {
                FirstName = result.FirstOrDefault().FirstName,
                LastName = result.FirstOrDefault().LastName,
                Email = result.FirstOrDefault().Email,
                Gender = result.FirstOrDefault().Gender,
                EmailIsConfirmed = result.FirstOrDefault().EmailIsConfirmed,
                DateOfBirth = result.FirstOrDefault().DateOfBirth,
                PhoneNumber = result.FirstOrDefault().PhoneNumber,
                UserId = result.FirstOrDefault().UserId,
                Role = new UserRole()
                {
                    RoleName = result.FirstOrDefault().Role.RoleName,
                    RoleId = result.FirstOrDefault().Role.RoleId,
                    Permissions = new HashSet<Permission>()
                },
                
            };
            foreach (var item in result)
            {
                user.Role.Permissions.Add(item.Role.Permissions.FirstOrDefault());
            }

            var userPermissions = String.Join(",", user.Role.Permissions.Select(r => r.PermissionName));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_configManager.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(CustomClaimTypes.FirstName, user.FirstName),
                    new Claim(CustomClaimTypes.LastName, user.LastName),
                
                    new Claim(CustomClaimTypes.Email, user.Email),
                    new Claim(CustomClaimTypes.PhoneNumber, user.PhoneNumber),
                    new Claim(CustomClaimTypes.DateOfBirth, (user.DateOfBirth).ToShortDateString()),
                     new Claim(CustomClaimTypes.EmailIsConfirmed, user.EmailIsConfirmed.ToString()),
                    new Claim(CustomClaimTypes.UserId,Convert.ToString(user.UserId)),
                    new Claim(CustomClaimTypes.Role, user.Role.RoleName),
                    new Claim(CustomClaimTypes.Permission, userPermissions)
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(tokenKey),
            SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<GenerateCodeDTO> ForgotPassword(ForgotPasswordDTO forgotPassword)
        {
            _queryParameters.Add("@Email",forgotPassword.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryFirstOrDefaultAsync<GenerateCodeDTO>("GenerateCode", _queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);

            var generateCodeDTO = new GenerateCodeDTO
            {
                UserId = result.UserId,
                UpdatedDate = result.UpdatedDate,
                CodeGenerated = result.CodeGenerated,
                Email = result.Email
            };
            return generateCodeDTO;
        }

        public async Task<int> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            resetPasswordDTO.NewPassword = _passwordHasher.HashPassword(resetPasswordDTO.NewPassword);
            _queryParameters.Add("@Email", resetPasswordDTO.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            _queryParameters.Add("@Code", resetPasswordDTO.Code, dbType: DbType.Int32, direction: ParameterDirection.Input);
            _queryParameters.Add("@NewPassword", resetPasswordDTO.NewPassword, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.ExecuteAsync("ResetPassword", _queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            changePasswordDTO.NewPassword = _passwordHasher.HashPassword(changePasswordDTO.NewPassword);
            changePasswordDTO.OldPassword = _passwordHasher.HashPassword(changePasswordDTO.OldPassword);
           
            _queryParameters.Add("@OldPassword", changePasswordDTO.OldPassword, dbType: DbType.String, direction: ParameterDirection.Input);
            _queryParameters.Add("@UserId", changePasswordDTO.UserId, dbType: DbType.String, direction: ParameterDirection.Input);
          
            _queryParameters.Add("@NewPassword", changePasswordDTO.NewPassword, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.ExecuteAsync("ChangePassword", _queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<User> GetById(int id)
        {
            return  new User();
        }

        public async Task<int> Create(User user)
        {
            return 1;
        }
        public async Task<List<User>> GetAll()
        {
            return new List<User>();
        }

        public async Task<PagedList<UserDTO>> GetAllPaged(UserParameters userParameters)
        {
            _queryParameters.Add("@SortingCol", userParameters.SortingColumn, dbType: DbType.String, direction: ParameterDirection.Input);
            _queryParameters.Add("@SortType", userParameters.SortType, dbType: DbType.String, direction: ParameterDirection.Input);
            _queryParameters.Add("@PageNumber", userParameters.PageNumber, dbType: DbType.Int32, direction: ParameterDirection.Input);
            _queryParameters.Add("@RowsOfPage", userParameters.PageSize, dbType: DbType.Int32, direction: ParameterDirection.Input);
            _queryParameters.Add("@TotalCount", null, dbType: DbType.Int32, direction: ParameterDirection.Output);
            
            var result = await _dbContext.Connection.QueryAsync<UserDTO, int, Tuple<UserDTO, int>>("GetAllUsers"
                ,(userDTO, tCount)=> 
                {
                    Tuple  <UserDTO, int> t = new Tuple<UserDTO, int>(userDTO, tCount);
                    return t;
                }
                , splitOn: "TotalCount"
                , transaction: _dbContext.Transaction
                , param: _queryParameters
                , commandType: CommandType.StoredProcedure);
            PagedList<UserDTO> usersPagedList;
            var users = new List<UserDTO>();
            if (result.Count() != 0)
            {
                int totalCount = result.FirstOrDefault().Item2;
                foreach (var item in result)
                {
                    users.Add(item.Item1);
                }
                usersPagedList = new PagedList<UserDTO>(users, totalCount, userParameters.PageNumber, userParameters.PageSize);
               
            }
            else
            {
                usersPagedList = new PagedList<UserDTO>(users, 0, userParameters.PageNumber, userParameters.PageSize);
            }
            return usersPagedList;
        }

        public async Task<int> EditUser(EditUserDTO editUserDTO)
        {
            _queryParameters.Add("@UserId", editUserDTO.UserId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            _queryParameters.Add("@PhoneNumber", editUserDTO.PhoneNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            _queryParameters.Add("@FirstName", editUserDTO.FirstName, dbType: DbType.String, direction: ParameterDirection.Input);
            _queryParameters.Add("@LastName", editUserDTO.LastName, dbType: DbType.String, direction: ParameterDirection.Input);
            _queryParameters.Add("@Email", editUserDTO.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            _queryParameters.Add("@DateOfBirth", editUserDTO.DateOfBirth, dbType: DbType.Date, direction: ParameterDirection.Input);
           
            var result = await _dbContext.Connection.ExecuteAsync("EditUser", _queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> Delete(int userId)
        {
            _queryParameters.Add("@UserId", userId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.ExecuteAsync("DeleteUser", _queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<PagedList<UserDTO>> Search(SearchUserDTO userDTO)
        {
            _queryParameters.Add("@PhoneNumber", userDTO.PhoneNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            _queryParameters.Add("@FirstName", userDTO.FirstName, dbType: DbType.String, direction: ParameterDirection.Input);
            _queryParameters.Add("@RoleId", userDTO.RoleId, dbType: DbType.String, direction: ParameterDirection.Input);
            _queryParameters.Add("@PageNumber", userDTO.PageNumber, dbType: DbType.Int32, direction: ParameterDirection.Input);
            _queryParameters.Add("@RowsOfPage", userDTO.PageSize, dbType: DbType.Int32, direction: ParameterDirection.Input);
            _queryParameters.Add("@TotalCount", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
            _queryParameters.Add("@SortingCol", userDTO.SortingColumn, dbType: DbType.String, direction: ParameterDirection.Input);
            _queryParameters.Add("@SortType", userDTO.SortType, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<UserDTO, int, Tuple<UserDTO, int>>("SearchUsers"
               , (userDTO, tCount) =>
               {
                   Tuple<UserDTO, int> t = new Tuple<UserDTO, int>(userDTO, tCount);
                   return t;
               }
               , splitOn: "TotalCount"
               , transaction: _dbContext.Transaction
               , param: _queryParameters
               , commandType: CommandType.StoredProcedure);

            var users = new List<UserDTO>();
           
            int totalCount = 0;
            if (result.Count() > 0) totalCount = result.FirstOrDefault().Item2;
            foreach (var item in result)
            {
                users.Add(item.Item1);
            }
            var usersPagedList = new PagedList<UserDTO>(users, totalCount, userDTO.PageNumber, userDTO.PageSize);
            return usersPagedList;
        }

        public Task<int> Edit(User entity)
        {
            throw new NotImplementedException();
        }
    }

}
