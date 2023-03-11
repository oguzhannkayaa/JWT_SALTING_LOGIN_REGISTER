using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using net6JWT.Dtos;
using net6JWT.Model;
using net6JWT.Services.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace net6JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        public static User user = new User();
        private IConfiguration _configuration;
        private IUserService _userService;
        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {

            bool result = _userService.Register(request);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {

            string result = _userService.Login(request);
            return Ok(result);
        }

      


    }
}
