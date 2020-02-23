using System.Collections.Generic;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            List<Value> values = new List<Value>{
                new Value { Id = 1, Name = "One"},
                new Value { Id= 2, Name = "Two"}
            };
            return Ok(values);
        }
    }
}