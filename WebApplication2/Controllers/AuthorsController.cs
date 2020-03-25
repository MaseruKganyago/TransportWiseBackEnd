using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using FirstProject.Domain;
using FirstProject.Data;
using FirstProject.Models.DTOs;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace FirstProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[System.Web.Http.HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
	public class AuthorsController : ControllerBase
    {
        private readonly MyDBContext _context;
		private readonly IMapper _mapper;

        public AuthorsController(MyDBContext context, IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
        }

        // GET: api/Authors
  	[HttpGet]
    [System.Web.Http.HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        public IEnumerable<AuthorDTO> GetAuthor()
        {
            var authors = _context.Author.ToList();
			var authorsDTO = _mapper.Map<List<AuthorDTO>>(authors);
			return authorsDTO;
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
		[System.Web.Http.HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
		public async Task<ActionResult<AuthorDTO>> GetAuthor([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = await _context.Author.FindAsync(id);
			var authorDTO = _mapper.Map<AuthorDTO>(author);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(authorDTO);
        }

        // PUT: api/Authors/5
        [HttpPut("{id}")]
		[System.Web.Http.HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
		public async Task<IActionResult> PutAuthor([FromRoute] Guid id, [FromBody] AuthorDTO author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != author.Id)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // POST: api/Authors
        [HttpPost]
		[System.Web.Http.HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
		public async Task<IActionResult> PostAuthor([FromBody] Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
		[System.Web.Http.HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
		public async Task<IActionResult> DeleteAuthor([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = await _context.Author.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Author.Remove(author);
            await _context.SaveChangesAsync();

            return Ok(author);
        }

        private bool AuthorExists(Guid id)
        {
            return _context.Author.Any(e => e.Id == id);
        }
    }
}
