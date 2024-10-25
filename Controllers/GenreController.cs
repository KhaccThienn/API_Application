namespace API_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly DbComicAppContext _context;
        private readonly GenreMemory _inMem;
        private readonly ILogger<GenreController> _logger;

        public GenreController(DbComicAppContext context, GenreMemory mem, ILogger<GenreController> logger)
        {
            _inMem = mem;
            _context = context;
            _logger = logger;
        }

        // GET: api/Genre
        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var data     = await _context.Genres.ToListAsync();
            return Ok(data);
        }

        // GET: api/Genre
        [HttpGet("by-paginate")]
        public async Task<IActionResult> GetGenresByPaginate(int page = 1, int pageSize = 1)
        {
            var total = _context.Genres.Count();
            var data = await _context.Genres
                         .OrderByDescending(x => x.Id)
                         .Skip((page - 1) * pageSize)
                         .Take(pageSize)
                         .ToListAsync();
            var response = new
            {
                Data = data,
                TotalItem = total,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)total / pageSize)
            };
            return Ok(response);
        }

        // GET: api/Actor
        [HttpGet("search/{name}")]
        public async Task<ActionResult<IEnumerable<Actor>>> GetDataByName(string name)
        {
            return Ok(await _context.Genres.Where(x => x.Name.ToLower().Contains(name.ToLower())).OrderByDescending(x => x.Id).ToListAsync());
        }

        [HttpGet("Search/{query}")]
        public async Task<IActionResult> GetGenresByQuery(string? query)
        {
            _logger.LogInformation(query.Length.ToString());

            if (query.Length != 0)
            {
                return Ok(await _context.Genres.Where(x => x.Name.ToLower().Contains(query.ToLower())).ToListAsync());

            }
            return Ok(await _context.Genres.ToListAsync());

        }


        // GET: api/Genre/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenre(int? id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);

            if (genre == null)
            {
                return NotFound();
            }

            return Ok(genre);
        }

        // PUT: api/Genre/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenre(int? id, Genre genre)
        {
            if (id != genre.Id)

            {
                return BadRequest();
            }

            try
            {
                var g = await _context.Genres.FirstOrDefaultAsync(u => u.Id == id);

                g.Name = genre.Name;
                g.Slug = genre.Slug;
                g.CreatedAt = genre.CreatedAt;
                g.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);

                // Update in database
                _context.Entry(g).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(genre);
        }

        // POST: api/Genre
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Genre>> PostGenre(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetGenre", new { id = genre.Id }, genre);
        }

        // DELETE: api/Genre/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int? id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            _inMem.GenreMem.Remove(genre.Id.ToString());

            return Ok(genre);
        }

        private bool GenreExists(int? id)
        {
            return _context.Genres.Any(e => e.Id == id);
        }
    }
}
