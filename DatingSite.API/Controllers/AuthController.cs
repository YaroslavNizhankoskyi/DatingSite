using Microsoft.AspNetCore.Mvc;
using DatingSite.API.Data;
using System.Threading.Tasks;
using DatingSite.API.Models;
using DatingSite.API.Dtos;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using DatingSite.API.Helpers;
using AutoMapper;

namespace DatingSite.API.Controllers {


    [Route ("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {

        private readonly IAuthRepositry _authRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthController (IAuthRepositry _authRepository, 
        IConfiguration config, IMapper mapper) {
            
            this._authRepository = _authRepository;
            _config = config;
            _mapper = mapper;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegister)
        {
            userForRegister.Username = userForRegister.Username.ToLower();

            if(await _authRepository.UserExists(userForRegister.Username))
                return BadRequest("UserName already exists");

            var UserToCreate = _mapper.Map<User>(userForRegister);

            var createdUser = await _authRepository.Register(UserToCreate, userForRegister.Password);

            var userToReturn = _mapper.Map<UserProfile>(createdUser);

            return CreatedAtRoute("GetUser", new {controller = "Users",
            id = createdUser.Id}, userToReturn);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLogin)
        {
            var user = await _authRepository.Login(userForLogin.UserName, userForLogin.Password);

            if(user == null){
                
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString() ),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.
                            GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });                        
        }
    }

    
}