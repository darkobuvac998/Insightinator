using Microsoft.AspNetCore.Mvc;

namespace Insightinator.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetHelloWorldAsync()
    {
        return Ok("Hello world");
    }
}
