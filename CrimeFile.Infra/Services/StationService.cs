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
    public class StationService: BaseService, IStationService
    {
        private readonly IStationRepository _stationRepository;
        public StationService(IStationRepository stationRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _stationRepository = stationRepository;
        }

        public async Task<ServiceResult<List<Station>>> GetAll()
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<List<Station>>(ResultCode.BadRequest);
                try
                {
                    var result = await _stationRepository.GetAll();
                    serviceResult.Data = result;
                    serviceResult.Status = ResultCode.Ok;
                }
                catch (Exception ex)
                {
                    
                }

                return serviceResult;
            });
        }

        public async Task<ServiceResult<Station>> GetById(int id)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<Station>(ResultCode.BadRequest);
                try
                {
                    var result = await _stationRepository.GetById(id);
                    serviceResult.Data = result;
                    serviceResult.Status = ResultCode.Ok;
                }
                catch (Exception ex)
                {
                    Error error = new Error(ex);
                    serviceResult.Errors.Add(error);
                }

                return serviceResult;
            });
        }

        public async Task<ServiceResult<int>> Create(Station station)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<int>(ResultCode.BadRequest);
                try
                {
                    var result = await _stationRepository.Create(station);
                    serviceResult.Data = result;
                    serviceResult.Status = ResultCode.Created;
                }
                catch (Exception ex)
                {
                    Error error = new Error(ex);
                    serviceResult.Errors = new List<Error>();
                    serviceResult.Errors.Add(error);
                }

                return serviceResult;
            });
        }

        public async Task<ServiceResult<int>> Edit(Station station)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<int>(ResultCode.BadRequest);
                try
                {
                    var result = await _stationRepository.Edit(station);
                    serviceResult.Data = result;
                    serviceResult.Status = ResultCode.Created;
                }
                catch (Exception ex)
                {
                    // _ = ex.Message;
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
                    var result = await _stationRepository.Delete(id);
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
