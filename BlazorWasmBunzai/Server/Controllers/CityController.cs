using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWasmBunzai.Server.Controllers
{
    [Route(template:"api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        public static List<City> cities = new List<City>
        {
            new City
            {
                CityId = 1,
                CityName = "London",
                CityPopulation = 9000000,
                CountryId = 1
            },
            new City
            {
                CityId = 2,
                CityName = "Birmigham",
                CityPopulation = 1500000,
                CountryId = 1
            },
            new City
            {
                CityId = 3,
                CityName = "Oxford",
                CityPopulation = 2500000,
                CountryId = 1
            },
            new City
            {
                CityId = 4,
                CityName = "Cambridge",
                CityPopulation = 200000,
                CountryId = 1
            },
            new City
            {
                CityId = 5,
                CityName = "Paris",
                CityPopulation = 7500000,
                CountryId = 2
            },
            new City
            {
                CityId = 6,
                CityName = "Toulouse",
                CityPopulation = 200000,
                CountryId = 2
            },
            new City
            {
                CityId = 7,
                CityName = "Grenoble",
                CityPopulation = 200000,
                CountryId = 2
            }
        };

        [HttpGet]
        public async Task<ActionResult<List<City>>> GetCities()
        {
            return Ok(cities);
        }

        [HttpGet]
        [Route(template:"CityId")]

        public async Task<ActionResult<City>> GetSingleCity(int CityID)
        {
            var city = cities.FirstOrDefault(c => c.CityId == CityID);
            if (city == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(city);
            }
        }
    }
}
