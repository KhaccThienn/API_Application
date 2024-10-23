using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Application.Core.Database;
using API_Application.Core.Models;
using API_Application.Core.Database.InMemory;
using NuGet.Protocol.Core.Types;
using Humanizer.Localisation;

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
            return Ok(_inMem.DirectorMem.Values.OrderByDescending(x => x.Id).ToList());
        }

        // GET: api/Genre
        [HttpGet("by-paginate")]
        public ActionResult<IEnumerable<Genre>> GetDirectorsByPaginate(int page = 1, int pageSize = 1)
        {
            var total = _inMem.DirectorMem.Values.Count;
            var data = _inMem.DirectorMem.Values
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

        // GET: api/Director/5
        [HttpGet("{id}")]
        public ActionResult<Director> GetDirector(int id)
        {
            var director = _inMem.DirectorMem.Values.FirstOrDefault(x => x.Id == id);

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

            var actorFound = _inMem.DirectorMem.FirstOrDefault(x => x.Value.Id == id);

            if (drt.ImageFile != null)
            {
                try
                {
                    // Generate a unique filename for the uploaded file
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(drt.ImageFile.FileName);
                    var filePath = Path.Combine("wwwroot/uploads", fileName);

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", drt.ImageFile.FileName);

                    var oldFileName = actorFound.Value.Avatar.Split($"{_httpContext.HttpContext.Request.Host.Value}/uploads/");
                    Console.WriteLine($"Old File: {oldFileName}");

                    var pathOldFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", oldFileName[1]);
                    if (System.IO.File.Exists(pathOldFile))
                    {
                        System.IO.File.Delete(pathOldFile);
                    }

                    // Save the file to the server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        drt.ImageFile.CopyTo(stream);
                    }

                    // Update the avatar field with the file path
                    drt.Avatar = $"https://{_httpContext.HttpContext.Request.Host.Value}/uploads/{fileName}";
                }
                catch
                {
                    throw;
                }

            }
            else
            {
                drt.Avatar = actorFound.Value.Avatar;
            }

            var director = new Director
            {
                Id = drt.Id,
                Name = drt.Name,
                Birthday = drt.Birthday,
                Description = drt.Description,
                Avatar = drt.Avatar,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                UpdatedAt = DateOnly.FromDateTime(DateTime.Now)
            };

            _context.Entry(director).State = EntityState.Modified;

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

            return NoContent();
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
                act.Avatar = $"https://{_httpContext.HttpContext.Request.Host.Value}/uploads/{fileName}";
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
            // insert into memory
            _inMem.DirectorMem.Add(ac.Entity.Id.ToString(), ac.Entity);

            return CreatedAtAction("GetDirector", new { id = director.Id }, director);
        }

        // DELETE: api/Director/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDirector(int id)
        {
            var director = _inMem.DirectorMem.FirstOrDefault(x => x.Value.Id == id);
            if (director.Value == null)
            {
                return NotFound();
            }
            try
            {
                var oldFileName = director.Value.Avatar.Split($"{_httpContext.HttpContext.Request.Host.Value}/uploads/");
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

            _context.Directors.Remove(director.Value);
            await _context.SaveChangesAsync();

            _inMem.DirectorMem.Remove(director.Value.Id.ToString());

            return Ok(director.Value);
        }

        private bool DirectorExists(int id)
        {
            return _inMem.DirectorMem.Values.Any(e => e.Id == id);
        }
    }
}
