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
    {
        return new HttpClient()
        {
            BaseAddress = new Uri(_apiUrl),
            Timeout = TimeSpan.FromSeconds(Constants.Http.TimeoutSeconds)
        };
    }

    private async Task<T?> GetAsync<T>(string endpoint)
    {
        using var httpClient = GetHttpClient();
        return await httpClient.GetFromJsonAsync<T>(endpoint);
    }

    private async Task<HttpResponseMessage> PostAsync<T>(string endpoint, T entity)
    {
        using var httpClient = GetHttpClient();
        var response = await httpClient.PostAsJsonAsync(endpoint, entity);
        response.EnsureSuccessStatusCode();
        return response;
    }



    #region Entries
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

    public async Task<List<EntryResponseDto>> GetEntriesFilteredAsync(int userId, EntryFilterRequestDto filter)
    {
        string endpoint = Constants.ApiEndpoints.EntriesFiltered + userId;
        var response = await PostAsync(endpoint, filter);
        return await response.Content.ReadFromJsonAsync<List<EntryResponseDto>>() ?? new();
    }

    public async Task<HttpResponseMessage> CreateEntryAsync(EntryRequestDto entry)
    {
        string endpoint = Constants.ApiEndpoints.EntryCreate;
        return await PostAsync(endpoint, entry);
    }
    #endregion

    #region Articles
    public async Task<List<ArticleResponseDto>> GetArticlesByUserAsync(int userId)
    {
        string endpoint = Constants.ApiEndpoints.ArticlesByUser + userId;
        return await GetAsync<List<ArticleResponseDto>>(endpoint) ?? new();
    }

    public async Task<HttpResponseMessage> CreateArticleAsync(ArticleRequestDto article)
    {
        string endpoint = Constants.ApiEndpoints.ArticleCreate;
        return await PostAsync(endpoint, article);
    }
    #endregion

    #region Tags
    public async Task<List<TagResponseDto>> GetTagsByUserAsync(int userId)
    {
        string endpoint = Constants.ApiEndpoints.TagsByUser + userId;
        return await GetAsync<List<TagResponseDto>>(endpoint) ?? new();
    }

    public async Task<HttpResponseMessage> CreateTagAsync(TagRequestDto tag)
    {
        string endpoint = Constants.ApiEndpoints.TagCreate;
        return await PostAsync(endpoint, tag);
    }
    #endregion

    #region Units
    public async Task<List<UnitResponseDto>> GetAllUnitsAsync()
    {
        string endpoint = Constants.ApiEndpoints.UnitsAll;
        return await GetAsync<List<UnitResponseDto>>(endpoint) ?? new();
    }
    #endregion
}
