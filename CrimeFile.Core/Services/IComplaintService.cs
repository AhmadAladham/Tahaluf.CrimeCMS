using CrimeFile.Core.Common;
using CrimeFile.Core.DTOs;
using CrimeFile.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrimeFile.Core.Services
{
    public interface IComplaintService:IBaseService
    {
        Task<ServiceResult<Complaint>> GetById(int id);
        Task<ServiceResult<PagedList<AllComplaintsDTO>>> GetAllPaged(ComplaintParameter complaintParameter);
        Task<ServiceResult<int>> Create(Complaint complaint);
        Task<ServiceResult<int>> Edit(Complaint complaint);
        Task<ServiceResult<int>> Delete(int id);
    }
}
