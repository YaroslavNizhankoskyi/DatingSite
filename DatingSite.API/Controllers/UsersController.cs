using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DatingSite.API.Data;
using DatingSite.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingSite.API.Controllers {
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly IDatingRepository _datingRepository;
        private readonly IMapper _mapper;

        public UsersController (IDatingRepository datingRepository, IMapper mapper) {
           
            this._datingRepository = datingRepository;
            this._mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetUsers () {
            var users = await _datingRepository.GetUsers ();

            var model = _mapper.Map<IEnumerable<UserForList>>(users);
            return Ok(model);
        }

        [AllowAnonymous]
        [HttpGet ("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _datingRepository.GetUser(id);

            var model = _mapper.Map<UserProfile>(user);
            return Ok(model);  
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userUpdate)
        {
            if(id != int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifyer).Value))
                {
                    return Unauthorized();
                }            
            var userFromRepo = await _datingRepository.GetUser(id);
            _mapper.Map(UserForUpdateDto, userfromRepo);

            if(await _repo.SaveAll())
            {
                return NoContent();
            }

            throw new Exception($"Updating user with {id} failed on save");

        }
    }
}