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



    [HttpGet("only/ByUser/{userId}")]
    public async Task<ActionResult<List<EntryResponseDto>>> GetOnlyByUserAsync(int userId)
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

    [HttpGet("only/{id}")]
    public async Task<ActionResult<EntryResponseDto>> GetOnlyAsync(int id)
    {
        try
        {
            var entry = await _unitOfWork.Entries.GetOnlyAsync(id);
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
    public async Task<ActionResult<EntryResponseDto>> GetAsync(int id)
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
    public async Task<ActionResult<List<EntryResponseDto>>> GetByArticleAsync(int articleId)
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
    public async Task<ActionResult<List<EntryResponseDto>>> GetByTagAsync(int tagId)
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
    public async Task<ActionResult<List<EntryResponseDto>>> GetByUserAsync(int userId)
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



    [HttpPost]
    public async Task<ActionResult> CreateAsync(EntryRequestDto dto)
    {
        try
        {
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
    public async Task<ActionResult> UpdateAsync(int id, EntryRequestDto dto)
    {
        try
        {
            var entry = await _unitOfWork.Entries.GetOnlyAsync(id);
            if (entry == null)
                return NotFound($"Fail: Find entry with id '{id}' to update.");
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
    public async Task<ActionResult> DeleteAsync(int id)
    {
        try
        {
            var entry = await _unitOfWork.Entries.GetOnlyAsync(id);
            if (entry == null)
                return NotFound($"Fail: Find entry with id '{id}' to delete.");
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
}
