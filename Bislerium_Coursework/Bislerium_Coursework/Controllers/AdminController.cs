using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bislerium_Coursework.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")] 
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet("users")]

        public IEnumerable<string> Get() 
        {
            return new List<string> { "Ayush", "Nischal", "Aryan" };
        }
    }
}
