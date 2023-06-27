using Microsoft.AspNetCore.Mvc;

namespace Insightinator.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetHelloWorldAsync()
    {
        await Task.Delay(200);
        return Ok("Hello world");
    }

    [HttpGet("error")]
    public async Task<IActionResult> ThrowErrorAsync()
    {
        await Task.Delay(500);
        throw new Exception("Error occured.....");
    }
}
