namespace API_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly DbComicAppContext          _context;
        private readonly ILogger<HistoryController> _logger;

        public HistoryController(DbComicAppContext context, ILogger<HistoryController> logger)
        {
            _context = context;
            _logger  = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateHistory([FromBody] History history)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var hasRead = await _context.Histories.FirstOrDefaultAsync(h => h.UserId == history.UserId && h.ComicId == history.ComicId);

            if (hasRead == null)
            {
                history.Created_At = history.Updated_At = DateTime.UtcNow;
                _context.Histories.Add(history);
            } else
            {
                hasRead.ComicId               = history.ComicId;
                hasRead.EpisodeId             = history.EpisodeId;
                hasRead.Updated_At            = DateTime.UtcNow;

                _context.Entry(hasRead).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
            return Ok(history);
        }

        [HttpGet("user/{userId}/")]
        public async Task<IActionResult> GetLatestUserHistory(int userId)
        {
            var latestHistory = await _context.Histories
                                              .Where(x => x.UserId == userId)
                                              .Include(h => h.Comic)
                                              .Include(h => h.Episode)
                                              .OrderByDescending(h => h.Updated_At)
                                              .Select(h => new ComicHistoryDTO
                                              {
                                                  ComicId      = h.Comic.Id,
                                                  ComicName    = h.Comic.Title,
                                                  EpisodeId    = h.Episode.Id,
                                                  EpisodeName  = h.Episode.Title,
                                                  ImageUrl     = h.Comic.Poster,
                                                  DisplayOrder = h.Episode.DisplayOrder,
                                                  UpdatedAt    = h.Updated_At ?? DateTime.UtcNow
                                              })
                                              .ToListAsync();

            if (latestHistory == null)
                return NotFound();

            return Ok(latestHistory);
        }


        [HttpDelete("user/{userId}/comic/{comicId}")]
        public async Task<IActionResult> DeleteUserComicHistory(int userId, int comicId)
        {
            var history = await _context.Histories
                .FirstOrDefaultAsync(h => h.UserId == userId && h.ComicId == comicId);

            if (history == null)
            {
                return NotFound(new { message = "History not found for the specified comic and user." });
            }

            _context.Histories.Remove(history);

            await _context.SaveChangesAsync();

            return Ok(new { message = "History entry successfully deleted." });
        }
    }
}
