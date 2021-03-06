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
    public class ComplaintService : BaseService, IComplaintService
    {
        private readonly IComplaintRepository _complaintRepository;
        public ComplaintService(IComplaintRepository complaintRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _complaintRepository = complaintRepository;
        }

        public async Task<ServiceResult<PagedList<AllComplaintsDTO>>> GetAllPaged(ComplaintParameter complaintParameter)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<PagedList<AllComplaintsDTO>>(ResultCode.BadRequest);
                try
                {
                    var result = await _complaintRepository.GetAllPaged(complaintParameter);
                    serviceResult.Data = result;
                    serviceResult.Status = ResultCode.Ok;
                }
                catch (Exception ex)
                {

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

        public async Task<ServiceResult<List<Complaint>>> GetByUserId(int id)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<List<Complaint>>(ResultCode.BadRequest);
                try
                {
                    var result = await _complaintRepository.GetByUserId(id);
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

        public async Task<ServiceResult<int>> EditComplaintStatus(EditComplaintStatusDTO editComplaintStatusDTO)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<int>(ResultCode.BadRequest);
                try
                {
                    var result = await _complaintRepository.EditComplaintStatus(editComplaintStatusDTO);
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

        public async Task<ServiceResult<PagedList<AllComplaintsDTO>>> Search(SearchComplaintsDTO searchComplaintsDTO)
        {
            return await ExecuteAsync(async x =>
            {
                var serviceResult = new ServiceResult<PagedList<AllComplaintsDTO>>(ResultCode.BadRequest);
                try
                {
                    var result = await _complaintRepository.Search(searchComplaintsDTO);
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
