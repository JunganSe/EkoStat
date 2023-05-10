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
    private readonly int _seedEntriesCount;

    public SeedController(IUnitOfWork unitOfWork, EkoStatContext ekoStatContext, IMapper mapper, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _ekoStatContext = ekoStatContext;
        _mapper = mapper;
        _jsonOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, AllowTrailingCommas = true };
        _seedLocation = configuration.GetValue<string>("SeedFilesLocation");
        _seedEntriesCount = configuration.GetValue<int>("SeedEntriesCount");
    }



    [HttpPost]
    public async Task<ActionResult> SeedAsync()
    {
        try
        {
            await ResetDatabaseAsync();
            await SeedUnitsAsync();
            await SeedUsersAsync();
            await SeedTagsAsync();
            await SeedArticlesAsync();
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

    private async Task SeedUnitsAsync()
    {
        string jsonData = await System.IO.File.ReadAllTextAsync($"{_seedLocation}/Units.json");
        var units = JsonSerializer.Deserialize<List<Unit>>(jsonData, _jsonOptions);

        await _unitOfWork.Units.AddRangeAsync(units!);
        if (!await _unitOfWork.TrySaveAsync())
            throw new Exception("Fail: Seed units.");
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
        string jsonData = await System.IO.File.ReadAllTextAsync($"{_seedLocation}/TagNames.json");
        var tagNames = JsonSerializer.Deserialize<List<string>>(jsonData, _jsonOptions);

        var tags = new List<Tag>();
        foreach ( var tagName in tagNames!)
            tags.Add(new Tag() { Name = tagName, UserId = 1 });


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
            article.UserId = 1;
            articles.Add(article);
        }

        await _unitOfWork.Articles.AddRangeAsync(articles);
        if (!await _unitOfWork.TrySaveAsync())
            throw new Exception("Fail: Seed articles.");
    }

    private async Task SeedEntriesAsync()
    {
        var random = new Random();
        var articles = (await _unitOfWork.Articles.GetByUserAsync(1)).ToList();
        var units = (await _unitOfWork.Units.GetAllMinimalAsync()).ToList();
        var comments = new string?[] { null, null, null, null, null, null, null, "This is a comment.", "Meaningful comment.", "This comment is longer than the shorter comments." };
        var timestamps = GenerateTimestamps();

        var entries = new List<Entry>();
        for (int i = 0; i < _seedEntriesCount; i++)
        {
            var newEntry = new Entry()
            {
                ArticleId = articles[random.Next(articles.Count)].Id,
                UnitId = units[random.Next(units.Count)].Id,
                UserId = 1,
                Comment = comments[random.Next(comments.Length)],
                Timestamp = timestamps[random.Next(timestamps.Count)],
                Count = random.Next(1,5),
                Size = random.Next(2, 50) / 10d,
                CostPerArticle = random.Next(5, 200)
            };
            entries.Add(newEntry);
        }

        await _unitOfWork.Entries.AddRangeAsync(entries!);
        if (!await _unitOfWork.TrySaveAsync())
            throw new Exception("Fail: Seed entries.");

        List<DateTime> GenerateTimestamps()
        {
            var min = new DateTime(2010, 01, 01, 00, 00, 00);
            var max = new DateTime(2022, 12, 31, 23, 59, 59);
            int loopCount = Math.Max(1, _seedEntriesCount / 5);
            var timestamps = new List<DateTime>();
            for (int i = 0; i < loopCount; i++)
            {
                int year = random.Next(min.Year, max.Year);
                int month = random.Next(min.Month, max.Month);
                int day = random.Next(min.Day, max.Day);
                int hour = random.Next(min.Hour, max.Hour);
                int minute = random.Next(min.Minute, max.Minute);
                int second = 0;
                var newTimestamp = (random.Next(2) == 0)
                    ? new DateTime(year, month, day, hour, minute, second)
                    : new DateTime(year, month, day, 0, 0, 0);
                timestamps.Add(newTimestamp);
            }
            return timestamps;
        }
    }
}
