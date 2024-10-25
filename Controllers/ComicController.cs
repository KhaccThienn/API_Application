﻿
namespace API_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComicController : ControllerBase
    {
        private readonly DbComicAppContext    _context;
        private readonly ComicMemory          _inMem;
        private readonly IHttpContextAccessor _httpContext;

        public ComicController(DbComicAppContext context, IHttpContextAccessor httpContext, ComicMemory memory)
        {
            _context     = context;
            _httpContext = httpContext;
            _inMem       = memory;
        }

        // GET: api/Comic
        [HttpGet]
        public IEnumerable<Comic> GetComics()
        {
            return _context.Comics.ToList();
        }

        // GET: api/Actor
        [HttpGet("search/{name}")]
        public async Task<ActionResult<IEnumerable<Actor>>> GetDataByName(string name)
        {
            return Ok(await _context.Comics.Where(x => x.Title.ToLower().Contains(name.ToLower())).OrderByDescending(x => x.Id).ToListAsync());
        }

        // GET: api/Genre
        [HttpGet("by-paginate")]
        public async Task<IActionResult> GetComicsByPaginate(int page = 1, int pageSize = 1)
        {
            var total = _context.Comics.Count();
            var data = await _context.Comics
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

        [HttpGet("by-genre/{genreId}")]
        public async Task<ActionResult<IEnumerable<Comic>>> GetComicsByGenre(int genreId)
        {
            // Get the list of ComicGenres for the specified genreId
            var comicGenres = await _context.ComicGenres
                .Where(cg => cg.GenreId == genreId)
                .ToListAsync();

            // Get the comic IDs from the comicGenres
            var comicIds = comicGenres.Select(cg => cg.ComicId).Distinct().ToList();

            // Fetch the comics based on the retrieved comic IDs
            var comics = await _context.Comics
                .Where(c => comicIds.Contains(c.Id))
                .ToListAsync();

            if (comics == null || !comics.Any())
            {
                return NotFound(new { message = "No comics found for the specified genre." });
            }

            return Ok(comics);
        }

        [HttpGet("comment/{comicId}")]
        public async Task<ActionResult> GetCommentByComic(int comicId)
        {
            var comment = await _context.Comics.Include(x => x.Reviews).FirstOrDefaultAsync(x => x.Id == comicId);
                
            if (comment == null)
            {
                return NotFound(new { message = "No comment found for the specified comic." });
            }

            return Ok(comment);
        }

        [HttpGet("Directors/{id}")]
        public async Task<ActionResult<IEnumerable<ComicDirector>>> GetComicDirector(int id)
        {
            return await _context.ComicDirectors.Include(x => x.Director).Where(x => x.ComicId == id).ToListAsync();
        }

        [HttpGet("Actors/{id}")]
        public async Task<ActionResult<IEnumerable<ComicActor>>> GetComicActor(int id)
        {
            return await _context.ComicActors.Include(x => x.Actor).Where(x => x.ComicId == id).ToListAsync();
        }

        [HttpGet("Genres/{id}")]
        public async Task<ActionResult<IEnumerable<ComicGenre>>> GetComicGenres(int id)
        {
            return await _context.ComicGenres.Include(x => x.Genre).Where(x => x.ComicId == id).ToListAsync();
        }

        [HttpGet("details/{comicId}")]
        public async Task<ActionResult> GetComicDetails(int comicId)
        {
            var comic = await _context.Comics
                                     .Include(c => c.Episodes)
                                     .Include(c => c.Reviews)
                                     .FirstOrDefaultAsync(c => c.Id == comicId);

            var comicDirectors = await _context.ComicDirectors.Include(x => x.Director).Where(x => x.ComicId == comicId).ToListAsync();
            var comicActors = await _context.ComicActors.Include(x => x.Actor).Where(x => x.ComicId == comicId).ToListAsync();
            var comicGenres = await _context.ComicGenres.Include(x => x.Genre).Where(x => x.ComicId == comicId).ToListAsync();

            if (comic == null)
            {
                return NotFound(new { message = "Comic not found" });
            }

            var comicDetails = new ComicDetailsDto
            {
                Id          = comic.Id,
                Title       = comic.Title,
                Slug        = comic.Slug,
                Description = comic.Description,
                Poster      = comic.Poster,
                ReleaseYear = comic.ReleaseYear,
                View        = comic.View,
                Rating      = comic.Rating,
                Type        = comic.Type,
                Status      = comic.Status,
                CreatedAt   = comic.CreatedAt,
                UpdatedAt   = comic.UpdatedAt,
                PublishedAt = comic.PublishedAt,

                Episodes = comic.Episodes.Select(e => new EpisodeDto
                {
                    Id           = e.Id,
                    Title        = e.Title,
                    DisplayOrder = e.DisplayOrder,
                    Status       = e.Status,
                    CreatedAt    = e.CreatedAt,
                    UpdatedAt    = e.UpdatedAt,
                    PublishedAt  = e.PublishedAt
                }).ToList(),

                Reviews = comic.Reviews.Select(r => new ReviewDto
                {
                    Id = r.Id,
                    Comment = r.Comment,
                    Rating = r.Rating,
                    UserId = r.UserId,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt
                }).ToList(),

                Directors = comicDirectors.Select(cd => new ComicDirectorDto
                {
                    Id   = cd.Director.Id,  // Adjust to your Director properties
                    Name = cd.Director.Name // Adjust to your Director properties
                }).ToList(),

                Actors = comicActors.Select(ca => new ComicActorDto
                {
                    Id   = ca.Actor.Id,  // Adjust to your Actor properties
                    Name = ca.Actor.Name // Adjust to your Actor properties
                }).ToList(),

                Genres = comicGenres.Select(cg => new ComicGenreDto
                {
                    Id   = cg.Genre.Id,  // Adjust to your Genre properties
                    Name = cg.Genre.Name // Adjust to your Genre properties
                }).ToList()
            };

            return Ok(comicDetails);
        }



        // GET: api/Comic/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comic>> GetComic(int id)
        {
            var comic = await _context.Comics.FindAsync(id);

            if (comic == null)
            {
                return NotFound();
            }

            return comic;
        }

        // PUT: api/Comic/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComic(int id,[FromForm] UpdateComicDTO updateComicDTO)
        {
            if (id != updateComicDTO.Id)
            {
                return BadRequest();
            }

            // Find the comic in the database
            var comicFound = _context.Comics.FirstOrDefault(x => x.Id == id);
            if (comicFound == null)
            {
                return NotFound();
            }

            // Handle file upload (Poster update)
            if (updateComicDTO.ImageFile != null)
            {
                // Generate a unique filename for the uploaded file
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(updateComicDTO.ImageFile.FileName);
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                // Delete the old file if it exists
                var oldFileName = comicFound.Poster.Split($"uploads/")[1];
                var pathOldFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", oldFileName);
                if (System.IO.File.Exists(pathOldFile))
                {
                    System.IO.File.Delete(pathOldFile);
                }

                // Save the new file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    updateComicDTO.ImageFile.CopyTo(stream);
                }

                // Update the Poster field with the new file path
                comicFound.Poster = $"uploads/{fileName}";
            }

            // Update the comic    fields with the updated values from DTO
            comicFound.Title       = updateComicDTO.Title;
            comicFound.Slug        = updateComicDTO.Slug;
            comicFound.Description = updateComicDTO.Description;
            comicFound.ReleaseYear = updateComicDTO.ReleaseYear;
            comicFound.View        = updateComicDTO.View;
            comicFound.Rating      = updateComicDTO.Rating;
            comicFound.Type        = updateComicDTO.Type;
            comicFound.Status      = updateComicDTO.Status;
            comicFound.UpdatedAt   = DateOnly.FromDateTime(DateTime.Now);

            // Update Genres
            var existingGenres = _context.ComicGenres.Where(x => x.ComicId == id).ToList();
            _context.ComicGenres.RemoveRange(existingGenres); // Remove old genres
            foreach (var genreId in updateComicDTO.ListGenres)
            {
                _context.ComicGenres.Add(new ComicGenre
                {
                    ComicId = id,
                    GenreId = genreId
                });
            }

            // Update Actors
            var existingActors = _context.ComicActors.Where(x => x.ComicId == id).ToList();
            _context.ComicActors.RemoveRange(existingActors); // Remove old actors
            foreach (var actorId in updateComicDTO.ListActors)
            {
                _context.ComicActors.Add(new ComicActor
                {
                    ComicId = id,
                    ActorId = actorId
                });
            }

            // Update Directors
            var existingDirectors = _context.ComicDirectors.Where(x => x.ComicId == id).ToList();
            _context.ComicDirectors.RemoveRange(existingDirectors); // Remove old directors
            foreach (var directorId in updateComicDTO.ListDirector)
            {
                _context.ComicDirectors.Add(new ComicDirector
                {
                    ComicId    = id,
                    DirectorId = directorId
                });
            }

            // Mark the comic entity as modified
            _context.Entry(comicFound).State = EntityState.Modified;

            try
            {
                // Save all changes
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComicExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(comicFound);
        }


        // POST: api/Comic
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comic>> PostComic([FromForm] AddComicDTO addComicDTO)
        {
            if (addComicDTO.ImageFile != null)
            {
                // Generate a unique filename for the uploaded file
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(addComicDTO.ImageFile.FileName);
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    addComicDTO.ImageFile.CopyTo(stream);
                }

                // Update the poster field with the file path
                addComicDTO.Poster = $"uploads/{fileName}";
            }

            // Create a new Comic entity
            Comic comic = new Comic
            {
                Title       = addComicDTO.Title,
                Slug        = addComicDTO.Slug,
                Description = addComicDTO.Description,
                Poster      = addComicDTO.Poster,
                ReleaseYear = addComicDTO.ReleaseYear,
                View        = addComicDTO.View,
                Rating      = addComicDTO.Rating,
                Type        = addComicDTO.Type,
                Status      = addComicDTO.Status,
                CreatedAt   = DateOnly.FromDateTime(DateTime.Now),
                UpdatedAt   = DateOnly.FromDateTime(DateTime.Now)
            };

            // Add the comic to the context and save changes to get the ID
            _context.Comics.Add(comic);
            await _context.SaveChangesAsync();

            // Get the inserted ComicId
            var comicId = comic.Id;

            // Save genres to the ComicGenre table
            foreach (var genreId in addComicDTO.ListGenres)
            {
                ComicGenre comicGenre = new ComicGenre
                {
                    ComicId = comicId,
                    GenreId = genreId
                };
                _context.ComicGenres.Add(comicGenre);
            }

            // Save actors to the ComicActor table
            foreach (var actorId in addComicDTO.ListActors)
            {
                ComicActor comicActor = new ComicActor
                {
                    ComicId = comicId,
                    ActorId = actorId
                };
                _context.ComicActors.Add(comicActor);
            }

            // Save directors to the ComicDirector table
            foreach (var directorId in addComicDTO.ListDirector)
            {
                ComicDirector comicDirector = new ComicDirector
                {
                    ComicId    = comicId,
                    DirectorId = directorId
                };
                _context.ComicDirectors.Add(comicDirector);
            }

            // Save all changes
            await _context.SaveChangesAsync();

            // Return the newly created comic with its ID
            return CreatedAtAction("GetComic", new { id = comicId }, comic);
        }


        // DELETE: api/Comic/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComic(int id)
        {
            var comic = await _context.Comics.FindAsync(id);
            if (comic == null)
            {
                return NotFound();
            }

            _context.Comics.Remove(comic);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComicExists(int id)
        {
            return _context.Comics.Any(e => e.Id == id);
        }
    }
}
