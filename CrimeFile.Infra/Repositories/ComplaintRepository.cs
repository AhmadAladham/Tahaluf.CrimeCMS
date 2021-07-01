using CrimeFile.Core.Common;
using CrimeFile.Core.DTOs;
using CrimeFile.Core.Entities;
using CrimeFile.Core.Repositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeFile.Infra.Repositories
{
    public class ComplaintRepository: IComplaintRepository
    {
        private readonly IDBContext _dbContext;
        DynamicParameters queryParameters;

        public ComplaintRepository(IDBContext dbContext)
        {
            queryParameters = new DynamicParameters();
            _dbContext = dbContext;
        }

        public async Task<Complaint> GetById(int id)
        {
            queryParameters.Add("@ComplaintId", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryFirstOrDefaultAsync<Complaint>("GetComplaintById", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<List<Complaint>> GetByUserId(int id)
        {
            queryParameters.Add("@UserId", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<Complaint>("GetComplaintByUserId", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return (List<Complaint>)result;
        }

        public async Task<List<Complaint>> GetAll()
        {
            return new List<Complaint>();
        }

        public async Task<PagedList<AllComplaintsDTO>> GetAllPaged(ComplaintParameter complaintParameter)
        {
            queryParameters.Add("@SortingCol", complaintParameter.SortingColumn, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@SortType", complaintParameter.SortType, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@PageNumber", complaintParameter.PageNumber, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@RowsOfPage", complaintParameter.PageSize, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@TotalCount", null, dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await _dbContext.Connection.QueryAsync<AllComplaintsDTO, int, Tuple<AllComplaintsDTO, int>>("GetAllComplaints"
                , (complaintsDTO, tCount) =>
                {
                    Tuple<AllComplaintsDTO, int> t = new Tuple<AllComplaintsDTO, int>(complaintsDTO, tCount);
                    return t;
                }
                , splitOn: "TotalCount"
                , transaction: _dbContext.Transaction
                , param: queryParameters
                , commandType: CommandType.StoredProcedure);
            PagedList<AllComplaintsDTO> crimesPagedList;
            var complaints = new List<AllComplaintsDTO>();
            if (result.Count() != 0)
            {
                int totalCount = result.FirstOrDefault().Item2;
                foreach (var item in result)
                {
                    complaints.Add(item.Item1);
                }
                crimesPagedList = new PagedList<AllComplaintsDTO>(complaints, totalCount, complaintParameter.PageNumber, complaintParameter.PageSize);
            }
            else
            {
                crimesPagedList = new PagedList<AllComplaintsDTO>(complaints, 0, complaintParameter.PageNumber, complaintParameter.PageSize);
            }


            return crimesPagedList;
        }

        public async Task<int> Create(Complaint complaint)
        {
            queryParameters.Add("@UserId", complaint.UserId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@StationId", complaint.StationId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@CrimeCategoryId", complaint.CrimeCategoryId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@ComplaintTitle", complaint.ComplaintTitle, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@ExpectedCrimeDate", complaint.ExpectedCrimeDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            queryParameters.Add("@ComplaintDate", DateTime.Now, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            queryParameters.Add("@ComplaintDescription", complaint.ComplaintDescription, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@CriminalDescription", complaint.CriminalDescription, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@ComplaintStatus", complaint.ComplaintStatus, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@CrimeLocation", complaint.CrimeLocation, dbType: DbType.String, direction: ParameterDirection.Input);
          
            var result = await _dbContext.Connection.ExecuteAsync("CreateComplaint", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> Edit(Complaint complaint)
        {
            queryParameters.Add("@ComplaintId", complaint.ComplaintId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@UserId", complaint.UserId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@StationId", complaint.StationId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@CrimeCategoryId", complaint.CrimeCategoryId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@ComplaintTitle", complaint.ComplaintTitle, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@ExpectedCrimeDate", complaint.ExpectedCrimeDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            queryParameters.Add("@ComplaintDate", complaint.ComplaintDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            queryParameters.Add("@ComplaintDescription", complaint.ComplaintDescription, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@CriminalDescription", complaint.CriminalDescription, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@ComplaintStatus", complaint.ComplaintStatus, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@CrimeLocation", complaint.CrimeLocation, dbType: DbType.String, direction: ParameterDirection.Input);
          
            var result = await _dbContext.Connection.ExecuteAsync("EditComplaint", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> Delete(int id)
        {
            queryParameters.Add("@ComplaintId", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.ExecuteAsync("DeleteComplaint", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }


        public async Task<PagedList<AllComplaintsDTO>> Search(SearchComplaintsDTO searchComplaintsDTO)
        {
            queryParameters.Add("@ComplaintTitle", searchComplaintsDTO.ComplaintTitle, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@DateFrom", searchComplaintsDTO.DateFrom, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            queryParameters.Add("@DateTo", searchComplaintsDTO.DateTo, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            queryParameters.Add("@CrimeCategoryId", searchComplaintsDTO.CrimeCategoryId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@StationId", searchComplaintsDTO.StationId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@ComplaintStatus", searchComplaintsDTO.ComplaintStatus, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@PageNumber", searchComplaintsDTO.PageNumber, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@RowsOfPage", searchComplaintsDTO.PageSize, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@TotalCount", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
            queryParameters.Add("@SortingCol", searchComplaintsDTO.SortingColumn, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@SortType", searchComplaintsDTO.SortType, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<AllComplaintsDTO, int, Tuple<AllComplaintsDTO, int>>("SearchComplaints"
               , (searchComplaintsDTO, tCount) =>
               {
                   Tuple<AllComplaintsDTO, int> t = new Tuple<AllComplaintsDTO, int>(searchComplaintsDTO, tCount);
                   return t;
               }
               , splitOn: "TotalCount"
               , transaction: _dbContext.Transaction
               , param: queryParameters
               , commandType: CommandType.StoredProcedure);

            var complaints = new List<AllComplaintsDTO>();
            int totalCount = 0;
            if (result.Count() > 0) totalCount = result.FirstOrDefault().Item2;
            foreach (var item in result)
            {
                complaints.Add(item.Item1);
            }
            var complaintsPagedList = new PagedList<AllComplaintsDTO>(complaints, totalCount, searchComplaintsDTO.PageNumber, searchComplaintsDTO.PageSize);
            return complaintsPagedList;
        }
    }
}
