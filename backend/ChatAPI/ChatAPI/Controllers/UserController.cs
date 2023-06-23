using Chat.Application.DTOs;
using Chat.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserAppService _appService;

        public UserController(IUserAppService appService)
        {
            _appService = appService;
        }

        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login([FromQuery]string username, [FromQuery] string password)
        {
            var token = _appService.Login(new UserLoginDTO(username, password));
            if (token == null)
            {
                return NotFound("Wrong username or password!");
            }
            return Ok(token);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _appService.GetAll();
            if (result == null || !result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _appService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("new")]
        public IActionResult Create([FromBody] CreateUserDTO dto)
        {
            var token = _appService.Create(dto);
            if (token== null)
            {
                return StatusCode(500, "Something went wrong. Please try again!");
            }
            return StatusCode(201, token);
        }
    }
}
