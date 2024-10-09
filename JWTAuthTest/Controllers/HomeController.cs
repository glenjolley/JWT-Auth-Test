using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthTest.Controllers;

[Route("/api/")]
public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Json(new { Message = "Hello, world!" });
    }

    [Authorize(Roles = "ADMIN")]
    [HttpGet("AuthCheck")]
    public IActionResult AuthCheck()
    {
        return Json(new { Message = "Clearly this worked" });
    }
}
