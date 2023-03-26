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

    public UserController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }



    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<List<UserResponseDto>>> GetAllAsync()
    {
        var users = await _unitOfWork.Users.GetAllOnlyAsync();
        var dtos = _mapper.Map<List<UserResponseDto>>(users);

        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseDto>> GetAsync(int id)
    {
        var user = await _unitOfWork.Users.GetOnlyAsync(id);
        var dto = _mapper.Map<UserResponseDto>(user);

        return (dto != null)
            ? Ok(dto) // 200
            : NotFound($"Fail: Find user with id '{id}'."); // 404
    }



    [HttpPost]
    public async Task<ActionResult> CreateAsync(UserRequestDto dto)
    {
        var user = _mapper.Map<User>(dto);
        await _unitOfWork.Users.AddAsync(user);

        return (await _unitOfWork.TrySaveAsync())
            ? StatusCode(201, user.Id) // Created
            : StatusCode(500, "Fail: Create user."); // Internal server error
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(int id, UserRequestDto dto)
    {
        var user = await _unitOfWork.Users.GetOnlyAsync(id);
        if (user == null)
            return NotFound($"Fail: Find user with id '{id}' to update.");
        _mapper.Map(dto, user);

        return (await _unitOfWork.TrySaveAsync())
            ? NoContent() // 204
            : StatusCode(500, $"Fail: Update user with id '{id}'."); // Internal server error
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveAsync(int id)
    {
        var user = await _unitOfWork.Users.GetOnlyAsync(id);
        if (user == null)
            return NotFound($"Fail: Find user with id '{id}' to delete.");
        _unitOfWork.Users.Remove(user);

        return (await _unitOfWork.TrySaveAsync())
            ? NoContent() // 204
            : StatusCode(500, $"Fail: Delete user with id '{id}'."); // Internal server error
    }
}
