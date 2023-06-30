using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.AuthApi.Data;
using Services.AuthApi.Models.Dto;
using Services.AuthApi.Services;

namespace Services.AuthApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        protected ResponseDto _responseDto;
        public AuthApiController(IAuthService _authService, IJwtTokenGenerator jwtTokenGenerator)
        {
            this._authService = _authService;
            this._responseDto = new ResponseDto();
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var msg = await _authService.Register(registrationRequestDto);
            if(string.IsNullOrWhiteSpace(msg))
            {
                msg = "User Created Successfully";
                _responseDto.IsSucess = true;
                _responseDto.Message = msg;
                return Ok(_responseDto);
            }
            _responseDto.IsSucess = false;
            _responseDto.Message = msg;
            return BadRequest(_responseDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var login = await _authService.Login(loginRequestDto);
            if (login.User == null)
            {
                _responseDto.IsSucess = false;
                _responseDto.Message = "Invalid username and password combination";
                return BadRequest(_responseDto);
            }
            _responseDto.Result = login;
            _responseDto.Message = "Login was successful";
            return Ok(_responseDto);
        }
        [HttpPost("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var role = await _authService.AssignRole(registrationRequestDto.Email, registrationRequestDto.Role.ToUpper());
            if (!role)
            {
                _responseDto.IsSucess = false;
                _responseDto.Message = "Error occured while assigning user to role";
                return BadRequest(_responseDto);
            }
            _responseDto.Message = "role successfully assigned to user";
            return Ok(_responseDto);
        }
    }
}
