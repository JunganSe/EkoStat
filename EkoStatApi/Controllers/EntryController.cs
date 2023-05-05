using AutoMapper;
using EkoStatApi.Data;
using EkoStatLibrary.Dtos;
using EkoStatApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace EkoStatApi.Controllers;

[Route("api/entries")]
[ApiController]
public class EntryController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<EntryController> _logger;

    public EntryController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<EntryController> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }



    #region Read

    [HttpGet("{id}")]
    public async Task<ActionResult<EntryResponseDto>> Get(int id)
    {
        try
        {
            var entry = await _unitOfWork.Entries.GetAsync(id);
            var dto = _mapper.Map<EntryResponseDto>(entry);

            return (dto != null)
                ? Ok(dto) // 200
                : NotFound($"Fail: Find entry with id '{id}'."); // 404
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Get entry with id '{id}' from database.", id);
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpGet("ByArticle/{articleId}")]
    public async Task<ActionResult<List<EntryResponseDto>>> GetByArticle(int articleId)
    {
        try
        {
            var entries = await _unitOfWork.Entries.GetByArticleAsync(articleId);
            var dtos = _mapper.Map<List<EntryResponseDto>>(entries);

            return Ok(dtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Get entries from database.");
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpGet("ByTag/{tagId}")]
    public async Task<ActionResult<List<EntryResponseDto>>> GetByTag(int tagId)
    {
        try
        {
            var entries = await _unitOfWork.Entries.GetByTagAsync(tagId);
            var dtos = _mapper.Map<List<EntryResponseDto>>(entries);

            return Ok(dtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Get entries from database.");
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpGet("ByUser/{userId}")]
    public async Task<ActionResult<List<EntryResponseDto>>> GetByUser(int userId)
    {
        try
        {
            var entries = await _unitOfWork.Entries.GetByTagAsync(userId);
            var dtos = _mapper.Map<List<EntryResponseDto>>(entries);

            return Ok(dtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Get entries from database.");
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpPost("Filtered/{userId}")]
    public async Task<ActionResult<List<EntryResponseDto>>> GetFiltered(int userId, EntryFilterRequestDto filter)
    {
        try
        {
            var entries = (await _unitOfWork.Entries.GetByUserAsync(userId)).ToList();
            entries = FilterEntries(entries, filter);
            var dtos = _mapper.Map<List<EntryResponseDto>>(entries);

            return Ok(dtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Get entries from database.");
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    #endregion



    #region CUD

    [HttpPost]
    public async Task<ActionResult> Create(EntryRequestDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400

            var article = await _unitOfWork.Articles.GetAsync(dto.ArticleId);
            if (article == null)
                return BadRequest("Can not find article."); // 400
            if (article.UserId != dto.UserId)
                return BadRequest("Article and entry must have same user."); // 400

            var entry = _mapper.Map<Entry>(dto);
            await _unitOfWork.Entries.AddAsync(entry);

            return (await _unitOfWork.TrySaveAsync())
                ? StatusCode(201, entry.Id) // Created
                : StatusCode(500, "Fail: Create entry."); // Internal server error
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Create new entry in database.");
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpPost("Multiple")]
    public async Task<ActionResult> CreateMultiple(List<EntryRequestDto> dtos)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400

            foreach (var dto in dtos)
            {
                var article = await _unitOfWork.Articles.GetAsync(dto.ArticleId);
                if (article == null)
                    return BadRequest("Can not find article."); // 400
                if (article.UserId != dto.UserId)
                    return BadRequest("Article and entry must have same user."); // 400
            }

            var entries = _mapper.Map<List<Entry>>(dtos);
            await _unitOfWork.Entries.AddRangeAsync(entries);

            if (!await _unitOfWork.TrySaveAsync())
                return StatusCode(500, "Fail: Create entry."); // Internal server error
            var ids = entries.Select(u => u.Id).ToList();
            return StatusCode(201, ids); // Created
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Create new entry in database.");
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, EntryRequestDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400

            var entry = await _unitOfWork.Entries.GetMinimalAsync(id);
            if (entry == null)
                return NotFound($"Fail: Find entry with id '{id}' to update."); // 404

            var article = await _unitOfWork.Articles.GetMinimalAsync(dto.ArticleId);
            if (article == null)
                return BadRequest("Can not find article."); // 400
            if (article.UserId != dto.UserId)
                return BadRequest("Article and entry must have same user."); // 400

            _mapper.Map(dto, entry);

            return (await _unitOfWork.TrySaveAsync())
                ? NoContent() // 204
                : StatusCode(500, $"Fail: Update entry with id '{id}'."); // Internal server error
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Update entry with id '{id}' in database.", id);
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var entry = await _unitOfWork.Entries.GetMinimalAsync(id);
            if (entry == null)
                return NotFound($"Fail: Find entry with id '{id}' to delete."); // 404
            _unitOfWork.Entries.Remove(entry);

            return (await _unitOfWork.TrySaveAsync())
                ? NoContent() // 204
                : StatusCode(500, $"Fail: Delete entry with id '{id}'."); // Internal server error
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Delete entry with id '{id}' from database.", id);
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    #endregion



    private List<Entry> FilterEntries(List<Entry> entries, EntryFilterRequestDto filter)
    {
        FilterByArticles();
        FilterByTags();
        FilterByPrice();
        FilterByTimestamps();
        return entries;



        void FilterByArticles()
        {
            if (filter.ArticleIds != null && filter.ArticleIds.Count > 0)
            {
                entries = entries
                    .Where(e => filter.ArticleIds.Contains(e.ArticleId))
                    .ToList();
            }
        }

        void FilterByTags()
        {
            if (filter.MustHaveAllTags != null
            && filter.TagIds != null
            && filter.TagIds.Count > 0)
            {
                if ((bool)filter.MustHaveAllTags) // Kolla om artiklen i entry har alla taggar.
                {
                    entries = entries
                        .Where(entry => filter.TagIds
                            .All(filterTagId => entry.Article.Tags
                                .Any(tag => tag.Id == filterTagId)))
                        .ToList();
                }
                else // Kolla om artiklen i entry har någon av taggarna.
                {
                    entries = entries
                        .Where(entry => filter.TagIds
                            .Any(filterTagId => entry.Article.Tags
                                .Any(tag => tag.Id == filterTagId)))
                        .ToList();
                }
            }
        }

        void FilterByPrice()
        {
            if (filter.PriceMin != null)
                entries = entries
                    .Where(entry => entry.CostPerArticle >= filter.PriceMin)
                    .ToList();
            if (filter.PriceMax != null)
                entries = entries
                    .Where(entry => entry.CostPerArticle <= filter.PriceMax)
                    .ToList();
        }

        void FilterByTimestamps()
        {
            if (filter.TimestampFrom != null)
                entries = entries
                    .Where(e => e.Timestamp >= filter.TimestampFrom)
                    .ToList();
            if (filter.TimestampUntil != null)
                entries = entries
                    .Where(e => e.Timestamp <= filter.TimestampUntil)
                    .ToList();
        }
    }
}
