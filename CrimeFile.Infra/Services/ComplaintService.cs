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
    public class ComplaintService : BaseService, IComplaintService
    {
        private readonly IComplaintRepository _complaintRepository;
        public ComplaintService(IComplaintRepository complaintRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _complaintRepository = complaintRepository;
        }

        public async Task<ServiceResult<List<Complaint>>> GetAll()
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<List<Complaint>>(ResultCode.BadRequest);
                try
                {
                    var result = await _complaintRepository.GetAll();
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

        public async Task<ServiceResult<Complaint>> GetById(int id)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<Complaint>(ResultCode.BadRequest);
                try
                {
                    var result = await _complaintRepository.GetById(id);
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

        public async Task<ServiceResult<int>> Create(Complaint complaint)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<int>(ResultCode.BadRequest);
                try
                {
                    var result = await _complaintRepository.Create(complaint);
                    serviceResult.Data = result;
                    serviceResult.Status = ResultCode.Created;
                }
                catch (Exception ex)
                {

                }

                return serviceResult;
            });

        }

        public async Task<ServiceResult<int>> Edit(Complaint complaint)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<int>(ResultCode.BadRequest);
                try
                {
                    var result = await _complaintRepository.Edit(complaint);
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
                    var result = await _complaintRepository.Delete(id);
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
