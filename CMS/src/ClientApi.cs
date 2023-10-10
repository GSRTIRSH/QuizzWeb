using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

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
        var res = await _http.GetAsync($"{_baseRoute}/{relativeUri}");
        if (res.IsSuccessStatusCode)
        {
            return await res.Content.ReadFromJsonAsync<TReturn>();
        }
        else
        {
            var msg = await res.Content.ReadAsStringAsync();
            Console.WriteLine(msg);
            throw new Exception(msg);
        }
    }

    public async Task<TReturn?> PostAsync<TReturn, TRequest>(string relativeUri, TRequest request)
    {
        var res = await _http.PostAsJsonAsync<TRequest>($"{_baseRoute}/{relativeUri}", request);
        if (res.IsSuccessStatusCode)
        {
            return await res.Content.ReadFromJsonAsync<TReturn>();
        }
        else
        {
            var msg = await res.Content.ReadAsStringAsync();
            Console.WriteLine(msg);
            throw new Exception(msg);
        }
    }
}