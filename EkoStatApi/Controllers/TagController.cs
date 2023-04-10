using AutoMapper;
using EkoStatApi.Data;
using EkoStatApi.Dtos;
using EkoStatApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EkoStatApi.Controllers;

[Route("api/tags")]
[ApiController]
public class TagController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<TagController> _logger;

    public TagController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<TagController> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }



    #region Read

    [HttpGet("Minimal/ByUser/{userId}")]
    public async Task<ActionResult<List<TagResponseDto>>> GetByUserMinimal(int userId)
    {
        try
        {
            var tags = await _unitOfWork.Tags.GetEntitiesAsync(e => e.UserId == userId);
            var dtos = _mapper.Map<List<TagResponseDto>>(tags);

            return Ok(dtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Get tags from database.");
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpGet("Minimal/{id}")]
    public async Task<ActionResult<TagResponseDto>> GetMinimal(int id)
    {
        try
        {
            var tag = await _unitOfWork.Tags.GetMinimalAsync(id);
            var dto = _mapper.Map<TagResponseDto>(tag);

            return (dto != null)
                ? Ok(dto) // 200
                : NotFound($"Fail: Find tag with id '{id}'."); // 404
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Get tag with id '{id}' from database.", id);
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TagResponseDto>> Get(int id)
    {
        try
        {
            var tag = await _unitOfWork.Tags.GetAsync(id);
            var dto = _mapper.Map<TagResponseDto>(tag);

            return (dto != null)
                ? Ok(dto) // 200
                : NotFound($"Fail: Find tag with id '{id}'."); // 404
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Get tag with id '{id}' from database.", id);
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpGet("ByArticle/{articleId}")]
    public async Task<ActionResult<List<TagResponseDto>>> GetByArticle(int articleId)
    {
        try
        {
            var tags = await _unitOfWork.Tags.GetByArticleAsync(articleId);
            var dtos = _mapper.Map<List<TagResponseDto>>(tags);

            return Ok(dtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Get tags from database.");
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpGet("ByUser/{userId}")]
    public async Task<ActionResult<List<TagResponseDto>>> GetByUser(int userId)
    {
        try
        {
            var tags = await _unitOfWork.Tags.GetByUserAsync(userId);
            var dtos = _mapper.Map<List<TagResponseDto>>(tags);

            return Ok(dtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Get tags from database.");
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    #endregion



    #region CUD

    [HttpPost]
    public async Task<ActionResult> Create(TagRequestDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400

            var tag = _mapper.Map<Tag>(dto);
            await _unitOfWork.Tags.AddAsync(tag);

            return (await _unitOfWork.TrySaveAsync())
                ? StatusCode(201, tag.Id) // Created
                : StatusCode(500, "Fail: Create tag."); // Internal server error
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Create new tag in database.");
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, TagRequestDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400

            var tag = await _unitOfWork.Tags.GetMinimalAsync(id);
            if (tag == null)
                return NotFound($"Fail: Find tag with id '{id}' to update."); // 404
            _mapper.Map(dto, tag);

            return (await _unitOfWork.TrySaveAsync())
                ? NoContent() // 204
                : StatusCode(500, $"Fail: Update tag with id '{id}'."); // Internal server error
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Update tag with id '{id}' in database.", id);
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var tag = await _unitOfWork.Tags.GetMinimalAsync(id);
            if (tag == null)
                return NotFound($"Fail: Find tag with id '{id}' to delete."); // 404
            _unitOfWork.Tags.Remove(tag);

            return (await _unitOfWork.TrySaveAsync())
                ? NoContent() // 204
                : StatusCode(500, $"Fail: Delete tag with id '{id}'."); // Internal server error
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Delete tag with id '{id}' from database.", id);
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    #endregion
}
