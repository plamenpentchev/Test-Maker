using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMakerFreeWebApp.Data.WFSSDTest;
using Newtonsoft.Json;
using Mapster;

namespace TestMakerFreeWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StammdatenController : ControllerBase
    {
        private readonly WFSSDTestContext _context;

        public StammdatenController(WFSSDTestContext context)
        {
            _context = context;
        }

        // GET: api/Stammdaten
        [HttpGet]
        public IActionResult GetStammdaten()
        {
            //IEnumerable<Stammdaten>
            //return _context.Stammdaten;
            var stData = _context.Stammdaten.ToArray();
            return new JsonResult(
                stData.Adapt<Stammdaten[]>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        // GET: api/Stammdaten/Absence/5
        [HttpGet("Absence/{perNr}")]
        public IActionResult Absence([FromRoute]string perNr)
        {
            var results = _context.StammdatenAbwesenheiten
                .Join(_context.Stammdaten,
                    s => s.PERNR,
                    a => a.PERNR,
                    (a, s) => new { STAMM=s, ABW=a})
                .Where(sAndA => sAndA.STAMM.PERNR == perNr);


            return new JsonResult(
                    results,
                    new JsonSerializerSettings()
                    {
                        Formatting = Formatting.Indented
                    }
                );
                
        }
        // GET: api/Stammdaten/Absence/00101295/0200
        [HttpGet("Absence/{perNr}/{abwArt}")]
        public IActionResult Absence([FromRoute]string perNr, [FromRoute] string abwArt)
        {
            //var results = _context.StammdatenAbwesenheiten
            //    .Join(_context.Stammdaten,
            //        s => s.PERNR,
            //        a => a.PERNR,
            //        (a, s) => new { STAMM = s, ABW = a })
            //    .Where(sAndA => sAndA.STAMM.PERNR == perNr && sAndA.ABW.AWART == abwArt);

            var results2 = (from stamm in _context.Stammdaten
                           join abw in _context.StammdatenAbwesenheiten on stamm.PERNR equals abw.PERNR
                           where stamm.PERNR == perNr && abw.AWART == abwArt
                           select abw).Distinct();


            return new JsonResult(
                    results2,
                    new JsonSerializerSettings()
                    {
                        Formatting = Formatting.Indented
                    }
                );

        }

        // GET: api/Stammdaten/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStammdaten([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stammdaten = await _context.Stammdaten.FindAsync(id);

            if (stammdaten == null)
            {
                return NotFound();
            }
            //return Ok(stammdaten);

            return new JsonResult(
                stammdaten,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        // PUT: api/Stammdaten/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStammdaten([FromRoute] string id, [FromBody] Stammdaten stammdaten)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stammdaten.PERNR)
            {
                return BadRequest();
            }

            _context.Entry(stammdaten).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StammdatenExists(id))
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

        // POST: api/Stammdaten
        [HttpPost]
        public async Task<IActionResult> PostStammdaten([FromBody] Stammdaten stammdaten)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Stammdaten.Add(stammdaten);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStammdaten", new { id = stammdaten.PERNR }, stammdaten);
        }

        // DELETE: api/Stammdaten/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStammdaten([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stammdaten = await _context.Stammdaten.FindAsync(id);
            if (stammdaten == null)
            {
                return NotFound();
            }

            _context.Stammdaten.Remove(stammdaten);
            await _context.SaveChangesAsync();

            return Ok(stammdaten);
        }

        private bool StammdatenExists(string id)
        {
            return _context.Stammdaten.Any(e => e.PERNR == id);
        }
    }
}