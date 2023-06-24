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

        #region Creation/Login
        [AllowAnonymous]
        [HttpPost("new")]
        public IActionResult Create([FromBody] CreateUserDTO dto)
        {
            try
            {
                var token = _appService.Create(dto);
                if (token == null)
                {
                    return Conflict("A user with this username already exists!");
                }
                return StatusCode(201, token);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong. Please try again!");
            }
        }

        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login([FromQuery]string username, [FromQuery] string password)
        {
            var token = _appService.Login(new UserLoginDTO(username, password));
            if (token == null)
            {
                return NotFound("Invalid data!");
            }
            return Ok(token);
        }
        #endregion

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

        #region Message request
        [HttpPost("request")]
        public IActionResult RequestMessage([FromQuery] string requester, [FromQuery] string receiver, [FromQuery] string message)
        {
            var response = _appService.RequestMessage(requester, receiver, message);
            if (response != null)
            {
                return Conflict(response);
            }

            return StatusCode(201);
        }

        #endregion


    }
}
