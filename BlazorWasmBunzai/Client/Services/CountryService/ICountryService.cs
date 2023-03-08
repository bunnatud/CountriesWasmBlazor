using BlazorWasmBunzai.Shared;

namespace BlazorWasmBunzai.Client.Services.CountryService
{
    public interface ICountryService
    {
        List<Country> Countries { get; set; }
        Task GetCountries();
        Task<HttpResponseMessage> CountryInsert(Country country);
        Task<Country> GetCountryById(int Countryid);
        Task<HttpResponseMessage> CountryUpdate(int Countryid, Country country);
        Task CountryDelete(int Countryid);
        Task<int> CountCountriesByName(string countryName);
        Task<int> CountCountriesByNameAndId(string countryName, int Id);
    }
}