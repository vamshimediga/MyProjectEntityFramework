using BusinessLayer;
using BusinessLayer.implemation;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyProjectEntityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostAPIController : ControllerBase
    {
        private readonly IPosts _postRepository;

        public PostAPIController(IPosts postRepository)
        {
            _postRepository = postRepository;
        }

        // GET: api/PostAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDomainModel>>> GetPosts()
        {
            var posts = await _postRepository.Getpostlist();
            return Ok(posts);
        }

        // GET api/PostAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDomainModel>> GetPostById(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        // POST api/PostAPI
        [HttpPost]
        public async Task<ActionResult<int>> CreatePost([FromBody] PostDomainModel model)
        {
            if (model == null)
                return BadRequest("Invalid data.");

            int postId = await _postRepository.InsertPostAsync(model);
            return CreatedAtAction(nameof(GetPostById), new { id = postId }, postId);
        }

        // PUT api/PostAPI/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePost(int id, [FromBody] PostDomainModel model)
        {
            if (model == null || id != model.PostId)
                return BadRequest("Invalid data.");

            int result = await _postRepository.UpdatePostAsync(model);
            if (result == 0)
                return NotFound("Post not found.");

            return NoContent();
        }

        // DELETE api/PostAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            int result = await _postRepository.DeletePostAsync(id);
            if (result == 0)
                return NotFound("Post not found.");

            return NoContent();
        }
    }
}
