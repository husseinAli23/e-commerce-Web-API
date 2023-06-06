using ecommerce_project.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_project.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : Controller
{
   // private IConfiguration _config;
    private AccountRepository _accountRepository;

    public TokenController(AccountRepository accountRepository)
    {
     //   _config = config;
        _accountRepository = accountRepository;
    }

    //[HttpPost]
    //[ProducesResponseType(200)]
    //[ProducesResponseType(404)]
    //[ProducesResponseType(400)]
    //public async Task<IActionResult> Login([FromBody] UserLoginDto _userData)
    //{
    //    var userExists = _accountRepository.GetUserByEmail(_userData.Email);

    //    if (userExists == null)
    //    {
    //       return BadRequest("Your email is not recored in our system");
    //    }

    //    var a = _accountRepository.checkPasswordHash(_userData.Password, userExists.PasswordHash);


    //    if (a == null)
    //    {

    //    }
    //    //if (userExists.PasswordHash != _userData.password)
    //    //{
    //    //    return BadRequest("Email or password uncorrected");
    //    //}

    //    return Ok();
    //}




}
