using ECommerce.Repository.Helper;
using MedicalApp.DTO;
using MedicalApp.Identity;
using MedicalApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicalApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
          
        private readonly UserManager<UserApp> _userManager;
        private readonly SignInManager<UserApp> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<UserApp> userManager,SignInManager<UserApp> signInManager,IConfiguration configuration,ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDTO registerDTO)
        {
            var emailExists = await CheckEmailExists(registerDTO.Email);

            if (emailExists is OkObjectResult resul && (bool)resul.Value)
            {
                return BadRequest("The Email is already in use");
            }
            if (registerDTO.Password != registerDTO.confirmPassword)
            {
                return BadRequest("Password and Confirm Password donot the same ");
            }

            var user = new UserApp
            {
                UserName = registerDTO.Email.Split("@")[0],
                Email = registerDTO.Email,
                FName = registerDTO.FName,
                Phone = registerDTO.Phone,
                LName = registerDTO.LName,
                Age=registerDTO.Age,
                NationaID=registerDTO.NationaID,
                gender = (UserApp.Gender)registerDTO.gender,

            };
            user.File = Files.UploadFile(registerDTO.File, "User");
            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            var res = new TokenDTO()
            {
                Email = registerDTO.Email,

                Token = await _tokenService.CreateToken(user)
            };
            return Ok(res);
        }




        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LogInDTO loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized("Invalid email or password");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized("Invalid email or password");

            var res = new TokenDTO()
            {
                Email = loginDto.Email,

                Token = await _tokenService.CreateToken(user)
            };
            return Ok(res);
        }
        [HttpGet("CheckEmailExists")]
        public async Task<IActionResult> CheckEmailExists(string email)
        {
            var res = await _userManager.FindByEmailAsync(email);
            return Ok(res is not null);
        }
       

    }
}
