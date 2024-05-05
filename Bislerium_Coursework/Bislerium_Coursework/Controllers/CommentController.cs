using Bislerium_Coursework.Data;
using Bislerium_Coursework.Model;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bislerium_Coursework.DTOs;

namespace Bislerium_Coursework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly AppDbContext appDbContext;

        public CommentController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        // GET: api/Comment
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await appDbContext.Comments.ToListAsync();
            return Ok(comments);
        }

        // GET: api/Comment/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            var comment = await appDbContext.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // POST: api/Comment
        [HttpPost]
        public async Task<IActionResult> PostComment([FromBody] CommentDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //mapping dto to comment
            var comment = new Comment()
            {
                Content = request.Content,
                BlogId = request.BlogId
            };

            appDbContext.Comments.Add(comment);
            await appDbContext.SaveChangesAsync();

            return Ok(comment);
        }

        // PUT: api/Comment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, [FromBody] Comment comment)
        {
            if (id != comment.CommentId)
            {
                return BadRequest();
            }

            appDbContext.Entry(comment).State = EntityState.Modified;

            try
            {
                await appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // DELETE: api/Comment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await appDbContext.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            appDbContext.Comments.Remove(comment);
            await appDbContext.SaveChangesAsync();

            return Ok(comment);
        }

        private bool CommentExists(int id)
        {
            return appDbContext.Comments.Any(e => e.CommentId == id);
        }
    }
}