namespace API_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodeController : ControllerBase
    {
        private readonly DbComicAppContext _context;
        private readonly EpisodeMemory _inMem;
        private readonly IHttpContextAccessor _httpContext;

        public EpisodeController(DbComicAppContext context, IHttpContextAccessor httpContext, EpisodeMemory memory)
        {
            _context = context;
            _httpContext = httpContext;
            _inMem = memory;
        }

        [HttpGet("get-all")]
        public async Task<IEnumerable<Episode>> GetAll()
        {
            return await _context.Episodes.Include(x => x.Comic).ToListAsync();
        }

        [HttpGet("get-all-comic")]
        public async Task<IActionResult> GetAllComic()
        {
            var data = await _context.Comics
                .OrderByDescending(x => x.Id)
                .Select(comic => new
                {
                    Comic = new Comic
                    {
                        Id = comic.Id,
                        Title = comic.Title,
                        Poster = comic.Poster
                    },

                    LastEpisode = comic.Episodes
                        .OrderByDescending(e => e.DisplayOrder)
                        .Select(e => new Episode
                        {
                            Id = e.Id,
                            Title = e.Title,
                        }).FirstOrDefault()
                }).ToListAsync();
            return Ok(data);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Episode>> GetEpisode(int id)
        {
            var episode = await _context.Episodes.Include(x => x.Comic).Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == id);

            if (episode == null)
            {
                return NotFound();
            }

            return episode;
        }

        [HttpGet("ByComic/{comic_id}")]
        public async Task<IEnumerable<Episode>> GetEpisodeByComicId(int comic_id)
        {
            return await _context.Episodes.Where(x => x.ComicId == comic_id)
                                          .OrderByDescending(x => x.Id)
                                          .ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> PostData([FromBody] AddEpisodeDTO model)
        {
            if (model.Title == null || model.DisplayOrder == null || model.Status == null || model.ComicId == null)
            {
                return BadRequest("The field cannot be null");
            }

            // Check if another episode with the same DisplayOrder exists for this ComicId
            var existingEpisode = _inMem.EpisodeMem.Values
                .FirstOrDefault(e => e.ComicId == model.ComicId && e.DisplayOrder == model.DisplayOrder);

            if (existingEpisode != null)
            {
                return BadRequest("An episode with the same display order already exists for this comic.");
            }

            var episode = new Episode()
            {
                Title = model.Title,
                DisplayOrder = model.DisplayOrder,
                Status = model.Status,
                ComicId = model.ComicId,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now)
            };

            _context.Episodes.Add(episode);

            await _context.SaveChangesAsync();

            _inMem.EpisodeMem.Add(episode.Id.ToString(), episode);

            // Return the newly created comic with its ID
            return CreatedAtAction("GetEpisode", new { id = episode.Id }, episode);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEpisode(int id, [FromBody] UpdateEpisodeDTO model)
        {
            if (id != model.Id)
            {
                return BadRequest("Id does not match");
            }

            var episode = _inMem.EpisodeMem.Values.FirstOrDefault(x => x.Id == id);

            if (episode == null)
            {
                return NotFound("Episode not found");
            }

            if (model.Title == null || model.DisplayOrder == null || model.Status == null || model.ComicId == null)
            {
                return BadRequest("The field cannot be null");
            }

            // Check if another episode with the same DisplayOrder exists for this ComicId
            var existingEpisode = _inMem.EpisodeMem.Values
                .FirstOrDefault(e => e.ComicId == episode.ComicId && e.DisplayOrder == model.DisplayOrder && e.Id != id);

            if (existingEpisode != null)
            {
                return BadRequest("Another episode with the same display order already exists for this comic.");
            }

            // Update the        episode details
            episode.Title = model.Title;
            episode.DisplayOrder = model.DisplayOrder;
            episode.Status = model.Status;
            episode.ComicId = model.ComicId;
            episode.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);

            // Mark the entity as modified
            _context.Entry(episode).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            // Update in-memory data if necessary
            _inMem.EpisodeMem.Remove(episode.Id.ToString());

            _inMem.EpisodeMem.Add(episode.Id.ToString(), episode);

            return Ok(episode);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEpisode(int id)
        {
            var episode = await _context.Episodes.Include(e => e.Images).FirstOrDefaultAsync(e => e.Id == id);

            if (episode == null)
            {
                return NotFound("Episode not found");
            }

            // Remove related images
            if (episode.Images != null && episode.Images.Count != 0)
            {
                foreach (var image in episode.Images)
                {
                    try
                    {
                        // Delete the old file if it exists
                        var oldFileName = image.Url.Split($"{_httpContext.HttpContext.Request.Host.Value}/uploads/")[1];
                        var pathOldFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", oldFileName);
                        if (System.IO.File.Exists(pathOldFile))
                        {
                            System.IO.File.Delete(pathOldFile);
                        }
                    }
                    catch (Exception ex)
                    {
                        return BadRequest("Having an error when deleting file");
                    }
                }
                _context.Images.RemoveRange(episode.Images);
            }

            // Remove the episode
            _context.Episodes.Remove(episode);

            await _context.SaveChangesAsync();

            // Remove from in-memory data if necessary
            _inMem.EpisodeMem.Remove(id.ToString());

            return Ok(episode);
        }

    }
}
