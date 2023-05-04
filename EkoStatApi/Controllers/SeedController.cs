using EkoStatApi.data.Migrations;
using EkoStatApi.Data;
using EkoStatApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EkoStatApi.Controllers;

[Route("api/seed")]
[ApiController]
public class SeedController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly EkoStatContext _ekoStatContext;

    public SeedController(IUnitOfWork unitOfWork, EkoStatContext ekoStatContext)
    {
        _unitOfWork = unitOfWork;
        _ekoStatContext = ekoStatContext;
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
        var users = new List<User>()
        {
            new User() { Name = "Andy" },
            new User() { Name = "Bella" }
        };
        await _unitOfWork.Users.AddRangeAsync(users);
        if (!await _unitOfWork.TrySaveAsync())
            throw new Exception("Fail: Seed users.");
    }

    private async Task SeedTagsAsync()
    {
        var tags = new List<Tag>()
        {
            new Tag() { Name = "Mat" , UserId = 1 },
            new Tag() { Name = "Frysvaror" , UserId = 1 },
            new Tag() { Name = "Torrvaror" , UserId = 1 },
            new Tag() { Name = "Godsaker" , UserId = 1 },
            new Tag() { Name = "Bränsle" , UserId = 2 },
            new Tag() { Name = "Transport" , UserId = 2 },
        };
        await _unitOfWork.Tags.AddRangeAsync(tags);
        if (!await _unitOfWork.TrySaveAsync())
            throw new Exception("Fail: Seed tags.");
    }

    private async Task SeedArticlesAsync()
    {
        var tags = (await _unitOfWork.Tags.GetAllMinimalAsync()) as List<Tag>;
        var articles = new List<Article>()
        {
            new Article()
            {
                Name = "Ärtor, frysta",
                Tags = new List<Tag>() { tags![0], tags[1] },
                UserId = 1,
            },
            new Article()
            {
                Name = "Nuggets, frysta",
                Tags = new List<Tag>() { tags[0], tags[1] },
                UserId = 1,
            },
            new Article()
            {
                Name = "Ris",
                Tags = new List<Tag>() { tags[0], tags[2] },
                UserId = 1,
            },
            new Article()
            {
                Name = "Makaroner",
                Tags = new List<Tag>() { tags[0], tags[2] },
                UserId = 1,
            },
            new Article()
            {
                Name = "Chips",
                Tags = new List<Tag>() { tags[0], tags[3] },
                UserId = 1,
            },
            new Article()
            {
                Name = "Bensin",
                Tags = new List<Tag>() { tags[4], tags[5] },
                UserId = 2,
            },
            new Article()
            {
                Name = "Tågbiljett",
                Tags = new List<Tag>() { tags[5] },
                UserId = 2,
            },
        };
        await _unitOfWork.Articles.AddRangeAsync(articles);
        if (!await _unitOfWork.TrySaveAsync())
            throw new Exception("Fail: Seed articles.");
    }

    private async Task SeedUnitsAsync()
    {
        var units = new List<Unit>()
        {
            new Unit() { Name = "gram", ShortName = "g" },
            new Unit() { Name = "kilo", ShortName = "kg" },
            new Unit() { Name = "liter", ShortName = "l" },
            new Unit() { Name = "styck", ShortName = "st" },
        };
        await _unitOfWork.Units.AddRangeAsync(units);
        if (!await _unitOfWork.TrySaveAsync())
            throw new Exception("Fail: Seed units.");
    }

    private async Task SeedEntriesAsync()
    {
        var timestamp1a = new DateTime(2023, 5, 2, 11, 45, 0);
        var timestamp1b = new DateTime(2023, 5, 4, 12, 10, 0);
        var timestamp2 = new DateTime(2023, 5, 6, 8, 0, 0);
        var entries = new List<Entry>()
        {
            
            new Entry()
            {
                Comment = "Extrapris",
                Timestamp = timestamp1a,
                Count = 1.5,
                CostPerArticle = 20,
                ArticleId = 1,
                UnitId = 2,
                UserId = 1,
            },
            new Entry()
            {
                Comment = "Extrapris",
                Timestamp = timestamp1a,
                Count = 1,
                CostPerArticle = 40,
                ArticleId = 4,
                UnitId = 4,
                UserId = 1,
            },
            new Entry()
            {
                Comment = "",
                Timestamp = timestamp1a,
                Count = 500,
                CostPerArticle = 35,
                ArticleId = 3,
                UnitId = 1,
                UserId = 1,
            },
            new Entry()
            {
                Comment = "Dill",
                Timestamp = timestamp1b,
                Count = 275,
                CostPerArticle = 25,
                ArticleId = 5,
                UnitId = 1,
                UserId = 1,
            },
            new Entry()
            {
                Comment = "Sour cream",
                Timestamp = timestamp1b,
                Count = 275,
                CostPerArticle = 25,
                ArticleId = 5,
                UnitId = 1,
                UserId = 1,
            },
            new Entry()
            {
                Comment = "",
                Timestamp = timestamp2,
                Count = 44.8,
                CostPerArticle = 22.17m,
                ArticleId = 6,
                UnitId = 3,
                UserId = 2,
            },
            new Entry()
            {
                Comment = "Tågbiljett",
                Timestamp = timestamp2,
                Count = 1,
                CostPerArticle = 410,
                ArticleId = 6,
                UnitId = 4,
                UserId = 2,
            },
            new Entry()
            {
                Comment = "Parkering",
                Timestamp = timestamp2,
                Count = 1,
                CostPerArticle = 80,
                ArticleId = 6,
                UnitId = 4,
                UserId = 2,
            },
        };
        await _unitOfWork.Entries.AddRangeAsync(entries);
        if (!await _unitOfWork.TrySaveAsync())
            throw new Exception("Fail: Seed entries.");
    }
}
