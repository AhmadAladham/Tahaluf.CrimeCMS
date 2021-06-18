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
    public class CrimeCategoryService : BaseService, ICrimeCategoryService
    {
        private readonly ICrimeCategoryRepository _crimeCategoryRepository;
        public CrimeCategoryService(ICrimeCategoryRepository crimeCategoryRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _crimeCategoryRepository = crimeCategoryRepository;
        }

        public async Task<ServiceResult<List<CrimeCategory>>> GetAll()
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<List<CrimeCategory>>(ResultCode.BadRequest);
                try
                {
                    var result = await _crimeCategoryRepository.GetAll();
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

        public async Task<ServiceResult<CrimeCategory>> GetById(int id)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<CrimeCategory>(ResultCode.BadRequest);
                try
                {
                    var result = await _crimeCategoryRepository.GetById(id);
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

        public async Task<ServiceResult<int>> Create(CrimeCategory crimeCategory)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<int>(ResultCode.BadRequest);
                try
                {
                    var result = await _crimeCategoryRepository.Create(crimeCategory);
                    serviceResult.Data = result;
                    serviceResult.Status = ResultCode.Created;
                }
                catch (Exception ex)
                {

                }

                return serviceResult;
            });
        }

        public async Task<ServiceResult<int>> Edit(CrimeCategory crimeCategory)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<int>(ResultCode.BadRequest);
                try
                {
                    var result = await _crimeCategoryRepository.Edit(crimeCategory);
                    serviceResult.Data = result;
                    serviceResult.Status = ResultCode.Created;
                }
                catch (Exception ex)
                {

                }

                return serviceResult;
            });
        }

        public async Task<ServiceResult<int>> Delete(int id)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<int>(ResultCode.BadRequest);
                try
                {
                    var result = await _crimeCategoryRepository.Delete(id);
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
