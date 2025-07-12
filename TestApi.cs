using System;
using System.Net.Http;
using System.Threading.Tasks;

class TestApi
{
    static async Task Main()
    {
        using var client = new HttpClient();
        var url = "https://nbg.gov.ge/gw/api/ct/monetarypolicy/currencies/en/json";
        
        try
        {
            Console.WriteLine($"Fetching from: {url}");
            var response = await client.GetAsync(url);
            Console.WriteLine($"Status: {response.StatusCode}");
            
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Content length: {content.Length}");
            Console.WriteLine($"First 1000 chars:\n{content.Substring(0, Math.Min(1000, content.Length))}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}