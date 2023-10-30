using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CMS;

public class ClientApi
{
    private readonly HttpClient _http;
    private readonly string _baseRoute;

    public ClientApi(string baseRoute, HttpClient http)
    {
        _baseRoute = baseRoute;
        _http = http;
    }

    public async Task<TReturn?> GetAsync<TReturn>(string relativeUri)
    {
        const string token =
            "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjA5ZDMwMzE1LTFiN2QtNDM1Ny05NWFiLTdmNDJiZTZlMjBiYyIsInN1YiI6Imlza19raGFub3ZAb3V0bG9vay5jb20iLCJlbWFpbCI6Imlza19raGFub3ZAb3V0bG9vay5jb20iLCJqdGkiOiJmY2U3ODg5Yi0yNTRlLTQyY2QtYmRkNS00OGQwMDA0MTZmNzAiLCJuYmYiOjE2OTgzODg4MDQsImV4cCI6MTY5ODQwMzIwNCwiaWF0IjoxNjk4Mzg4ODA0fQ.YdNlXR6ntrwth1Xosc6dQw3lH11V0Cexf36F08CIEpIAsSEVV-lOIqOwlWGgh1-fst22yS0xL5QXbJbHxhtX2Q";

        var requestMessage = new HttpRequestMessage();
        requestMessage.RequestUri = new Uri($"{_baseRoute}/{relativeUri}");
        requestMessage.Method = HttpMethod.Get;
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        requestMessage.Headers.Add("x-api-key", "01FEACB94B2B4E0AB3D251622864DCBC");
        //requestMessage.Headers.Add("x-api-key", "46BB6D176C0A4B56BA67B6A65CEBDA75");

        var res = await _http.SendAsync(requestMessage);
        if (res.IsSuccessStatusCode)
        {
            return await res.Content.ReadFromJsonAsync<TReturn>();
        }

        var msg = await res.Content.ReadAsStringAsync();
        Console.WriteLine(msg);
        throw new Exception(msg);
    }

    public async Task<TReturn?> SendAsync<TReturn, TRequest>(string relativeUri, TRequest request, HttpMethod method)
    {
        const string token =
            "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjA5ZDMwMzE1LTFiN2QtNDM1Ny05NWFiLTdmNDJiZTZlMjBiYyIsInN1YiI6Imlza19raGFub3ZAb3V0bG9vay5jb20iLCJlbWFpbCI6Imlza19raGFub3ZAb3V0bG9vay5jb20iLCJqdGkiOiJmY2U3ODg5Yi0yNTRlLTQyY2QtYmRkNS00OGQwMDA0MTZmNzAiLCJuYmYiOjE2OTgzODg4MDQsImV4cCI6MTY5ODQwMzIwNCwiaWF0IjoxNjk4Mzg4ODA0fQ.YdNlXR6ntrwth1Xosc6dQw3lH11V0Cexf36F08CIEpIAsSEVV-lOIqOwlWGgh1-fst22yS0xL5QXbJbHxhtX2Q";

        var requestMessage = new HttpRequestMessage();
        requestMessage.RequestUri = new Uri($"{_baseRoute}/{relativeUri}");
        requestMessage.Method = method;
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        requestMessage.Headers.Add("x-api-key", "46BB6D176C0A4B56BA67B6A65CEBDA75");

        var jsonContent = JsonConvert.SerializeObject(request);
        requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var res = await _http.SendAsync(requestMessage);

        if (res.IsSuccessStatusCode)
        {
            return await res.Content.ReadFromJsonAsync<TReturn>();
        }

        var msg = await res.Content.ReadAsStringAsync();
        Console.WriteLine(msg);
        throw new Exception(msg);
    }
}