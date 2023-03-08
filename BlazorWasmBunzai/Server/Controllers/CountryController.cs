using Dapper;
using System.Data;
using System.Data.SQLite;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlazorCountriesWasm.Server.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IConfiguration _config;
        public CountryController(IConfiguration config)
        {
            _config = config;
        }

        public string connectionId = "Default";
        public string sqlCommand = "";
        IEnumerable<Country>? countries;

        [HttpGet]
        [Route("api/country/")]
        public async Task<ActionResult<List<Country>>> GetCountries()
        {
            sqlCommand = "Select * From Country";
            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                countries = await conn.QueryAsync<Country>(sqlCommand);
            }
            return Ok(countries);
        }

        [HttpGet]
        [Route("api/country/{CountryId}")]
        public async Task<ActionResult<Country>> GetCountryById(int CountryId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CountryId", CountryId, DbType.Int32);

            sqlCommand = $"Select * From Country " +
                "Where CountryId =  @CountryId";

            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                var country = await conn.QueryFirstAsync<Country>(sqlCommand, parameters);
                return Ok(country);
            }
        }

        [HttpPost]
        [Route("api/country/")]
        public async Task<ActionResult> CountryInsert(Country country)
        {
            var parameters = new DynamicParameters();
            parameters.Add("CountryName", country.CountryName, DbType.String);

            sqlCommand = "Insert into Country (CountryName) values(@CountryName)";
            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                await conn.ExecuteAsync(sqlCommand, parameters);
            }
            return Ok();
        }

        [HttpPut]
        [Route("api/country/{CountryId}")]
        public async Task<ActionResult> CountryUpdate(Country country)
        {
            var parameters = new DynamicParameters();
            parameters.Add("CountryId", country.CountryId, DbType.Int32);
            parameters.Add("CountryName", country.CountryName, DbType.String);

            sqlCommand =
                "Update Country " +
                "set CountryName = @CountryName " +
                "Where CountryId = @CountryId";
            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                await conn.ExecuteAsync(sqlCommand, parameters);
            }
            return Ok();
        }

        [HttpDelete]
        [Route("api/country/{CountryId}")]
        public async Task<ActionResult> CountryDelete(int CountryId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("CountryId", CountryId, DbType.Int32);

            sqlCommand =
                "Delete From Country " +
                "Where CountryId = @CountryId";
            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                await conn.ExecuteAsync(sqlCommand, parameters);
            }
            return Ok();
        }

        [HttpGet]
        [Route("api/countryname/{CountryName}")]
        public async Task<ActionResult> CountCountriesByName(string CountryName)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CountryName", CountryName, DbType.String);

            sqlCommand = $"Select Count(*) From Country " +
                "Where Upper(CountryName) =  Upper(@CountryName)";

            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                int duplicates = await conn.QueryFirstAsync<int>(sqlCommand, parameters);
                return Ok(duplicates);
            }
        }

        [HttpGet]
        [Route("api/countryname/{CountryName}/{CountryId}")]
        public async Task<ActionResult> CountCountriesByNameAndId(string CountryName, int CountryId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CountryName", CountryName, DbType.String);
            parameters.Add("@CountryId", CountryId, DbType.Int32);

            sqlCommand = $"Select Count(*) From Country " +
                "Where Upper(CountryName) =  Upper(@CountryName)" +
                "And CountryId <> @CountryId";

            using IDbConnection conn = new SQLiteConnection(_config.GetConnectionString(connectionId));
            {
                int duplicates = await conn.QueryFirstAsync<int>(sqlCommand, parameters);
                return Ok(duplicates);
            }
        }

    }
}
