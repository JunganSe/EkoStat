using EkoStatLibrary.Dtos;
using System.Net.Http.Json;

namespace EkoStatLibrary.Helpers;

public class ApiHandler
{
    private readonly string _apiUrl;

    public ApiHandler(string apiUrl)
    {
        _apiUrl = apiUrl;
    }

    private HttpClient GetHttpClient()
        => new HttpClient() { BaseAddress = new Uri(_apiUrl) };

    private async Task<T?> GetAsync<T>(string endpoint)
    {
        using var httpClient = GetHttpClient();
        return await httpClient.GetFromJsonAsync<T>(endpoint);
    }



    public async Task<List<TagResponseDto>> GetTagsByUserAsync(int userId)
    {
        string endpoint = Constants.ApiEndpoints.TagsByUser + userId;
        return await GetAsync<List<TagResponseDto>>(endpoint) ?? new();
    }

    public async Task<List<ArticleResponseDto>> GetArticlesByUserAsync(int userId)
    {
        string endpoint = Constants.ApiEndpoints.ArticlesByUser + userId;
        return await GetAsync<List<ArticleResponseDto>>(endpoint) ?? new();
    }

    public async Task<List<EntryResponseDto>> GetEntriesByUserAsync(int userId)
    {
        string endpoint = Constants.ApiEndpoints.EntriesByUser + userId;
        return await GetAsync<List<EntryResponseDto>>(endpoint) ?? new();
    }

    public async Task<EntryResponseDto> GetLatestEntryByUserAsync(int userId)
    {
        string endpoint = Constants.ApiEndpoints.EntryLatestByUser + userId;
        return await GetAsync<EntryResponseDto>(endpoint) ?? new();
    }

    public async Task<List<UnitResponseDto>> GetAllUnitsAsync()
    {
        string endpoint = Constants.ApiEndpoints.UnitsAll;
        return await GetAsync<List<UnitResponseDto>>(endpoint) ?? new();
    }
}
