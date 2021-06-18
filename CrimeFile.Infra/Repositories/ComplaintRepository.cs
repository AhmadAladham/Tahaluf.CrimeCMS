using CrimeFile.Core.Common;
using CrimeFile.Core.Entities;
using CrimeFile.Core.Repositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task<List<Complaint>> GetAll()
        {
            return new List<Complaint>();
        }

        public async Task<int> Create(Complaint complaint)
        {
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
            queryParameters.Add("@Image", complaint.Image, dbType: DbType.String, direction: ParameterDirection.Input);
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
            queryParameters.Add("@Image", complaint.Image, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.ExecuteAsync("EditComplaint", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> Delete(int id)
        {
            queryParameters.Add("@ComplaintId", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.ExecuteAsync("DeleteComplaint", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
