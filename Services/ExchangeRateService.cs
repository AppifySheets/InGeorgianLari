using System.Net.Http.Json;
using InGeorgianLari.Models;

namespace InGeorgianLari.Services;

public interface IExchangeRateService
{
    Task<ExchangeRatesResponse?> GetCurrentRatesAsync();
}

public class ExchangeRateService(HttpClient httpClient) : IExchangeRateService
{
    private const string BogApiUrl = "https://nbg.gov.ge/gw/api/ct/monetarypolicy/currencies/en/json";
    
    public async Task<ExchangeRatesResponse?> GetCurrentRatesAsync()
    {
        try
        {
            // BOG API returns array directly, wrap it in our response model
            var rates = await httpClient.GetFromJsonAsync<List<CurrencyRate>>(BogApiUrl);
            return rates != null ? new ExchangeRatesResponse(rates) : null;
        }
        catch
        {
            return null;
        }
    }
}