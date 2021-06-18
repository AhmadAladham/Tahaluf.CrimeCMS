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
    public class StationRepository: IStationRepository
    {
        private readonly IDBContext _dbContext;
        DynamicParameters queryParameters;
        
        public StationRepository(IDBContext dbContext)
        {
            queryParameters = new DynamicParameters();
            _dbContext = dbContext;
        }
        
        public async Task<Station> GetById(int id)
        {
            queryParameters.Add("@StationId", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryFirstOrDefaultAsync<Station>("GetStationById", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<List<Station>> GetAll()
        {  
            var result = await _dbContext.Connection.QueryAsync<Station>("GetAllStations",queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return (List<Station>)result;
        }

        public async Task<int> Create(Station station)
        {
            queryParameters.Add("@StationName", station.StationName, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@PhoneNumber", station.PhoneNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@StationAddress", station.StationAddress, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@TotalStaff", station.TotalStaff, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.ExecuteAsync("CreateStation", queryParameters, _dbContext.Transaction,   commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> Edit(Station station)
        {
            queryParameters.Add("@StationId", station.StationId, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@StationName", station.StationName, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@PhoneNumber", station.PhoneNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@StationAddress", station.StationAddress, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@TotalStaff", station.TotalStaff, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.ExecuteAsync("EditStation", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> Delete(int id)
        {
                queryParameters.Add("@StationId", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
                var result = await _dbContext.Connection.ExecuteAsync("DeleteStation", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
                return result;
        }
    }
}
