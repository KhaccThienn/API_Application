using System;
namespace API_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly DbComicAppContext _context;
        private readonly DirectorMemory _inMem;
        private readonly IHttpContextAccessor _httpContext;
        public DirectorController(DbComicAppContext context, DirectorMemory mem, IHttpContextAccessor httpContext)
        {
            _context = context;
            _inMem = mem;
            _httpContext = httpContext;
        }

        // GET: api/Director
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Director>>> GetDirectors()
        {
            return Ok(await _context.Directors.OrderByDescending(x => x.Id).ToListAsync());
        }

        // GET: api/Actor
        [HttpGet("search/{name}")]
        public async Task<ActionResult<IEnumerable<Actor>>> GetDataByName(string name)
        {
            return Ok(await _context.Directors.Where(x => x.Name.ToLower().Contains(name.ToLower())).OrderByDescending(x => x.Id).ToListAsync());
        }

        // GET: api/Genre
        [HttpGet("by-paginate")]
        public async Task<IActionResult> GetDirectorsByPaginate(int page = 1, int pageSize = 1)
        {
            var total = _context.Directors.Count();
            var data = await _context.Directors
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

        // GET: api/Director/5
        [HttpGet("{id}")]
        public ActionResult<Director> GetDirector(int id)
        {
            var director = _context.Directors.FirstOrDefault(x => x.Id == id);

            if (director == null)
            {
                return NotFound();
            }

            return director;
        }

        // PUT: api/Director/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDirector(int id, UpdateDirectorDTO drt)
        {
            if (id != drt.Id)
            {
                return BadRequest();
            }

            var actorFound = await _context.Directors.FirstOrDefaultAsync(x => x.Id == id);
            if (actorFound == null)
            {
                return NoContent();
            }
            if (drt.ImageFile != null)
            {
                try
                {
                    // Generate a unique filename for the uploaded file
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(drt.ImageFile.FileName);
                    var filePath = Path.Combine("wwwroot/uploads", fileName);

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", drt.ImageFile.FileName);
                    if (actorFound.Avatar != null)
                    {
                        var oldFileName = actorFound.Avatar.Split($"uploads/");
                        Console.WriteLine($"Old File: {oldFileName}");

                        var pathOldFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", oldFileName[1]);
                        if (System.IO.File.Exists(pathOldFile))
                        {
                            System.IO.File.Delete(pathOldFile);
                        }
                    }
                  
                    // Save the file to the server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        drt.ImageFile.CopyTo(stream);
                    }

                    // Update the avatar field with the file path
                    actorFound.Avatar = $"uploads/{fileName}";
                }
                catch
                {
                    throw;
                }

            }

            actorFound.CreatedAt = DateOnly.FromDateTime(DateTime.Now);
            actorFound.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);

            _context.Entry(actorFound).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(drt);
        }

        // POST: api/Director
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Director>> PostDirector(CreateActorDTO act)
        {
            if (act.ImageFile != null)
            {
                // Generate a unique filename for the uploaded file
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(act.ImageFile.FileName);
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    act.ImageFile.CopyTo(stream);
                }

                // Update the avatar field with the file path
                act.Avatar = $"uploads/{fileName}";
            }

            Director director = new Director
            {
                Name = act.Name,
                Avatar = act.Avatar,
                Description = act.Description,
                Birthday = act.Birthday,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                UpdatedAt = DateOnly.FromDateTime(DateTime.Now)
            };

            var ac = _context.Directors.Add(director);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDirector", new { id = director.Id }, director);
        }

        // DELETE: api/Director/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDirector(int id)
        {
            var director = await _context.Directors.FirstOrDefaultAsync(x => x.Id == id);
            if (director == null)
            {
                return NotFound();
            }
            try
            {
                var oldFileName = director.Avatar.Split($"uploads/");
                Console.WriteLine($"Old File: {oldFileName}");
                var pathOldFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", oldFileName[1]);
                if (System.IO.File.Exists(pathOldFile))
                {
                    System.IO.File.Delete(pathOldFile);
                }
            }
            catch
            {
                throw;
            }

            _context.Directors.Remove(director);
            await _context.SaveChangesAsync();


            return Ok(director);
        }

        private bool DirectorExists(int id)
        {
            return _inMem.DirectorMem.Values.Any(e => e.Id == id);
        }
    }
}
