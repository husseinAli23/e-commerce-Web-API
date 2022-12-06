using ecommerce_project.Data;
using ecommerce_project.Interface;
using ecommerce_project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private IConfiguration _config;
        private readonly DatabaseContext _context;
        private readonly IdentityUser _userManager;
        private readonly UserController _userController;

        public TokenController(IConfiguration config, DatabaseContext context, IdentityUser userManager, UserController userController)
        {
            _config = config;
            _context = context;
            _userManager = userManager;
            _userController = userController;
        }

        [HttpPost]

        public async Task<IActionResult> Register([FromBody] User _userData)
        {
            var userExists = _userController.GetUserByEmail(_userData.Email);

            if (userExists != null)
            {
               // return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = “Error”, Message = “User already exists!” });
            }

            IdentityUser user = new IdentityUser()
            {
                Email = _userData.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = _userData.Username
            };

            return Ok();
        }


    }
}
