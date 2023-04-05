using AutoMapper;
using EkoStatApi.Data;
using EkoStatApi.Dtos;
using EkoStatApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EkoStatApi.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<UserController> _logger;

    public UserController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserController> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }



    #region Read

    [HttpGet("Minimal/all")]
    public async Task<ActionResult<List<UserResponseDto>>> GetAllMinimal()
    {
        try
        {
            var users = await _unitOfWork.Users.GetAllMinimalAsync();
            var dtos = _mapper.Map<List<UserResponseDto>>(users);

            return Ok(dtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Get users from database.");
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpGet("Minimal/{id}")]
    public async Task<ActionResult<UserResponseDto>> GetMinimal(int id)
    {
        try
        {
            var user = await _unitOfWork.Users.GetMinimalAsync(id);
            var dto = _mapper.Map<UserResponseDto>(user);

            return (dto != null)
                ? Ok(dto) // 200
                : NotFound($"Fail: Find user with id '{id}'."); // 404
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Get user with id '{id}' from database.", id);
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseDto>> Get(int id)
    {
        try
        {
            var user = await _unitOfWork.Users.GetAsync(id);
            var dto = _mapper.Map<UserResponseDto>(user);

            return (dto != null)
                ? Ok(dto) // 200
                : NotFound($"Fail: Find user with id '{id}'."); // 404
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Get user with id '{id}' from database.", id);
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    #endregion



    #region CUD

    [HttpPost]
    public async Task<ActionResult> Create(UserRequestDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400

            var user = _mapper.Map<User>(dto);
            await _unitOfWork.Users.AddAsync(user);

            return (await _unitOfWork.TrySaveAsync())
                ? StatusCode(201, user.Id) // Created
                : StatusCode(500, "Fail: Create user."); // Internal server error
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Create new user in database.");
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UserRequestDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400

            var user = await _unitOfWork.Users.GetMinimalAsync(id);
            if (user == null)
                return NotFound($"Fail: Find user with id '{id}' to update.");
            _mapper.Map(dto, user);

            return (await _unitOfWork.TrySaveAsync())
                ? NoContent() // 204
                : StatusCode(500, $"Fail: Update user with id '{id}'."); // Internal server error
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Update user with id '{id}' in database.", id);
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var user = await _unitOfWork.Users.GetMinimalAsync(id);
            if (user == null)
                return NotFound($"Fail: Find user with id '{id}' to delete.");
            _unitOfWork.Users.Remove(user);

            return (await _unitOfWork.TrySaveAsync())
                ? NoContent() // 204
                : StatusCode(500, $"Fail: Delete user with id '{id}'."); // Internal server error
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail: Delete user with id '{id}' from database.", id);
            return StatusCode(500, ex.Message); // Internal server error
        }
    }

    #endregion
}
