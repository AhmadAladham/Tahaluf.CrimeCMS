using CrimeFile.Core.DTOs;
using CrimeFile.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrimeFile.Core.Repositories
{
    public interface IComplaintRepository: IRepository<Complaint>
    {
        Task<Complaint> GetById(int id);
        Task<PagedList<AllComplaintsDTO>> GetAllPaged(ComplaintParameter complaintParameter);
    }
}
