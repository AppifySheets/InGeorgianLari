using System.Text.Json.Serialization;

namespace InGeorgianLari.Models;

// Model for blockchain.info ticker API response
public record BitcoinTickerResponse(Dictionary<string, BitcoinCurrencyData> Currencies);

public record BitcoinCurrencyData
{
    [JsonPropertyName("15m")]
    public decimal FifteenMinutes { get; init; }
    
    [JsonPropertyName("last")]
    public decimal Last { get; init; }
    
    [JsonPropertyName("buy")]
    public decimal Buy { get; init; }
    
    [JsonPropertyName("sell")]
    public decimal Sell { get; init; }
    
    [JsonPropertyName("symbol")]
    public string Symbol { get; init; } = string.Empty;
}

// Simplified model for UI consumption
public record BitcoinPrice
{
    public decimal PriceUsd { get; init; }
    public decimal PriceGel { get; init; }
    public DateTime LastUpdated { get; init; }
}