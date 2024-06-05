using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AceInternBatch1.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        public IActionResult GetBlogs()
        {
            return Ok("Get Blogs");
        }
        [HttpPost]
        public IActionResult CreateBlogs()
        {
            return Ok("Get Blogs");
        }
        [HttpPut]
        public IActionResult UpdateBlogs()
        {
            return Ok("UpdateBlogs");
        }
        [HttpPatch]
        public IActionResult PatchBlogs()
        {
            return Ok("PatchBlogs");
        }
        [HttpDelete]
        public IActionResult DeleteBlogs()
        {
            return Ok("DeleteBlogs");
        }
    }
}
