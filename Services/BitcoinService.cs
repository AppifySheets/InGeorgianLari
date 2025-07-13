using System.Text.Json;
using InGeorgianLari.Models;

namespace InGeorgianLari.Services;

public interface IBitcoinService
{
    Task<BitcoinPrice?> GetCurrentPriceAsync();
}

public class BitcoinService(HttpClient httpClient, IExchangeRateService exchangeRateService) : IBitcoinService
{
    private const string BlockchainApiUrl = "https://blockchain.info/ticker";
    private BitcoinPrice? _cachedPrice;
    private DateTime _lastFetchTime = DateTime.MinValue;
    private readonly TimeSpan _cacheExpiration = TimeSpan.FromSeconds(5); // Cache for 5 seconds
    
    public async Task<BitcoinPrice?> GetCurrentPriceAsync()
    {
        try
        {
            // Return cached price if still valid
            if (_cachedPrice != null && DateTime.UtcNow - _lastFetchTime < _cacheExpiration)
            {
                return _cachedPrice;
            }
            
            // Get Bitcoin price in USD from blockchain.info
            var response = await httpClient.GetStringAsync(BlockchainApiUrl);
            var bitcoinData = JsonSerializer.Deserialize<Dictionary<string, BitcoinCurrencyData>>(response);
            
            if (bitcoinData == null || !bitcoinData.ContainsKey("USD"))
            {
                Console.WriteLine("[BitcoinService] No USD data found in blockchain.info response");
                return null;
            }
            
            var btcUsd = bitcoinData["USD"].Last;
            Console.WriteLine($"[BitcoinService] BTC/USD: {btcUsd}");
            
            // Get USD to GEL rate from National Bank of Georgia
            var exchangeRates = await exchangeRateService.GetCurrentRatesAsync();
            var usdToGel = exchangeRates?.Rates?.FirstOrDefault(r => r.Code == "USD");
            
            if (usdToGel == null)
            {
                Console.WriteLine("[BitcoinService] No USD/GEL rate found");
                return null;
            }
            
            // Calculate BTC to GEL
            // USD rate from NBG is for 1 USD, so we multiply directly
            var btcGel = btcUsd * usdToGel.Rate;
            Console.WriteLine($"[BitcoinService] BTC/GEL: {btcGel} (USD/GEL rate: {usdToGel.Rate})");
            
            _cachedPrice = new BitcoinPrice
            {
                PriceUsd = btcUsd,
                PriceGel = btcGel,
                LastUpdated = DateTime.UtcNow
            };
            _lastFetchTime = DateTime.UtcNow;
            
            return _cachedPrice;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[BitcoinService] Error: {ex.Message}");
            return null;
        }
    }
}