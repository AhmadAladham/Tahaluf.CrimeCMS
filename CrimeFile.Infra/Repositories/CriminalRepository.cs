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
    public class CriminalRepository : ICriminalRepository
    {
        private readonly IDBContext _dbContext;
        DynamicParameters queryParameters;

        public CriminalRepository(IDBContext dbContext)
        {
            queryParameters = new DynamicParameters();
            _dbContext = dbContext;
        }

        public async Task<Criminal> GetById(int id)
        {
            queryParameters.Add("@CriminalId", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryFirstOrDefaultAsync<Criminal>("GetCriminalById", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<List<Criminal>> GetAll()
        {
            return new List<Criminal>();
        }

        public async Task<int> Create(Criminal criminal)
        {
            queryParameters.Add("@CriminalFirstName", criminal.CriminalFirstName, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@CriminalLastName", criminal.CriminalLastName, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@Height", criminal.Height, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            queryParameters.Add("@Weight", criminal.Weight, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            queryParameters.Add("@Image", criminal.Image, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@PhoneNumber", criminal.PhoneNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@DateOfBirth", criminal.DateOfBirth, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            queryParameters.Add("@Address", criminal.Address, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.ExecuteAsync("CreateCriminal", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> Edit(Criminal criminal)
        {
            queryParameters.Add("@CriminalId", criminal.CriminalId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@CriminalFirstName", criminal.CriminalFirstName, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@CriminalLastName", criminal.CriminalLastName, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@Height", criminal.Height, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            queryParameters.Add("@Weight", criminal.Weight, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            queryParameters.Add("@Image", criminal.Image, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@PhoneNumber", criminal.PhoneNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@DateOfBirth", criminal.DateOfBirth, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            queryParameters.Add("@Address", criminal.Address, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.ExecuteAsync("EditCriminals", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> Delete(int id)
        {
            queryParameters.Add("@CriminalId", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.ExecuteAsync("DeleteCriminal", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<IEnumerable<Criminal>> Search(CriminalDto criminalDto)
        {
            queryParameters.Add("@CriminalFirstName", criminalDto.CriminalFirstName, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@DateFrom", criminalDto.DateFrom, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            queryParameters.Add("@DateTo", criminalDto.DateTo, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            queryParameters.Add("@CrimeTtile", criminalDto.CrimeTtile, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<Criminal>("SearchCriminals", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return (IEnumerable<Criminal>)result;
        }

    }
}
