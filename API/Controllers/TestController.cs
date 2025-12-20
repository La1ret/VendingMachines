using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("hello")]
    public IActionResult GetHello()
    {
        return Ok(new { Message = "Сервер отвечает: Соединение установлено!", Time = DateTime.Now });
    }
}

