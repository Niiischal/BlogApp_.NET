using Bislerium_Coursework.Data;
using Bislerium_Coursework.Model;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bislerium_Coursework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext appDbContext;

        public BlogController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        // GET: api/Blog
        [HttpGet]
        public async Task<IActionResult> GetBlogs()
        {
            var blogs = await appDbContext.Blogs.ToListAsync();
            return Ok(blogs);
        }

        // GET: api/Blog/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlog(int id)
        {
            var blog = await appDbContext.Blogs.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            return Ok(blog);
        }

        // POST: api/Blog
        [HttpPost]
        public async Task<IActionResult> PostBlog([FromBody] Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            appDbContext.Blogs.Add(blog);
            await appDbContext.SaveChangesAsync();

            return CreatedAtAction("GetBlog", new { id = blog.BlogId }, blog);
        }

        // PUT: api/Blog/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlog(int id, [FromBody] Blog blog)
        {
            if (id != blog.BlogId)
            {
                return BadRequest();
            }

            appDbContext.Entry(blog).State = EntityState.Modified;

            try
            {
                await appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Blog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var blog = await appDbContext.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            appDbContext.Blogs.Remove(blog);
            await appDbContext.SaveChangesAsync();

            return Ok(blog);
        }

        private bool BlogExists(int id)
        {
            return appDbContext.Blogs.Any(e => e.BlogId == id);
        }
    }
}
