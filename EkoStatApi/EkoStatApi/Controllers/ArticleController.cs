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

    public ArticleController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }



    [HttpGet("only/all")]
    public async Task<ActionResult<List<ArticleResponseDto>>> GetAllOnlyAsync()
    {
        var articles = await _unitOfWork.Articles.GetAllOnlyAsync();
        var dtos = _mapper.Map<List<ArticleResponseDto>>(articles);

        return Ok(dtos);
    }

    [HttpGet("only/{id}")]
    public async Task<ActionResult<ArticleResponseDto>> GetOnlyAsync(int id)
    {
        var article = await _unitOfWork.Articles.GetOnlyAsync(id);
        var dto = _mapper.Map<ArticleResponseDto>(article);

        return (dto != null)
            ? Ok(dto) // 200
            : NotFound($"Fail: Find article with id '{id}'."); // 404
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ArticleResponseDto>> GetAsync(int id)
    {
        var article = await _unitOfWork.Articles.GetAsync(id);
        var dto = _mapper.Map<ArticleResponseDto>(article);

        return (dto != null)
            ? Ok(dto) // 200
            : NotFound($"Fail: Find article with id '{id}'."); // 404
    }

    [HttpGet("ByEntry/{id}")]
    public async Task<ActionResult<ArticleResponseDto>> GetByEntryAsync(int entryId)
    {
        var articles = await _unitOfWork.Articles.GetByEntryAsync(entryId);
        var dtos = _mapper.Map<List<ArticleResponseDto>>(articles);

        return Ok(dtos); // 200
    }

    [HttpGet("ByTag/{id}")]
    public async Task<ActionResult<ArticleResponseDto>> GetByTagAsync(int tagId)
    {
        var articles = await _unitOfWork.Articles.GetByTagAsync(tagId);
        var dtos = _mapper.Map<List<ArticleResponseDto>>(articles);

        return Ok(dtos); // 200
    }

    [HttpGet("ByUser/{id}")]
    public async Task<ActionResult<ArticleResponseDto>> GetByUserAsync(int userId)
    {
        var articles = await _unitOfWork.Articles.GetByUserAsync(userId);
        var dtos = _mapper.Map<List<ArticleResponseDto>>(articles);

        return Ok(dtos); // 200
    }



    [HttpPost]
    public async Task<ActionResult> CreateAsync(ArticleRequestDto dto)
    {
        var article = _mapper.Map<Article>(dto);
        await _unitOfWork.Articles.AddAsync(article);

        return (await _unitOfWork.TrySaveAsync())
            ? StatusCode(201, article.Id) // Created
            : StatusCode(500, "Fail: Create article."); // Internal server error
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(int id, ArticleRequestDto dto)
    {
        var article = await _unitOfWork.Articles.GetOnlyAsync(id);
        if (article == null)
            return NotFound($"Fail: Find article with id '{id}' to update.");
        _mapper.Map(dto, article);

        return (await _unitOfWork.TrySaveAsync())
            ? NoContent() // 204
            : StatusCode(500, $"Fail: Update article with id '{id}'."); // Internal server error
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var article = await _unitOfWork.Articles.GetOnlyAsync(id);
        if (article == null)
            return NotFound($"Fail: Find article with id '{id}' to delete.");
        _unitOfWork.Articles.Remove(article);

        return (await _unitOfWork.TrySaveAsync())
            ? NoContent() // 204
            : StatusCode(500, $"Fail: Delete article with id '{id}'."); // Internal server error
    }
}
