using AutoMapper;
using EkoStatApi.Data;
using EkoStatApi.Dtos;
using EkoStatApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EkoStatApi.Controllers;

[Route("api/articles")]
[ApiController]
public class ArticleController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<ArticleController> _logger;

    public ArticleController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ArticleController> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }



    [HttpGet("only/ByUser/{userId}")]
    public async Task<ActionResult<List<ArticleResponseDto>>> GetOnlyByUser(int userId)
    {
        try
        {
            var articles = await _unitOfWork.Articles.GetEntitiesAsync(e => e.UserId == userId);
            var dtos = _mapper.Map<List<ArticleResponseDto>>(articles);

            return Ok(dtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Get articles from database.");
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpGet("only/{id}")]
    public async Task<ActionResult<ArticleResponseDto>> GetOnly(int id)
    {
        try
        {
            var article = await _unitOfWork.Articles.GetOnlyAsync(id);
            var dto = _mapper.Map<ArticleResponseDto>(article);

            return (dto != null)
                ? Ok(dto) // 200
                : NotFound($"Fail: Find article with id '{id}'."); // 404
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Get article with id '{id}' from database.", id);
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ArticleResponseDto>> Get(int id)
    {
        try
        {
            var article = await _unitOfWork.Articles.GetAsync(id);
            var dto = _mapper.Map<ArticleResponseDto>(article);

            return (dto != null)
                ? Ok(dto) // 200
                : NotFound($"Fail: Find article with id '{id}'."); // 404
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Get article with id '{id}' from database.", id);
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpGet("ByEntry/{entryId}")]
    public async Task<ActionResult<ArticleResponseDto>> GetByEntry(int entryId)
    {
        try
        {
            var articles = await _unitOfWork.Articles.GetByEntryAsync(entryId);
            var dtos = _mapper.Map<List<ArticleResponseDto>>(articles);

            return Ok(dtos); // 200
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Get articles from database.");
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpGet("ByTag/{tagId}")]
    public async Task<ActionResult<ArticleResponseDto>> GetByTag(int tagId)
    {
        try
        {
            var articles = await _unitOfWork.Articles.GetByTagAsync(tagId);
            var dtos = _mapper.Map<List<ArticleResponseDto>>(articles);

            return Ok(dtos); // 200
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Get articles from database.");
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpGet("ByUser/{userId}")]
    public async Task<ActionResult<ArticleResponseDto>> GetByUser(int userId)
    {
        try
        {
            var articles = await _unitOfWork.Articles.GetByUserAsync(userId);
            var dtos = _mapper.Map<List<ArticleResponseDto>>(articles);

            return Ok(dtos); // 200
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Get articles from database.");
            return StatusCode(500, ex.Message); // Internal server error
        }
    }



    [HttpPost]
    public async Task<ActionResult> Create(ArticleRequestDto dto)
    {
        try
        {
            var article = _mapper.Map<Article>(dto);
            await _unitOfWork.Articles.AddAsync(article);

            return (await _unitOfWork.TrySaveAsync())
                ? StatusCode(201, article.Id) // Created
                : StatusCode(500, "Fail: Create article."); // Internal server error
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Create new article in database.");
            return StatusCode(500, "Fail: Create article."); // Internal server error
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, ArticleRequestDto dto)
    {
        try
        {
            var article = await _unitOfWork.Articles.GetOnlyAsync(id);
            if (article == null)
                return NotFound($"Fail: Find article with id '{id}' to update.");
            _mapper.Map(dto, article);

            return (await _unitOfWork.TrySaveAsync())
                ? NoContent() // 204
                : StatusCode(500, $"Fail: Update article with id '{id}'."); // Internal server error
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Update article with id '{id}' in database.", id);
            return StatusCode(500, $"Fail: Update article with id '{id}'."); // Internal server error
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var article = await _unitOfWork.Articles.GetOnlyAsync(id);
            if (article == null)
                return NotFound($"Fail: Find article with id '{id}' to delete.");
            _unitOfWork.Articles.Remove(article);

            return (await _unitOfWork.TrySaveAsync())
                ? NoContent() // 204
                : StatusCode(500, $"Fail: Delete article with id '{id}'."); // Internal server error
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Delete article with id '{id}' from database.", id);
            return StatusCode(500, ex.Message); // Internal server error
        }
    }
}
