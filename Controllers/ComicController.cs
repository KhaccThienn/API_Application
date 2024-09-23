using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Application.Core.Database;
using API_Application.Core.Models;

namespace API_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComicController : ControllerBase
    {
        private readonly DbComicAppContext _context;

        public ComicController(DbComicAppContext context)
        {
            _context = context;
        }

        // GET: api/Comic
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comic>>> GetComics()
        {
            return await _context.Comics.ToListAsync();
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
        public async Task<IActionResult> PutComic(int id, Comic comic)
        {
            if (id != comic.Id)
            {
                return BadRequest();
            }

            _context.Entry(comic).State = EntityState.Modified;

            try
            {
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

            return NoContent();
        }

        // POST: api/Comic
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comic>> PostComic(Comic comic)
        {
            _context.Comics.Add(comic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComic", new { id = comic.Id }, comic);
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
