using System;
using System.Net.Http;
using System.Threading.Tasks;

var client = new HttpClient();
var url = "https://nbg.gov.ge/gw/api/ct/monetarypolicy/currencies/en/json";

Console.WriteLine($"Fetching from: {url}");
var response = await client.GetAsync(url);
Console.WriteLine($"Status: {response.StatusCode}");

var content = await response.Content.ReadAsStringAsync();
Console.WriteLine($"Content length: {content.Length}");
Console.WriteLine($"First 500 chars:");
Console.WriteLine(content.Substring(0, Math.Min(500, content.Length)));