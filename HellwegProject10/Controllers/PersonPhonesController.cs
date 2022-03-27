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
    public class PersonPhonesController : ControllerBase
    {
        private readonly Adventureworks2019Context _context;

        public PersonPhonesController(Adventureworks2019Context context)
        {
            _context = context;
        }

        // GET: api/PersonPhones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonPhone>>> GetPersonPhones()
        {
            return await _context.PersonPhones.ToListAsync();
        }

        // GET: api/PersonPhones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonPhone>> GetPersonPhone(int id)
        {
            var personPhone = await _context.PersonPhones.FindAsync(id);

            if (personPhone == null)
            {
                return NotFound();
            }

            return personPhone;
        }

        // PUT: api/PersonPhones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonPhone(int id, PersonPhone personPhone)
        {
            if (id != personPhone.BusinessEntityId)
            {
                return BadRequest();
            }

            _context.Entry(personPhone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonPhoneExists(id))
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

        // POST: api/PersonPhones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PersonPhone>> PostPersonPhone(PersonPhone personPhone)
        {
            _context.PersonPhones.Add(personPhone);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PersonPhoneExists(personPhone.BusinessEntityId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPersonPhone", new { id = personPhone.BusinessEntityId }, personPhone);
        }

        // DELETE: api/PersonPhones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonPhone(int id)
        {
            var personPhone = await _context.PersonPhones.FindAsync(id);
            if (personPhone == null)
            {
                return NotFound();
            }

            _context.PersonPhones.Remove(personPhone);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonPhoneExists(int id)
        {
            return _context.PersonPhones.Any(e => e.BusinessEntityId == id);
        }
    }
}
