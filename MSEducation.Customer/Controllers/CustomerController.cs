using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MSEducation.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        [Authorize]

        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [Authorize]
        public string Post([FromBody] string value)
        {
            return $"I've posted some with value: {value}";
        }

        [HttpPut("{id}")]
        [Authorize]
        public string Put(int id, [FromBody] string value)
        {
            return $"I've put some with value: {value}";
        }

        [HttpDelete("{id}")]
        [Authorize]
        public string Delete(int id)
        {
            return $"I've deleted some with id: {id}";
        }
    }
}
