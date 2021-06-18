using CrimeFile.Core.Common;
using CrimeFile.Core.DTOs;
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
    public class CrimeRepository : ICrimeRepository
    {
        private readonly IDBContext _dbContext;
        DynamicParameters queryParameters;

        public CrimeRepository(IDBContext dbContext)
        {
            queryParameters = new DynamicParameters();
            _dbContext = dbContext;
        }

        public async Task<Crime> GetById(int id)
        {
            queryParameters.Add("@CrimeId", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryFirstOrDefaultAsync<Crime>("GetCrimeById", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<List<Crime>> GetAll()
        {
            return new List<Crime>();
        }

        public async Task<int> Create(Crime crime)
        {
            queryParameters.Add("@CrimeTtile", crime.CrimeTtile, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@CrimeEntryDate", crime.CrimeEntryDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            queryParameters.Add("@CrimeDate", crime.CrimeDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            queryParameters.Add("@CloseDate", crime.CloseDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            queryParameters.Add("@IsClosed", crime.IsClosed, dbType: DbType.Boolean, direction: ParameterDirection.Input);
            queryParameters.Add("@Location", crime.Location, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@CrimeDescription", crime.CrimeDescription, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@CriminalDescription", crime.CriminalDescription, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@Image", crime.Image, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@StationId", crime.StationId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@CrimeCategoryId", crime.CrimeCategoryId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@CriminalId", crime.CriminalId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.ExecuteAsync("CreateCrime", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> Edit(Crime crime)
        {
            queryParameters.Add("@CrimeId", crime.CrimeId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@CrimeTtile", crime.CrimeTtile, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@CrimeEntryDate", crime.CrimeEntryDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            queryParameters.Add("@CrimeDate", crime.CrimeDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            queryParameters.Add("@CloseDate", crime.CloseDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            queryParameters.Add("@IsClosed", crime.IsClosed, dbType: DbType.Boolean, direction: ParameterDirection.Input);
            queryParameters.Add("@Location", crime.Location, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@CrimeDescription", crime.CrimeDescription, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@CriminalDescription", crime.CriminalDescription, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@Image", crime.Image, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@StationId", crime.StationId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@CrimeCategoryId", crime.CrimeCategoryId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@CriminalId", crime.CriminalId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.ExecuteAsync("EditCrime", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> Delete(int id)
        {
            queryParameters.Add("@CrimeId", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.ExecuteAsync("DeleteCrime", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<IEnumerable<Crime>> Search(CrimeDto crimeDto)
        {
            queryParameters.Add("@CrimeTtile", crimeDto.CrimeTtile, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@DateFrom", crimeDto.DateFrom, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            queryParameters.Add("@DateTo", crimeDto.DateTo, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            queryParameters.Add("@CrimeCategoryName", crimeDto.CrimeCategoryName, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@Location", crimeDto.Location, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<Crime>("SearchCrimes", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return (IEnumerable<Crime>)result;
        }
    }
}
