using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProviderAPI.Models;

namespace ProviderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BindingController : ControllerBase
    {

        [HttpGet("{age:int}/{name:alpha}")]
        public IActionResult TestPremetive(int age, string name) 
        {
            return Ok();
        }


        //[HttpGet("{age:int}/{name:alpha}/{managerName:alpha}")]
        //public IActionResult TestPremetive(int age, string name, string managerName)
        //{
        //    return Ok();
        //}


        [HttpGet("{Id:int}/{Name:alpha}/{Description:alpha}")]
        public IActionResult TestComplex([FromRoute] Department department)
        {
            return Ok();
        }
    }
}
