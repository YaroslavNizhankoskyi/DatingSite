using System.Collections.Generic;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("GetValues")]
        public ActionResult<IEnumerable<string>> GetValues()
        {
            List<Value> values = new List<Value>{
                new Value { Id = 1, Name = "One"},
                new Value { Id= 2, Name = "Two"}
            };
            return Ok(values);
        }


        [AllowAnonymous]
        [HttpGet("Value/{i}")]
        public ActionResult<IEnumerable<string>> Value(int i)
        {
            List<Value> values = new List<Value>{
                new Value { Id = 1, Name = "One"},
                new Value { Id= 2, Name = "Two"}
            };
            return Ok(values[i]);
        }

        [HttpPost("postman")]
        public ActionResult<IEnumerable<string>> Postman(string name)
        {
            return Ok(name + "Hello");
        } 
    }
}