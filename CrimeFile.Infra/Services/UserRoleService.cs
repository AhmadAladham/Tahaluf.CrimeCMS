using CrimeFile.Core.Common;
using CrimeFile.Core.Entities;
using CrimeFile.Core.Repositories;
using CrimeFile.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrimeFile.Infra.Services
{
    public class UserRoleService : BaseService, IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        public UserRoleService(IUserRoleRepository userRoleRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task<ServiceResult<List<UserRole>>> GetAll()
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<List<UserRole>>(ResultCode.BadRequest);
                try
                {
                    var result = await _userRoleRepository.GetAll();
                    serviceResult.Data = result;
                    serviceResult.Status = ResultCode.Ok;
                }
                catch (Exception ex)
                {
                    // _ = ex.Message;
                }

                return serviceResult;
            });
        }
    }
}
