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

        public async Task<PagedList<AllCrimeDTO>> GetAllPaged(CrimeParameters crimeParameters)
        {
            queryParameters.Add("@SortingCol", crimeParameters.SortingColumn, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@SortType", crimeParameters.SortType, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@PageNumber", crimeParameters.PageNumber, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@RowsOfPage", crimeParameters.PageSize, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@TotalCount", null, dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await _dbContext.Connection.QueryAsync<AllCrimeDTO, int, Tuple<AllCrimeDTO, int>>("GetAllCrimes"
                , (crimeDTO, tCount) =>
                {
                    Tuple<AllCrimeDTO, int> t = new Tuple<AllCrimeDTO, int>(crimeDTO, tCount);
                    return t;
                }
                , splitOn: "TotalCount"
                , transaction: _dbContext.Transaction
                , param: queryParameters
                , commandType: CommandType.StoredProcedure);

            var crimes = new List<AllCrimeDTO>();
            int totalCount = result.FirstOrDefault().Item2;
            foreach (var item in result)
            {
                crimes.Add(item.Item1);
            }
            var crimesPagedList = new PagedList<AllCrimeDTO>(crimes, totalCount, crimeParameters.PageNumber, crimeParameters.PageSize);
            return crimesPagedList;
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

        public async Task<PagedList<AllCrimeDTO>> Search(CrimeDto crimeDto){
            queryParameters.Add("@CrimeTtile", crimeDto.CrimeTtile, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@DateFrom", crimeDto.DateFrom, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            queryParameters.Add("@DateTo", crimeDto.DateTo, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            queryParameters.Add("@CrimeCategoryId", crimeDto.CrimeCategoryId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@StationId", crimeDto.StationId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@Location", crimeDto.Location, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@PageNumber", crimeDto.PageNumber, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@RowsOfPage", crimeDto.PageSize, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@TotalCount", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
            queryParameters.Add("@SortingCol", crimeDto.SortingColumn, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@SortType", crimeDto.SortType, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryAsync<AllCrimeDTO, int, Tuple<AllCrimeDTO, int>>("SearchCrimes"
               , (crimeDTO, tCount) =>
               {
                   Tuple<AllCrimeDTO, int> t = new Tuple<AllCrimeDTO, int>(crimeDTO, tCount);
                   return t;
               }
               , splitOn: "TotalCount"
               , transaction: _dbContext.Transaction
               , param: queryParameters
               , commandType: CommandType.StoredProcedure);

            var crimes = new List<AllCrimeDTO>();
            int totalCount = 0;
            if(result.Count()>0) totalCount = result.FirstOrDefault().Item2;
            foreach (var item in result)
            {
                crimes.Add(item.Item1);
            }
            var crimesPagedList = new PagedList<AllCrimeDTO>(crimes, totalCount, crimeDto.PageNumber, crimeDto.PageSize);
            return crimesPagedList;
        }
    }
}
