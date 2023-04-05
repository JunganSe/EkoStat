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
    private readonly ILogger<UnitController> _logger;

    public UnitController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UnitController> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }



    [HttpGet("Minimal/all")]
    public async Task<ActionResult<List<UnitResponseDto>>> GetAllMinimal()
    {
        try
        {
            var units = await _unitOfWork.Units.GetAllMinimalAsync();
            var dtos = _mapper.Map<List<UnitResponseDto>>(units);

            return Ok(dtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Get units from database.");
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpGet("Minimal/{id}")]
    public async Task<ActionResult<UnitResponseDto>> GetMinimal(int id)
    {
        try
        {
            var unit = await _unitOfWork.Units.GetMinimalAsync(id);
            var dto = _mapper.Map<UnitResponseDto>(unit);

            return (dto != null)
                ? Ok(dto) // 200
                : NotFound($"Fail: Find unit with id '{id}'."); // 404
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Get unit with id '{id}' from database.", id);
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UnitResponseDto>> Get(int id)
    {
        try
        {
            var unit = await _unitOfWork.Units.GetAsync(id);
            var dto = _mapper.Map<UnitResponseDto>(unit);

            return (dto != null)
                ? Ok(dto) // 200
                : NotFound($"Fail: Find unit with id '{id}'."); // 404
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Get unit with id '{id}' from database.", id);
            return StatusCode(500, ex.Message); // Internal server error
        }
    }



    [HttpPost]
    public async Task<ActionResult> Create(UnitRequestDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400

            var unit = _mapper.Map<Unit>(dto);
            await _unitOfWork.Units.AddAsync(unit);

            return (await _unitOfWork.TrySaveAsync())
                ? StatusCode(201, unit.Id) // Created
                : StatusCode(500, "Fail: Create course."); // Internal server error
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Create new unit in database.");
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UnitRequestDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400

            var unit = await _unitOfWork.Units.GetMinimalAsync(id);
            if (unit == null)
                return NotFound($"Fail: Find unit with id '{id}' to update.");
            _mapper.Map(dto, unit);

            return (await _unitOfWork.TrySaveAsync())
                ? NoContent() // 204
                : StatusCode(500, $"Fail: Update unit with id '{id}'."); // Internal server error
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Update unit with id '{id}' in database.", id);
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var unit = await _unitOfWork.Units.GetMinimalAsync(id);
            if (unit == null)
                return NotFound($"Fail: Find unit with id '{id}' to delete.");
            _unitOfWork.Units.Remove(unit);

            return (await _unitOfWork.TrySaveAsync())
                ? NoContent() // 204
                : StatusCode(500, $"Fail: Delete unit with id '{id}'."); // Internal server error
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Delete unit with id '{id}' from database.", id);
            return StatusCode(500, ex.Message); // Internal server error
        }
    }
}
