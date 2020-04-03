using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstProject.Data;
using FirstProject.Domain;
using FirstProject.Models.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNet.Identity;

namespace FirstProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[System.Web.Http.HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
	public class TipsForEveryOnesController : ControllerBase
    {
        private readonly MyDBContext _context;
		private readonly IMapper _mapper;

        public TipsForEveryOnesController(MyDBContext context, IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
        }

        // GET: api/TipsForEveryOnes
        [HttpGet]
		[System.Web.Http.HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
		public IEnumerable<TipsForEveryOneDTO> GetTipsForEveryOne()
        {
            var hints = _context.TipsForEveryOne;
			var hintsDTO = _mapper.Map<List<TipsForEveryOneDTO>>(hints);

			return (hintsDTO);
        }

        // GET: api/TipsForEveryOnes/5
        [HttpGet("{id}")]
		[System.Web.Http.HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
		public async Task<ActionResult<TipsForEveryOneDTO>> GetTipsForEveryOne([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hint = await _context.TipsForEveryOne.FindAsync(id);
			var hintDTO = _mapper.Map<TipsForEveryOneDTO>(hint);

            if (hintDTO == null)
            {
                return NotFound();
            }

            return Ok(hintDTO);
        }

        // PUT: api/TipsForEveryOnes/5
        [HttpPut("{id}")]
		[System.Web.Http.HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
		public async Task<IActionResult> PutTipsForEveryOne([FromRoute] Guid id, [FromBody] TipsForEveryOne tipsForEveryOne)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipsForEveryOne.Id)
            {
                return BadRequest();
            }

            _context.Entry(tipsForEveryOne).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipsForEveryOneExists(id))
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

        // POST: api/TipsForEveryOnes
        [HttpPost]
		[System.Web.Http.HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
		public async Task<IActionResult> PostTipsForEveryOne([FromBody] TipsForEveryOne tipsForEveryOne)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TipsForEveryOne.Add(tipsForEveryOne);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipsForEveryOne", new { id = tipsForEveryOne.Id }, tipsForEveryOne);
        }

        // DELETE: api/TipsForEveryOnes/5
        [HttpDelete("{id}")]
		[Authorize
			
			]
        public async Task<IActionResult> DeleteTipsForEveryOne([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tipsForEveryOne = await _context.TipsForEveryOne.FindAsync(id);
            if (tipsForEveryOne == null)
            {
                return NotFound();
            }

            _context.TipsForEveryOne.Remove(tipsForEveryOne);
            await _context.SaveChangesAsync();

            return Ok(tipsForEveryOne);
        }

        private bool TipsForEveryOneExists(Guid id)
        {
            return _context.TipsForEveryOne.Any(e => e.Id == id);
        }
    }
}
