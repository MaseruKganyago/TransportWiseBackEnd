using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstProject.Data;
using FirstProject.Domain;
using AutoMapper;
using FirstProject.Models.DTOs;

namespace FirstProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileStoragesController : ControllerBase
    {
        private readonly MyDBContext _context;
		private readonly IMapper _mapper;

        public FileStoragesController(MyDBContext context, IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
        }

        // GET: api/FileStorages
        [HttpGet]
        public IEnumerable<FileStorageDTO> GetFileStorage()
        {
            var files =  _context.FileStorage.ToList();
			var filesDTO = _mapper.Map<List<FileStorageDTO>>(files);

			return filesDTO;
        }

        // GET: api/FileStorages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FileStorageDTO>> GetFileStorage([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var file = await _context.FileStorage.FindAsync(id);
			var fileDTO = _mapper.Map<FileStorageDTO>(file);

            if (file == null)
            {
                return NotFound();
            }

            return Ok(file);
        }

        // PUT: api/FileStorages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFileStorage([FromRoute] Guid id, [FromBody] FileStorage fileStorage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fileStorage.FileStorageId)
            {
                return BadRequest();
            }

            _context.Entry(fileStorage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FileStorageExists(id))
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

        // POST: api/FileStorages
        [HttpPost]
        public async Task<IActionResult> PostFileStorage([FromBody] FileStorage fileStorage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.FileStorage.Add(fileStorage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFileStorage", new { id = fileStorage.FileStorageId }, fileStorage);
        }

        // DELETE: api/FileStorages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFileStorage([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fileStorage = await _context.FileStorage.FindAsync(id);
            if (fileStorage == null)
            {
                return NotFound();
            }

            _context.FileStorage.Remove(fileStorage);
            await _context.SaveChangesAsync();

            return Ok(fileStorage);
        }

        private bool FileStorageExists(Guid id)
        {
            return _context.FileStorage.Any(e => e.FileStorageId == id);
        }
    }
}