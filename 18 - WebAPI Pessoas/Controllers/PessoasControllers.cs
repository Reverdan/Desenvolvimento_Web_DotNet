using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasControllers : ControllerBase
    {
        [HttpPost]
        public IActionResult Teste([FromBody] StringContent data)
        {
            return Ok("ok");
        }
    }
}
