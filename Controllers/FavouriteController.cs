namespace API_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {
        private readonly DbComicAppContext            _context;
        private readonly ILogger<FavouriteController> _logger;

        public FavouriteController(DbComicAppContext context, ILogger<FavouriteController> logger)
        {
            _context  = context;
            _logger   = logger;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllFavouriteComicByUser(int userId)
        {
            var comics = await _context.Favourites
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Created_At)
                .Select(x => x.Comic)  // Select only the Comic from each Favourite
                .ToListAsync();
            if (comics == null)
            {
                return NoContent();
            }
            return Ok(comics);
        }

        [HttpGet("check/{userId}/{comicId}")]
        public async Task<IActionResult> GetAllFavouriteComicByUser(int userId, int comicId)
        {
            var comics = await _context.Favourites
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ComicId == comicId);
            if (comics == null)
            {
                return BadRequest();
            }
            return Ok(comics);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddFavourite([FromBody] CreateFavouriteDTO model)
        {
            // Check if the favorite already exists
            var existingFavourite = await _context.Favourites
                .FirstOrDefaultAsync(f => f.ComicId == model.ComicId && f.UserId == model.UserId);

            if (existingFavourite != null)
            {
                return BadRequest(new { message = "This comic is already in your favorites." });
            }

            // Add new favorite
            var newFavourite = new Favourite
            {
                ComicId = model.ComicId,
                UserId = model.UserId,
                Created_At = DateTime.UtcNow
            };

            _context.Favourites.Add(newFavourite);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Comic added to your favorites successfully." });
        }

        [HttpDelete("remove/{userId}/{comicId}")]
        public async Task<IActionResult> RemoveFavourite(int userId, int comicId)
        {
            // Check if the favorite exists
            var existingFavourite = await _context.Favourites
                .FirstOrDefaultAsync(f => f.ComicId == comicId && f.UserId == userId);

            if (existingFavourite == null)
            {
                return NotFound(new { message = "This comic is not in your favorites." });
            }

            // Remove the favorite
            _context.Favourites.Remove(existingFavourite);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Comic removed from your favorites successfully." });
        }

    }
}
