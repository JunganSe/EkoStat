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

    public EntryController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }



    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<List<EntryResponseDto>>> GetAllAsync()
    {
        var entries = await _unitOfWork.Entries.GetAllOnlyAsync();
        var dtos = _mapper.Map<List<EntryResponseDto>>(entries);

        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EntryResponseDto>> GetAsync(int id)
    {
        var entry = await _unitOfWork.Entries.GetOnlyAsync(id);
        var dto = _mapper.Map<EntryResponseDto>(entry);

        return (dto != null)
            ? Ok(dto) // 200
            : NotFound($"Fail: Find entry with id '{id}'."); // 404
    }



    [HttpPost]
    public async Task<ActionResult> CreateAsync(EntryRequestDto dto)
    {
        var entry = _mapper.Map<Entry>(dto);
        await _unitOfWork.Entries.AddAsync(entry);

        return (await _unitOfWork.TrySaveAsync())
            ? StatusCode(201, entry.Id) // Created
            : StatusCode(500, "Fail: Create entry."); // Internal server error
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(int id, EntryRequestDto dto)
    {
        var entry = await _unitOfWork.Entries.GetOnlyAsync(id);
        if (entry == null)
            return NotFound($"Fail: Find entry with id '{id}' to update.");
        _mapper.Map(dto, entry);

        return (await _unitOfWork.TrySaveAsync())
            ? NoContent() // 204
            : StatusCode(500, $"Fail: Update entry with id '{id}'."); // Internal server error
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveAsync(int id)
    {
        var entry = await _unitOfWork.Entries.GetOnlyAsync(id);
        if (entry == null)
            return NotFound($"Fail: Find entry with id '{id}' to delete.");
        _unitOfWork.Entries.Remove(entry);

        return (await _unitOfWork.TrySaveAsync())
            ? NoContent() // 204
            : StatusCode(500, $"Fail: Delete entry with id '{id}'."); // Internal server error
    }
}
