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

    public TagController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }



    [HttpGet("only/all")]
    public async Task<ActionResult<List<TagResponseDto>>> GetAllOnlyAsync()
    {
        var tags = await _unitOfWork.Tags.GetAllOnlyAsync();
        var dtos = _mapper.Map<List<TagResponseDto>>(tags);

        return Ok(dtos);
    }

    [HttpGet("only/{id}")]
    public async Task<ActionResult<TagResponseDto>> GetOnlyAsync(int id)
    {
        var tag = await _unitOfWork.Tags.GetOnlyAsync(id);
        var dto = _mapper.Map<TagResponseDto>(tag);

        return (dto != null)
            ? Ok(dto) // 200
            : NotFound($"Fail: Find tag with id '{id}'."); // 404
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TagResponseDto>> GetAsync(int id)
    {
        var tag = await _unitOfWork.Tags.GetAsync(id);
        var dto = _mapper.Map<TagResponseDto>(tag);

        return (dto != null)
            ? Ok(dto) // 200
            : NotFound($"Fail: Find tag with id '{id}'."); // 404
    }

    [HttpGet("ByArticle/{articleId}")]
    public async Task<ActionResult<List<TagResponseDto>>> GetByArticleAsync(int articleId)
    {
        var tags = await _unitOfWork.Tags.GetByArticleAsync(articleId);
        var dtos = _mapper.Map<List<TagResponseDto>>(tags);

        return Ok(dtos);
    }

    [HttpGet("ByUser/{userId}")]
    public async Task<ActionResult<List<TagResponseDto>>> GetByUserAsync(int userId)
    {
        var tags = await _unitOfWork.Tags.GetByUserAsync(userId);
        var dtos = _mapper.Map<List<TagResponseDto>>(tags);

        return Ok(dtos);
    }



    [HttpPost]
    public async Task<ActionResult> CreateAsync(TagRequestDto dto)
    {
        var tag = _mapper.Map<Tag>(dto);
        await _unitOfWork.Tags.AddAsync(tag);

        return (await _unitOfWork.TrySaveAsync())
            ? StatusCode(201, tag.Id) // Created
            : StatusCode(500, "Fail: Create tag."); // Internal server error
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(int id, TagRequestDto dto)
    {
        var tag = await _unitOfWork.Tags.GetOnlyAsync(id);
        if (tag == null)
            return NotFound($"Fail: Find tag with id '{id}' to update.");
        _mapper.Map(dto, tag);

        return (await _unitOfWork.TrySaveAsync())
            ? NoContent() // 204
            : StatusCode(500, $"Fail: Update tag with id '{id}'."); // Internal server error
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var tag = await _unitOfWork.Tags.GetOnlyAsync(id);
        if (tag == null)
            return NotFound($"Fail: Find tag with id '{id}' to delete.");
        _unitOfWork.Tags.Remove(tag);

        return (await _unitOfWork.TrySaveAsync())
            ? NoContent() // 204
            : StatusCode(500, $"Fail: Delete tag with id '{id}'."); // Internal server error
    }
}
