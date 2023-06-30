using Microsoft.AspNetCore.Identity;
using Services.AuthApi.Data;
using Services.AuthApi.Models;
using Services.AuthApi.Models.Dto;

namespace Services.AuthApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthService(ApplicationDbContext _context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> _userManager, IJwtTokenGenerator jwtTokenGenerator)
        {

            _roleManager = roleManager;
            this._userManager = _userManager;
            this._context = _context;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole { Name = roleName }).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
            }
            return true;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto requestDto)
        {
            var user = await _userManager.FindByNameAsync(requestDto.UserName);
            if (user == null)
                return new LoginResponseDto();

            var passwordValidation = await _userManager.CheckPasswordAsync(user, requestDto.Password);
            if (!passwordValidation)
                return new LoginResponseDto();
            UserDto userDto = new()
            {
                Name = user.Name,
                Email = user.Email,
                ID = user.Id,
                PhoneNumber = user.PhoneNumber,
            };
            var LoginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = _jwtTokenGenerator.GenerateToken(user),

            };
            return LoginResponseDto;
        }
        public async Task<string> Register(RegistrationRequestDto requestDto)
        {
            string msg = string.Empty;
            ApplicationUser user = new()
            {
                Email = requestDto.Email,
                Name = requestDto.Name,
                PhoneNumber = requestDto.PhoneNumber,
                NormalizedEmail = requestDto.Email.ToUpperInvariant(),
                UserName = requestDto.Name,
            };
            try
            {
                var result = await _userManager.CreateAsync(user, requestDto.Password);
                if (result.Succeeded)
                {
                    var userToreturn = await _userManager.FindByEmailAsync(requestDto.Email);
                    var userDto = new UserDto
                    {
                        Email = userToreturn.Email,
                        ID = userToreturn.Id,
                        Name = userToreturn.Name,
                        PhoneNumber = userToreturn.PhoneNumber,
                    };
                    return msg;
                }
                else
                {
                    return result.Errors.FirstOrDefault()?.Description.ToString();
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;

        }
    }
}
