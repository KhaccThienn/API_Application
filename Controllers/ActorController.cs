using Microsoft.AspNetCore.Cors;
using System.Drawing.Printing;

namespace API_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class ActorController : ControllerBase
    {
        private readonly DbComicAppContext _context;
        private readonly ActorMemory _inMem;
        private readonly IHttpContextAccessor _httpContext;
        public ActorController(DbComicAppContext context, ActorMemory mem, IHttpContextAccessor httpContext)
        {
            _context = context;
            _inMem = mem;
            _httpContext = httpContext;
        }

        // GET: api/Actor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> GetActors()
        {
            return Ok(await _context.Actors.OrderByDescending(x => x.Id).ToListAsync());
        }

        // GET: api/Actor
        [HttpGet("search/{name}")]
        public async Task<ActionResult<IEnumerable<Actor>>> GetDataByName(string name)
        {
            return Ok(await _context.Actors.Where(x => x.Name.Contains(name)).OrderByDescending(x => x.Id).ToListAsync());
        }

        // GET: api/Actor
        [HttpGet("by-paginate")]
        public async Task<ActionResult<IEnumerable<Actor>>> GetActorsByPaginate(int page = 1, int pageSize = 1)
        {
            var total = _context.Actors.Count();
            var data = await _context.Actors
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

        // GET: api/Actor/5
        [HttpGet("{id}")]
        public ActionResult<Actor> GetActor(int id)
        {
            var actor = _context.Actors.FirstOrDefault(x => x.Id == id);

            if (actor == null)
            {
                return NotFound();
            }

            return actor;
        }

        // PUT: api/Actor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor(int id, UpdateActorDTO act)
        {
            if (id != act.Id)
            {
                return BadRequest();
            }

            var actorFound = _context.Actors.FirstOrDefault(x => x.Id == id);

            if (act.ImageFile != null)
            {
                try
                {
                    // Generate a unique filename for the uploaded file
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(act.ImageFile.FileName);
                    var filePath = Path.Combine("wwwroot/uploads", fileName);

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", act.ImageFile.FileName);
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
                        act.ImageFile.CopyTo(stream);
                    }

                    // Update the avatar field with the file path
                    act.Avatar = $"uploads/{fileName}";
                }
                catch
                {
                    throw;
                }

            }
            else
            {
                act.Avatar = actorFound.Avatar;
            }

            var actor = new Actor
            {
                Id = act.Id,
                Name = act.Name,
                Birthday = act.Birthday,
                Description = act.Description,
                Avatar = act.Avatar,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                UpdatedAt = DateOnly.FromDateTime(DateTime.Now)
            };

            _context.Entry(actor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(actor);
        }

        // POST: api/Actor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Actor>> PostActor(CreateActorDTO act)
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

            Actor actor = new Actor
            {
                Name = act.Name,
                Avatar = act.Avatar,
                Description = act.Description,
                Birthday = act.Birthday,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                UpdatedAt = DateOnly.FromDateTime(DateTime.Now)
            };

            var ac = _context.Actors.Add(actor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActor", new { id = actor.Id }, actor);
        }

        // DELETE: api/Actor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == id);
            if (actor == null)
            {
                return NotFound();
            }
            try
            {   
                var oldFileName = actor.Avatar.Split($"uploads/");
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

            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();

            return Ok(actor);
        }

        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.Id == id);
        }
    }
}
