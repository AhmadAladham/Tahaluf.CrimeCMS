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
            var result = await _dbContext.Connection.QueryAsync<Criminal, Crime,
                    Criminal>("GetCriminalById"
                , (criminal, crime) =>
                { 
                    criminal.Crimes = criminal.Crimes ?? new List<Crime>();
                    if (crime != null)
                    {
                        criminal.Crimes.Add(crime);
                    }
                    return criminal;
                }
                , splitOn: "CrimeId"
                , transaction: _dbContext.Transaction
                , param: queryParameters
                , commandType: CommandType.StoredProcedure);

            var Finalresult = result.AsList<Criminal>().GroupBy(c => c.CriminalId).Select(g =>
            {
                Criminal criminal = g.First();

                criminal.Crimes = g.Where(g => g.Crimes.Any() && g.Crimes.Count > 0)
                .Select(p => p.Crimes.Single())
                .GroupBy(crime => crime.CrimeId).Select(crime => new Crime
                {
                    CrimeId = crime.First().CrimeId,
                    CrimeTtile = crime.First().CrimeTtile,
                    CrimeDate = crime.First().CrimeDate,
                    CrimeDescription = crime.First().CrimeDescription,
                    Location = crime.First().Location
                }).ToList();
                return criminal;
            }).ToList();
            return Finalresult.FirstOrDefault();
        }
        public async Task<Criminal> GetByNationalNumber(string nationalNumber)
        {
            queryParameters.Add("@CriminalNationalNumber", nationalNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            var result = await _dbContext.Connection.QueryFirstOrDefaultAsync<Criminal>("GetCriminalByNationalNumber", queryParameters, _dbContext.Transaction, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<List<Criminal>> GetAll()
        {
            return new List<Criminal>();
        }

        public async Task<PagedList<Criminal>> GetAllPaged(CriminalParameters criminalParameters)
        {
            queryParameters.Add("@SortingCol", criminalParameters.SortingColumn, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@SortType", criminalParameters.SortType, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@PageNumber", criminalParameters.PageNumber, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@RowsOfPage", criminalParameters.PageSize, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@TotalCount", null, dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await _dbContext.Connection.QueryAsync<Criminal, int, Tuple<Criminal , int>>("GetAllCriminals"
                , (criminal, totalCount) =>
                {
                    Tuple<Criminal, int> t = new Tuple<Criminal, int>(criminal, totalCount);
                    return t;
                }
                , splitOn: "TotalCount"
                , transaction: _dbContext.Transaction
                , param: queryParameters
                , commandType: CommandType.StoredProcedure);
            PagedList<Criminal> crimesPagedList;
            var criminals = new List<Criminal>();
            if (result.Count() != 0)
            {
                int totalCount = result.FirstOrDefault().Item2;
                foreach (var item in result)
                {
                    criminals.Add(item.Item1);
                }
                crimesPagedList = new PagedList<Criminal>(criminals, totalCount, criminalParameters.PageNumber, criminalParameters.PageSize);
            }
            else
            {
                crimesPagedList = new PagedList<Criminal>(criminals, 0, criminalParameters.PageNumber, criminalParameters.PageSize);
            }
            return crimesPagedList;
        }

        public async Task<int> Create(Criminal criminal)
        {
            queryParameters.Add("@CriminalNationalNumber", criminal.CriminalNationalNumber, dbType: DbType.String, direction: ParameterDirection.Input);
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
            queryParameters.Add("@CriminalNationalNumber", criminal.CriminalNationalNumber, dbType: DbType.String, direction: ParameterDirection.Input);
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

        public async Task<PagedList<Criminal>> Search(SearchCriminalsDTO searchCriminalsDTO)
        {
            queryParameters.Add("@CriminalName", searchCriminalsDTO.CriminalName, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@HeightFrom", searchCriminalsDTO.HeightFrom, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            queryParameters.Add("@HeightTo", searchCriminalsDTO.HeightTo, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            queryParameters.Add("@WeightFrom", searchCriminalsDTO.WeightFrom, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            queryParameters.Add("@WeightTo", searchCriminalsDTO.WeightTo, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            queryParameters.Add("@SortingCol", searchCriminalsDTO.SortingColumn, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@SortType", searchCriminalsDTO.SortType, dbType: DbType.String, direction: ParameterDirection.Input);
            queryParameters.Add("@PageNumber", searchCriminalsDTO.PageNumber, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@RowsOfPage", searchCriminalsDTO.PageSize, dbType: DbType.Int32, direction: ParameterDirection.Input);
            queryParameters.Add("@TotalCount", null, dbType: DbType.Int32, direction: ParameterDirection.Output);
            var result = await _dbContext.Connection.QueryAsync<Criminal, int, Tuple<Criminal, int>>("SearchCriminals"
                , (criminal, totalCount) =>
                {
                    Tuple<Criminal, int> t = new Tuple<Criminal, int>(criminal, totalCount);
                    return t;
                }
                , splitOn: "TotalCount"
                , transaction: _dbContext.Transaction
                , param: queryParameters
                , commandType: CommandType.StoredProcedure);
            PagedList<Criminal> criminalsPagedList;
            var criminals = new List<Criminal>();
            if (result.Count() != 0)
            {
                int totalCount = result.FirstOrDefault().Item2;
                foreach (var item in result)
                {
                    criminals.Add(item.Item1);
                }
                criminalsPagedList = new PagedList<Criminal>(criminals, totalCount, searchCriminalsDTO.PageNumber, searchCriminalsDTO.PageSize);
            }
            else
            {
                criminalsPagedList = new PagedList<Criminal>(criminals, 0, searchCriminalsDTO.PageNumber, searchCriminalsDTO.PageSize);
            }
            return criminalsPagedList;

        }

    }
}
