namespace InGeorgianLari.Models;

// Model for BOG API response structure
public record ExchangeRatesResponse(List<CurrencyRate> Rates);

public record CurrencyRate(
    string Currency,
    string Code,
    int Quantity,
    decimal RateFormated,
    decimal DiffFormated,
    decimal Rate,
    string ValidFromDate,
    int OrderNo,
    decimal Dgtl,
    decimal Mid
);