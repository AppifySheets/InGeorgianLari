using System.Text.Json.Serialization;

namespace InGeorgianLari.Models;

// Model for BOG API response structure
public record ExchangeRatesResponse(List<CurrencyRate> Rates);

public record CurrencyRate
{
    [JsonPropertyName("code")]
    public string Code { get; init; } = string.Empty;
    
    [JsonPropertyName("name")]
    public string Currency { get; init; } = string.Empty;
    
    [JsonPropertyName("quantity")]
    public int Quantity { get; init; }
    
    [JsonPropertyName("rateFormated")]
    public string RateFormated { get; init; } = string.Empty;
    
    [JsonPropertyName("diffFormated")]
    public string DiffFormated { get; init; } = string.Empty;
    
    [JsonPropertyName("rate")]
    public decimal Rate { get; init; }
    
    [JsonPropertyName("date")]
    public string ValidFromDate { get; init; } = string.Empty;
    
    [JsonPropertyName("orderNo")]
    public int OrderNo { get; init; }
    
    [JsonPropertyName("dgtl")]
    public decimal Dgtl { get; init; }
    
    [JsonPropertyName("mid")]
    public decimal Mid { get; init; }
}