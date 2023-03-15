using AutoMapper;
using ecommerce_project.Dto;
using ecommerce_project.Interface;
using ecommerce_project.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_project.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : Controller
{
    private readonly IAccount _accountRepository;
    private readonly IMapper _mapper;
    public AccountController(IAccount iAccount, IMapper iMapper)
    {
        _accountRepository = iAccount;
        _mapper = iMapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
    public IActionResult GetUsers()
    {
        var Users = _mapper.Map<List<UserDto>>(_accountRepository.getUsers());

        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(Users);
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(200, Type = typeof(User))]
    [ProducesResponseType(400)]
    public IActionResult GetUser(int userId)
    {
        if (!_accountRepository.UserExistsID(userId))
            return NotFound();

        var user = _mapper.Map<UserDto>(_accountRepository.GetUserByID(userId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(user);
    }

    [HttpGet("{email}")]
    [ProducesResponseType(200, Type = typeof(User))]
    [ProducesResponseType(400)]
    public IActionResult GetUserByEmail(string email)
    {
        if (!_accountRepository.UserExistsEmail(email))
            return NotFound();

        var user = _mapper.Map<List<UserDto>>(_accountRepository.GetUserByEmail(email));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(user);
    }

    [HttpPut("{userID}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public IActionResult UpdateUser(int userID, [FromBody] UserDto updatedUser)
    {
        if (updatedUser == null)
            return BadRequest(ModelState);

        if (userID != updatedUser.ID)
            return BadRequest(ModelState);

        if (!_accountRepository.UserExistsID(userID))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();

        var userMap = _mapper.Map<User>(updatedUser);

        if (!_accountRepository.UpdateUser(userMap))
        {
            ModelState.AddModelError("", "Something went wrong when updating your account");
            return StatusCode(500, ModelState);
        }

        return Ok("Updated seccessfully");
    }


    [HttpPost]
    [Route("Register")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public IActionResult CreateUser([FromBody] CreateUserDto newUser)
    {
        if (newUser == null)
            return BadRequest(ModelState);

        var user = _accountRepository.GetUserByUsername(newUser.Username);
        if (user != null)
        {
            ModelState.AddModelError("", "Username already exists");
            return BadRequest(ModelState);
        }
            
        if (!ModelState.IsValid)
            return BadRequest();

        newUser.Username = _accountRepository.createPasswordHash(newUser.Password);

              var userMap = _mapper.Map<User>(newUser);

        if (!_accountRepository.CreateUser(userMap))
        {
            ModelState.AddModelError("", "Something went wrong when create the account");
            return StatusCode(500, ModelState);
        }

        return Ok("create your account seccessfully");
    }


  
    [HttpPost]
    [Route("Login")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public IActionResult Login([FromBody] UserLoginDto _userData)
    {
        var userExists = _accountRepository.GetUserByEmail(_userData.Email);

        if (userExists == null)
        {
            return BadRequest("Your email is not recorded in our system");
        }

        var isPasswordMatch = _accountRepository.checkPasswordHash(_userData.Password, userExists.PasswordHash);
        if (!isPasswordMatch)
        {
            return BadRequest("Your email or password incorrect");
        }

        return Ok("create your account seccessfully");
    }
}
