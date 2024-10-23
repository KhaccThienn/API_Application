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
        public ActionResult<IEnumerable<Genre>> GetGenres()
        {
            var data     = _inMem.GenreMem.Values.ToList();
            return Ok(data);
        }

        // GET: api/Genre
        [HttpGet("by-paginate")]
        public ActionResult<IEnumerable<Genre>> GetGenresByPaginate(int page = 1, int pageSize = 1)
        {
            var total = _inMem.GenreMem.Values.Count;
            var data = _inMem.GenreMem.Values
                         .OrderByDescending(x => x.Id)
                         .Skip((page - 1) * pageSize)
                         .Take(pageSize)
                         .ToList();
            var response = new
            {
                Data = data,
                TotalItem = total,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)total / pageSize)
            };
            return Ok(response);
        }

        [HttpGet("Search/{query}")]
        public ActionResult<IEnumerable<Genre>> GetGenresByQuery(string? query)
        {
            _logger.LogInformation(query.Length.ToString());

            if (query.Length != 0)
            {
                return Ok(_inMem.GenreMem.Values.Where(x => x.Name.ToLower().Contains(query.ToLower())).ToList());

            }
            return Ok(_inMem.GenreMem.Values.ToList());

        }


        // GET: api/Genre/5
        [HttpGet("{id}")]
        public ActionResult<Genre> GetGenre(int? id)
        {
            var genre = _inMem.GenreMem.Values.FirstOrDefault(x => x.Id == id);

            if (genre == null)
            {
                return NotFound();
            }

            return genre;
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
                var g = _inMem.GenreMem.FirstOrDefault(u => u.Value.Id == id);

                g.Value.Name = genre.Name;
                g.Value.Slug = genre.Slug;
                g.Value.CreatedAt = genre.CreatedAt;
                g.Value.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);

                // Update in database
                _context.Entry(genre).State = EntityState.Modified;

                _inMem.GenreMem.Remove(id.ToString());
                _inMem.GenreMem.Add(g.Value.Id.ToString(), genre);

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
            // Save into memory
            _inMem.GenreMem.Add(genre.Id.ToString(), genre);
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
