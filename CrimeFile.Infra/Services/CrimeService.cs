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
    public class CrimeService : BaseService, ICrimeService
    {
        private readonly ICrimeRepository _crimeRepository;
        public CrimeService(ICrimeRepository crimeRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _crimeRepository = crimeRepository;
        }

        public async Task<ServiceResult<PagedList<AllCrimeDTO>>> GetAllPaged(CrimeParameters crimeParameters)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<PagedList<AllCrimeDTO>>(ResultCode.BadRequest);
                try
                {
                    var result = await _crimeRepository.GetAllPaged(crimeParameters);
                    serviceResult.Data = result;
                    serviceResult.Status = ResultCode.Ok;
                }
                catch (Exception ex)
                {

                }

                return serviceResult;
            });
        }

        public async Task<ServiceResult<AllCrimeDTO>> GetById(int id)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<AllCrimeDTO>(ResultCode.BadRequest);
                try
                {
                    var result = await _crimeRepository.GetById(id);
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

        public async Task<ServiceResult<int>> Create(Crime crime)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<int>(ResultCode.BadRequest);
                try
                {
                    var result = await _crimeRepository.Create(crime);
                    serviceResult.Data = result;
                    serviceResult.Status = ResultCode.Created;
                }
                catch (Exception ex)
                {

                }

                return serviceResult;
            });
        }

        public async Task<ServiceResult<int>> Edit(Crime crime)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<int>(ResultCode.BadRequest);
                try
                {
                    var result = await _crimeRepository.Edit(crime);
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
                    var result = await _crimeRepository.Delete(id);
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

        public async Task<ServiceResult<PagedList<AllCrimeDTO>>> Search(CrimeDto crimeDto)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<PagedList<AllCrimeDTO>>(ResultCode.BadRequest);
                try
                {
                    var result = await _crimeRepository.Search(crimeDto);
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

