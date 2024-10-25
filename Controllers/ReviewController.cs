﻿using API_Application.Core.Models;

namespace API_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly DbComicAppContext         _context;
        private readonly ReviewMemory              _inMem;
        private readonly ILogger<ReviewController> _logger;

        public ReviewController(DbComicAppContext context, ReviewMemory inMem, ILogger<ReviewController> logger)
        {
            _inMem   = inMem;
            _context = context;
            _logger  = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            return Ok(await _context.Reviews.Include(x => x.Comic).ToListAsync());
        }

        [HttpGet("get-by-comic/{comicId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetByComicId(int comicId)
        {
            var data = await _context.Reviews
                                    .Include(x => x.Comic)  // Include the Comic entity
                                    .Include(x => x.User)   // Include the User entity (assuming it's a navigation property)
                                    .Where(x => x.ComicId == comicId)
                                    .ToListAsync();

            if (data == null)
            {
                return NoContent();
            }

            return Ok(data);
        }

        [HttpGet("getByComic/{comicId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAllReviewByComicId(int comicId)
        {
            var data = await _context.Reviews
                                    .Include(x => x.Comic)  // Include the Comic entity if needed
                                    .Include(x => x.User)   // Include the User entity (assuming it's a navigation property)
                                    .Where(x => x.ComicId == comicId)
                                    .Select(x => new ReturnReviewDto
                                    {
                                        ReviewId   = x.Id,
                                        UserName   = x.User.Name,          // Assuming User has a Name property
                                        UserAvatar = x.User.Avatar,   // Assuming User has an AvatarUrl property
                                        CreatedAt  = x.CreatedAt,
                                        Comment    = x.Comment,
                                        Rating     = x.Rating
                                    })
                                    .OrderByDescending(x => x.ReviewId)
                                    .ToListAsync();

            if (data == null || !data.Any())
            {
                return Ok();
            }

            return Ok(data);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _context.Reviews.Include(x => x.Comic).FirstOrDefaultAsync(x => x.Id == id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        [HttpGet("search/{name}")]
        public async Task<ActionResult> SearchComics(string name)
        {
            var data = await _context.Reviews
                                     .Include(x => x.Comic)
                                     .Where(x => x.Comic.Title.Contains(name))
                                     .ToListAsync();

            if (data == null || !data.Any())
            {
                return NotFound();
            }

            return Ok(data);
        }


        [HttpPost]
        public async Task<IActionResult> PostReview([FromBody] Review model)
        {
            if (string.IsNullOrEmpty(model.Comment) || model.ComicId == null || model.UserId == null)
            {
                return BadRequest("All fields are required");
            }

            try
            {
                model.Rating    = 5;
                model.CreatedAt = DateOnly.FromDateTime(DateTime.Now);
                _context.Reviews.Add(model);
                await _context.SaveChangesAsync();

                _inMem.ReviewMem.Add(model.Id.ToString(), model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, [FromBody] Review model)
        {
            if (id != model.Id)
            {
                return BadRequest("Id Does not match");
            }
            if (model.Comment == null || model.ComicId == null || model.UserId == null)
            {
                return BadRequest("All fields are required");
            }
            var review = _context.Reviews.FirstOrDefault(x => x.Id == id);

            if (review == null)
            {
                return NotFound($"Not found review with id {id}");
            }

            try
            {
                review.Comment   = model.Comment;
                review.Rating    = 5;
                review.ComicId   = model.ComicId;
                review.UserId    = model.UserId;
                review.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);

                // Update in database
                _context.Entry(review).State = EntityState.Modified;

                _context.Reviews.Update(model);
                await _context.SaveChangesAsync();

                _inMem.ReviewMem.Remove(id.ToString());
                _inMem.ReviewMem.Add(review.Id.ToString(), review);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int? id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            _inMem.ReviewMem.Remove(review.Id.ToString());

            return Ok(review);
        }
    }
}
