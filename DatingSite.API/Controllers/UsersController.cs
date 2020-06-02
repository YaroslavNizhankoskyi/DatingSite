using System.Text;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DatingSite.API.Data;
using DatingSite.API.Dtos;
using DatingSite.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;


namespace DatingSite.API.Controllers {
    [ServiceFilter(typeof(LogUserActivity))]
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
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams) {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userFromRepo = await _datingRepository.GetUser(currentUserId);

            userParams.UserId = currentUserId;

            if (string.IsNullOrEmpty(userParams.Gender))
            {
                userParams.Gender = userFromRepo.Gender == "male" ? "female" : "male";
            }

            var users = await _datingRepository.GetUsers(userParams);

            var usersToReturn = _mapper.Map<IEnumerable<UserForList>>(users);

            Response.AddPagination(users.CurrentPage, users.PageSize,
                 users.TotalCount, users.TotalPages);

            return Ok(usersToReturn);
        }

        [AllowAnonymous]
        [HttpGet ("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _datingRepository.GetUser(id);

            var model = _mapper.Map<UserProfile>(user);
            return Ok(model);  
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userUpdate)
        {
            if(id != int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier).Value))
                {
                    return Unauthorized();
                }            
            var userFromRepo = await _datingRepository.GetUser(id);
            _mapper.Map(userUpdate, userFromRepo);

            if(await _datingRepository.SaveAll())
            {
                return NoContent();
            }

            throw new Exception($"Updating user with {id} failed on save");

        }
    }
}