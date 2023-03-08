using BlazorWasmBunzai.Shared;
using System.Net.Http.Json;

namespace BlazorWasmBunzai.Client.Services.CountryService
{
    public class CountryService : ICountryService
    {
        private readonly HttpClient _http;
        public CountryService(HttpClient http)
        {
            _http = http;
        }

        public List<Country> Countries { get; set; } = new List<Country>();
        public HttpClient? Http { get; }

        public async Task GetCountries()
        {
            var result = await _http.GetFromJsonAsync<List<Country>>("api/country");
            if (result != null)
                Countries = result;
        }
        public async Task<Country> GetCountryById(int id)
        {
            var result = await _http.GetFromJsonAsync<Country>($"api/country/{id}");
            return result;
        }
        public async Task<HttpResponseMessage> CountryInsert(Country country)
        {
            var result = await _http.PostAsJsonAsync("api/country/", country);
            return result;
        }

        public async Task<HttpResponseMessage> CountryUpdate(int Countryid, Country country)
        {
            var result = await _http.PutAsJsonAsync($"api/country/{Countryid}", country);
            return result;
        }
        public async Task CountryDelete(int Countryid)
        {
            var result = await _http.DeleteAsync($"api/country/{Countryid}");
        }
        public async Task<int> CountCountriesByName(string countryName)
        {
            var result = await _http.GetFromJsonAsync<int>($"api/countryname/{countryName}");
            return result;
        }
        public async Task<int> CountCountriesByNameAndId(string countryName, int id)
        {
            var result = await _http.GetFromJsonAsync<int>($"api/countryname/{countryName}/{id}");
            return result;
        }
    }
}

