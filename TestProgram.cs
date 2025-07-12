using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

public record BogApiResponse
{
    [JsonPropertyName("date")]
    public DateTime Date { get; init; }
    
    [JsonPropertyName("currencies")]
    public List<CurrencyRate> Currencies { get; init; } = new();
}

public record CurrencyRate
{
    [JsonPropertyName("code")]
    public string Code { get; init; } = string.Empty;
    
    [JsonPropertyName("quantity")]
    public int Quantity { get; init; }
    
    [JsonPropertyName("rateFormated")]
    public decimal RateFormated { get; init; }
    
    [JsonPropertyName("rate")]
    public decimal Rate { get; init; }
    
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
}

class TestProgram
{
    static async Task Main()
    {
        var client = new HttpClient();
        var url = "https://nbg.gov.ge/gw/api/ct/monetarypolicy/currencies/en/json";

        Console.WriteLine($"Fetching from: {url}");

        var response = await client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        var bogResponses = JsonSerializer.Deserialize<List<BogApiResponse>>(content, jsonOptions);

        if (bogResponses != null && bogResponses.Count > 0)
        {
            var latestResponse = bogResponses[0];
            Console.WriteLine($"Date: {latestResponse.Date}");
            Console.WriteLine($"Currencies count: {latestResponse.Currencies.Count}");
            
            foreach (var rate in latestResponse.Currencies.Take(5))
            {
                Console.WriteLine($"{rate.Code}: {rate.Name} - {rate.Rate} (per {rate.Quantity})");
            }
            
            // Find USD rate
            var usdRate = latestResponse.Currencies.FirstOrDefault(r => r.Code == "USD");
            if (usdRate != null)
            {
                Console.WriteLine($"\nUSD Rate: {usdRate.Rate} per {usdRate.Quantity}");
                var amountInUSD = 100m;
                var amountInGEL = amountInUSD * usdRate.Rate / usdRate.Quantity;
                Console.WriteLine($"${amountInUSD} = ₾{amountInGEL:N2}");
            }
        }
    }
}