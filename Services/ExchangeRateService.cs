using System.Net.Http.Json;
using System.Text.Json;
using InGeorgianLari.Models;

namespace InGeorgianLari.Services;

public interface IExchangeRateService
{
    Task<ExchangeRatesResponse?> GetCurrentRatesAsync();
    Task<List<CurrencyRate>?> GetHistoricalRatesAsync(string currencyCode, DateTime from, DateTime to);
}

public class ExchangeRateService(HttpClient httpClient) : IExchangeRateService
{
    private const string BogApiUrl = "https://nbg.gov.ge/gw/api/ct/monetarypolicy/currencies/en/json";
    private const string BogHistApiUrl = "https://nbg.gov.ge/gw/api/ct/monetarypolicy/currencies/ka/json";
    
    public async Task<ExchangeRatesResponse?> GetCurrentRatesAsync()
    {
        try
        {
            // Log the request
            Console.WriteLine($"[ExchangeRateService] Fetching rates from: {BogApiUrl}");
            
            // Get raw response first to debug
            var response = await httpClient.GetAsync(BogApiUrl);
            var jsonContent = await response.Content.ReadAsStringAsync();
            
            Console.WriteLine($"[ExchangeRateService] Response Status: {response.StatusCode}");
            Console.WriteLine($"[ExchangeRateService] Response Length: {jsonContent.Length}");
            
            // Log first part of response to see structure
            if (jsonContent.Length > 0)
            {
                Console.WriteLine($"[ExchangeRateService] First 200 chars: {jsonContent.Substring(0, Math.Min(200, jsonContent.Length))}");
            }
            
            // Try to parse as dynamic first to see structure
            try
            {
                var jsonDoc = JsonDocument.Parse(jsonContent);
                var root = jsonDoc.RootElement;
                
                if (root.ValueKind == JsonValueKind.Array && root.GetArrayLength() > 0)
                {
                    var firstElement = root[0];
                    
                    // Check if it has a currencies property
                    if (firstElement.TryGetProperty("currencies", out var currenciesElement))
                    {
                        Console.WriteLine("[ExchangeRateService] Found 'currencies' array in response");
                        var rates = JsonSerializer.Deserialize<List<InGeorgianLari.Models.CurrencyRate>>(currenciesElement.GetRawText());
                        
                        if (rates != null)
                        {
                            Console.WriteLine($"[ExchangeRateService] Parsed {rates.Count} rates from currencies array");
                            return new ExchangeRatesResponse(rates);
                        }
                    }
                    else
                    {
                        // Try direct array parsing
                        Console.WriteLine("[ExchangeRateService] Trying direct array parsing");
                        var rates = JsonSerializer.Deserialize<List<InGeorgianLari.Models.CurrencyRate>>(jsonContent);
                        
                        if (rates != null)
                        {
                            Console.WriteLine($"[ExchangeRateService] Parsed {rates.Count} rates directly");
                            return new ExchangeRatesResponse(rates);
                        }
                    }
                }
            }
            catch (Exception parseEx)
            {
                Console.WriteLine($"[ExchangeRateService] Parse error: {parseEx.Message}");
            }
            
            // Fallback to direct parsing
            var ratesList = await httpClient.GetFromJsonAsync<List<InGeorgianLari.Models.CurrencyRate>>(BogApiUrl);
            return ratesList != null ? new ExchangeRatesResponse(ratesList) : null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ExchangeRateService] Error: {ex.Message}");
            Console.WriteLine($"[ExchangeRateService] Stack: {ex.StackTrace}");
            return null;
        }
    }
    
    public async Task<List<CurrencyRate>?> GetHistoricalRatesAsync(string currencyCode, DateTime from, DateTime to)
    {
        try
        {
            var url = $"{BogHistApiUrl}?currencies={currencyCode}&from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}";
            return await httpClient.GetFromJsonAsync<List<InGeorgianLari.Models.CurrencyRate>>(url);
        }
        catch
        {
            return null;
        }
    }
}