using AutoMapper;
using EkoStatApi.Data;
using EkoStatApi.Dtos;
using EkoStatApi.Models;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("Minimal/ByUser/{userId}")]
    public async Task<ActionResult<List<EntryResponseDto>>> GetByUserMinimal(int userId)
    {
        try
        {
            var entries = await _unitOfWork.Entries.GetEntitiesAsync(e => e.UserId == userId);
            var dtos = _mapper.Map<List<EntryResponseDto>>(entries);

            return Ok(dtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Get entries from database.");
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpGet("Minimal/{id}")]
    public async Task<ActionResult<EntryResponseDto>> GetMinimal(int id)
    {
        try
        {
            var entry = await _unitOfWork.Entries.GetMinimalAsync(id);
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
            var entries = await _unitOfWork.Entries.GetByUserAsync(userId);
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



    private ICollection<Entry> FilterEntries(ICollection<Entry> entries, EntryFilterRequestDto filter)
    {
        // Artikel
        if (filter.ArticleIds != null && filter.ArticleIds.Count > 0)
            entries = (ICollection<Entry>)entries
                .Where(e => filter.ArticleIds.Contains(e.ArticleId));

        // Taggar
        if (filter.TagIds != null 
            && filter.TagIds.Count > 0
            && filter.MustHaveAllTags != null)
        {
            if ((bool)filter.MustHaveAllTags) // Artiklen i entry har alla taggar.
            {
                entries = (ICollection<Entry>)entries
                    .Where(entry => filter.TagIds
                        .All(filterTagId => entry.Article.Tags
                            .Any(tag => tag.Id == filterTagId)));
            }
            else // Artiklen i entry har någon av taggarna.
            {
                entries = (ICollection<Entry>)entries
                    .Where(entry => filter.TagIds
                        .Any(filterTagId => entry.Article.Tags
                            .Any(tag => tag.Id == filterTagId)));
            }
        }

        // Pris
        if (filter.PriceMin != null)
            entries = (ICollection<Entry>)entries
                .Where(entry => entry.CostPerArticle >= filter.PriceMin);
        if (filter.PriceMax != null)
            entries = (ICollection<Entry>)entries
                .Where(entry => entry.CostPerArticle <= filter.PriceMax);

        // Timestamp
        if (filter.TimestampFrom != null)
            entries = (ICollection<Entry>)entries
                .Where(e => e.TimeStamp >= filter.TimestampFrom);
        if (filter.TimestampUntil != null)
            entries = (ICollection<Entry>)entries
                .Where(e => e.TimeStamp <= filter.TimestampUntil);

        return entries;
    }
}
