using CrimeFile.Core.Common;
using CrimeFile.Core.DTOs;
using CrimeFile.Core.Entities;
using CrimeFile.Core.Repositories;
using CrimeFile.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrimeFile.Infra.Services
{
    public class CriminalService: BaseService, ICriminalService
    {
        private readonly ICriminalRepository _criminalRepository;
        public CriminalService(ICriminalRepository criminalRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _criminalRepository = criminalRepository;
        }

        public async Task<ServiceResult<List<Criminal>>> GetAll()
        {

            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<List<Criminal>>(ResultCode.BadRequest);
                try
                {
                    var result = await _criminalRepository.GetAll();
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

        public async Task<ServiceResult<Criminal>> GetById(int id)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<Criminal>(ResultCode.BadRequest);
                try
                {
                    var result = await _criminalRepository.GetById(id);
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

        public async Task<ServiceResult<Criminal>> GetByNationalNumber(string nationalNumber)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<Criminal>(ResultCode.BadRequest);
                try
                {
                    var result = await _criminalRepository.GetByNationalNumber(nationalNumber);
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

        public async Task<ServiceResult<int>> Create(Criminal criminal)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<int>(ResultCode.BadRequest);
                try
                {
                    var result = await _criminalRepository.Create(criminal);
                    serviceResult.Data = result;
                    serviceResult.Status = ResultCode.Created;
                }
                catch (Exception ex)
                {

                }

                return serviceResult;
            });
        }

        public async Task<ServiceResult<int>> Edit(Criminal criminal)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<int>(ResultCode.BadRequest);
                try
                {
                    var result = await _criminalRepository.Edit(criminal);
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
                    var result = await _criminalRepository.Delete(id);
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

        public async Task<ServiceResult<IEnumerable<Criminal>>> Search(CriminalDto criminalDto)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<IEnumerable<Criminal>>(ResultCode.BadRequest);
                try
                {
                    var result = await _criminalRepository.Search(criminalDto);
                    serviceResult.Data = result;
                    serviceResult.Status = ResultCode.Ok;
                }
                catch (Exception ex)
                {

                }

                return serviceResult;
            });
        }
    }
}
