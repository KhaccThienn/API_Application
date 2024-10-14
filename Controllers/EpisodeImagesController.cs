using API_Application.Core.Models;
using API_Application.Core.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodeImagesController : ControllerBase
    {
        private readonly DbComicAppContext _context;
        private readonly ImagesMemory _inMem;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<EpisodeImagesController> _logger;

        public EpisodeImagesController(DbComicAppContext context, IHttpContextAccessor httpContext, ImagesMemory memory, ILogger<EpisodeImagesController> logger)
        {
            _context = context;
            _httpContext = httpContext;
            _inMem = memory;
            _logger = logger;
        }

        [HttpGet("get-all")]
        public ActionResult<IEnumerable<Image>> GetAll()
        {
            var data = _inMem.ImagesInMem.Values.ToList();
            return Ok(data);
        }

        [HttpGet("get-by-episode/{episodeId}")]
        public async Task< ActionResult<IEnumerable<Image>>> GetByEpisodeId(int episodeId)
        {
            var data = await _context.Images.Include(x => x.Episode).Where(x => x.EpisodeId == episodeId).ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Image>> GetImage(int id)
        {
            var image = await _context.Images.Include(x => x.Episode).FirstOrDefaultAsync(x => x.Id == id);

            if (image == null)
            {
                return NotFound();
            }

            return image;
        }

        [HttpPost]
        public async Task<IActionResult> PostImage([FromForm] AddImageDTO model)
        {
            if (model.Name == null || model.DisplayOrder == null || model.EpisodeId == null)
            {
                return BadRequest("The fields are required");
            }

            if (model.ImageFile == null)
            {
                return BadRequest("ImageFile Cannot be null");
            }

            try
            {
                // Generate a unique filename for the uploaded file
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(stream);
                }

                // Update the field with the file path
                model.Url = $"https://{_httpContext.HttpContext.Request.Host.Value}/uploads/{fileName}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Having an error when uploading file");
            }

            var image = new Image
            {
                Name = model.Name,
                Url = model.Url,
                DisplayOrder = model.DisplayOrder,
                EpisodeId = model.EpisodeId,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now)
            };

            _context.Images.Add(image);

            await _context.SaveChangesAsync();

            _inMem.ImagesInMem.Add(image.Id.ToString(), image);

            // Return the newly created comic with its ID
            return CreatedAtAction("GetImage", new { id = image.Id }, image);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateImage(int id, [FromForm] UpdateImageDTO model)
        {
            if (id != model.Id)
            {
                return BadRequest("Id Does not match");
            }
            if (model.Name == null || model.DisplayOrder == null || model.EpisodeId == null)
            {
                return BadRequest("The fields are required");
            }
            var image = await _context.Images.FirstOrDefaultAsync(x => x.Id == id);
            if (image == null)
            {
                return BadRequest($"Not found image with id {id}");
            }

            try
            {
                if (model.ImageFile != null)
                {
                    // Generate a unique filename for the uploaded file
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                    var filePath = Path.Combine("wwwroot/uploads", fileName);

                    // Delete the old file if it exists
                    var oldFileName = image.Url.Split($"{_httpContext.HttpContext.Request.Host.Value}/uploads/")[1];
                    var pathOldFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", oldFileName);
                    if (System.IO.File.Exists(pathOldFile))
                    {
                        System.IO.File.Delete(pathOldFile);
                    }

                    // Save the file to the server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.ImageFile.CopyTo(stream);
                    }

                    // Update the field with the file path
                    model.Url = $"https://{_httpContext.HttpContext.Request.Host.Value}/uploads/{fileName}";
                }
                else
                {
                    model.Url = image.Url;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Having an error when uploading file");
            }


            image.Name = model.Name;
            image.Url = model.Url;
            image.DisplayOrder = model.DisplayOrder;
            image.EpisodeId = model.EpisodeId;
            image.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);


            // Mark the entity as modified
            _context.Entry(image).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            // Update in-memory data if necessary
            _inMem.ImagesInMem.Remove(image.Id.ToString());

            _inMem.ImagesInMem.Add(image.Id.ToString(), image);

            return Ok(image);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _context.Images.FirstOrDefaultAsync(x => x.Id == id);
            if (image == null)
            {
                return BadRequest($"Not found image with id {id}");
            }
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
                _logger.LogError(ex.Message);
                return BadRequest("Having an error when deleting file");
            }

            // Remove the episode
            _context.Images.Remove(image);

            await _context.SaveChangesAsync();

            // Remove from in-memory data if necessary
            _inMem.ImagesInMem.Remove(id.ToString());

            return Ok(image);
        }
    }
}
