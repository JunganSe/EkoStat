using AutoMapper;
using EkoStatApi.Data;
using EkoStatApi.Models;
using EkoStatLibrary.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace EkoStatApi.Controllers;

[Route("api/seed")]
[ApiController]
public class SeedController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly EkoStatContext _ekoStatContext;
    private readonly IMapper _mapper;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly string _seedLocation;

    public SeedController(IUnitOfWork unitOfWork, EkoStatContext ekoStatContext, IMapper mapper, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _ekoStatContext = ekoStatContext;
        _mapper = mapper;
        _jsonOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, AllowTrailingCommas = true };
        _seedLocation = configuration.GetValue<string>("SeedFilesLocation");
    }



    [HttpPost]
    public async Task<ActionResult> SeedAsync()
    {
        try
        {
            await ResetDatabaseAsync();
            await SeedUsersAsync();
            await SeedTagsAsync();
            await SeedArticlesAsync();
            await SeedUnitsAsync();
            await SeedEntriesAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
        return Ok();
    }

    private async Task ResetDatabaseAsync()
    {
        await _ekoStatContext.Database.EnsureDeletedAsync();
        await _ekoStatContext.Database.MigrateAsync();
    }

    private async Task SeedUsersAsync()
    {
        string jsonData = await System.IO.File.ReadAllTextAsync($"{_seedLocation}/Users.json");
        var users = JsonSerializer.Deserialize<List<User>>(jsonData, _jsonOptions);

        await _unitOfWork.Users.AddRangeAsync(users!);
        if (!await _unitOfWork.TrySaveAsync())
            throw new Exception("Fail: Seed users.");
    }

    private async Task SeedTagsAsync()
    {
        string jsonData = await System.IO.File.ReadAllTextAsync($"{_seedLocation}/Tags.json");
        var tags = JsonSerializer.Deserialize<List<Tag>>(jsonData, _jsonOptions);

        await _unitOfWork.Tags.AddRangeAsync(tags!);
        if (!await _unitOfWork.TrySaveAsync())
            throw new Exception("Fail: Seed tags.");
    }

    private async Task SeedArticlesAsync()
    {
        string jsonData = await System.IO.File.ReadAllTextAsync($"{_seedLocation}/Articles.json");
        var articleDtos = JsonSerializer.Deserialize<List<ArticleRequestDto>>(jsonData, _jsonOptions);

        var articles = new List<Article>();
        foreach (var dto in articleDtos!)
        {
            var article = _mapper.Map<Article>(dto);
            article.Tags = await _unitOfWork.Tags.GetByIdsAsync(dto.TagIds);
            articles.Add(article);
        }

        await _unitOfWork.Articles.AddRangeAsync(articles);
        if (!await _unitOfWork.TrySaveAsync())
            throw new Exception("Fail: Seed articles.");
    }

    private async Task SeedUnitsAsync()
    {
        string jsonData = await System.IO.File.ReadAllTextAsync($"{_seedLocation}/Units.json");
        var units = JsonSerializer.Deserialize<List<Unit>>(jsonData, _jsonOptions);

        await _unitOfWork.Units.AddRangeAsync(units!);
        if (!await _unitOfWork.TrySaveAsync())
            throw new Exception("Fail: Seed units.");
    }

    private async Task SeedEntriesAsync()
    {
        string jsonData = await System.IO.File.ReadAllTextAsync($"{_seedLocation}/Entries.json");
        var entries = JsonSerializer.Deserialize<List<Entry>>(jsonData, _jsonOptions);

        await _unitOfWork.Entries.AddRangeAsync(entries!);
        if (!await _unitOfWork.TrySaveAsync())
            throw new Exception("Fail: Seed entries.");
    }
}
