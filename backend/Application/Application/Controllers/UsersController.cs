namespace Application.Controllers;

using Application.Models.Users;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;
    private IMapper _mapper;

    public UsersController(
        IUserService userService,
        IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _userService.GetAll();
        return users != null ? Ok(users) : NotFound();
    }

    [HttpGet]
    [Route("names")]
    public IActionResult GetUsersNames()
    {
        var usersNames = _userService.GetAllNames();
        return Ok(usersNames);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetUser([FromRoute] Guid id)
    {
        var user = _userService.GetById(id);
        return Ok(user);
    }

    [HttpPost]
    public IActionResult AddUser(CreateUserRequest model)
    {
        _userService.Create(model);
        return Ok(new { message = "User created" });
    }

    [HttpPut]
    [Route("{id:guid}")]
    public IActionResult UpdateUser([FromRoute] Guid id, UpdateUserRequest model)
    {
        _userService.Update(id, model);
        return Ok(new { message = "User updated" });
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public IActionResult DeleteUser([FromRoute] Guid id)
    {
        _userService.Delete(id);
        return Ok(new { message = "User deleted" });
    }
}