using AutoMapper;
using ecommerce_project.Dto;
using ecommerce_project.Interface;
using ecommerce_project.Models;
using ecommerce_project.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_project.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUser _userRepository;
        private readonly IMapper _mapper;


        public UserController(IUser iUser,IMapper iMapper)
        {
            _userRepository = iUser;
            _mapper = iMapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var Users = _mapper.Map<List<UserDto>>(_userRepository.getUsers());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Users);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int userId)
        {
            if (!_userRepository.UserExistsID(userId))
                return NotFound();

            var user = _mapper.Map<UserDto>(_userRepository.GetUserByID(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpGet("{email}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUserByEmail(string email)
        {
            if (!_userRepository.UserExistsEmail(email))
                return NotFound();

            var user = _mapper.Map<List<UserDto>>(_userRepository.GetUserByEmail(email));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpPut("{userID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdateCategory(int userID, [FromBody] UserDto updatedUser)
        {
            if (updatedUser == null)
                return BadRequest(ModelState);

            if (userID != updatedUser.ID)
                return BadRequest(ModelState);

            if (!_userRepository.UserExistsID(userID))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var userMap = _mapper.Map<User>(updatedUser);

            if (!_userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong when updating category");
                return StatusCode(500, ModelState);
            }

            return Ok("Updated seccessfully");
        }
    }
}
