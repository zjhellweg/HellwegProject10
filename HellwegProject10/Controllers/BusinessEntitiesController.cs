#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HellwegProject10.Data;
using HellwegProject10.Models;

namespace HellwegProject10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessEntitiesController : ControllerBase
    {
        private readonly Adventureworks2019Context _context;

        public BusinessEntitiesController(Adventureworks2019Context context)
        {
            _context = context;
        }

        // GET: api/BusinessEntities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusinessEntity>>> GetBusinessEntities()
        {
            return await _context.BusinessEntities.ToListAsync();
        }

        // GET: api/BusinessEntities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BusinessEntity>> GetBusinessEntity(int id)
        {
            var businessEntity = await _context.BusinessEntities.FindAsync(id);

            if (businessEntity == null)
            {
                return NotFound();
            }

            return businessEntity;
        }

        // PUT: api/BusinessEntities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBusinessEntity(int id, BusinessEntity businessEntity)
        {
            if (id != businessEntity.BusinessEntityId)
            {
                return BadRequest();
            }

            _context.Entry(businessEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessEntityExists(id))
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

        // POST: api/BusinessEntities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BusinessEntity>> PostBusinessEntity(BusinessEntity businessEntity)
        {
            _context.BusinessEntities.Add(businessEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBusinessEntity", new { id = businessEntity.BusinessEntityId }, businessEntity);
        }

        // DELETE: api/BusinessEntities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusinessEntity(int id)
        {
            var businessEntity = await _context.BusinessEntities.FindAsync(id);
            if (businessEntity == null)
            {
                return NotFound();
            }

            _context.BusinessEntities.Remove(businessEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BusinessEntityExists(int id)
        {
            return _context.BusinessEntities.Any(e => e.BusinessEntityId == id);
        }
    }
}
