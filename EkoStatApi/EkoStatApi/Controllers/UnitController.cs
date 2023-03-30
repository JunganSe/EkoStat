using AutoMapper;
using EkoStatApi.Data;
using EkoStatApi.Dtos;
using EkoStatApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EkoStatApi.Controllers;

[ApiController]
[Route("api/units")]
public class UnitController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UnitController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }



    [HttpGet("only/all")]
    public async Task<ActionResult<List<UnitResponseDto>>> GetAllOnlyAsync()
    {
        var units = await _unitOfWork.Units.GetAllOnlyAsync();
        var dtos = _mapper.Map<List<UnitResponseDto>>(units);

        return Ok(dtos);
    }

    [HttpGet("only/{id}")]
    public async Task<ActionResult<UnitResponseDto>> GetOnlyAsync(int id)
    {
        var unit = await _unitOfWork.Units.GetOnlyAsync(id);
        var dto = _mapper.Map<UnitResponseDto>(unit);

        return (dto != null)
            ? Ok(dto) // 200
            : NotFound($"Fail: Find unit with id '{id}'."); // 404
    }



    [HttpPost]
    public async Task<ActionResult> CreateAsync(UnitRequestDto dto)
    {
        var unit = _mapper.Map<Unit>(dto);
        await _unitOfWork.Units.AddAsync(unit);

        return (await _unitOfWork.TrySaveAsync())
            ? StatusCode(201, unit.Id) // Created
            : StatusCode(500, "Fail: Create course."); // Internal server error
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(int id, UnitRequestDto dto)
    {
        var unit = await _unitOfWork.Units.GetOnlyAsync(id);
        if (unit == null)
            return NotFound($"Fail: Find unit with id '{id}' to update.");
        _mapper.Map(dto, unit);

        return (await _unitOfWork.TrySaveAsync())
            ? NoContent() // 204
            : StatusCode(500, $"Fail: Update unit with id '{id}'."); // Internal server error
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var unit = await _unitOfWork.Units.GetOnlyAsync(id);
        if (unit == null)
            return NotFound($"Fail: Find unit with id '{id}' to delete.");
        _unitOfWork.Units.Remove(unit);

        return (await _unitOfWork.TrySaveAsync())
            ? NoContent() // 204
            : StatusCode(500, $"Fail: Delete unit with id '{id}'."); // Internal server error
    }
}
