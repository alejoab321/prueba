using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Personas;
using Sistema.Web.Models.Persona;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public PersonasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Personas/ListarPersonas
        [HttpGet("[action]")]
        public async Task <IEnumerable<PersonaViewModel>> ListarPersonas()
        {
            var persona = await _context.Personas.ToListAsync();
            return persona.Select(c => new PersonaViewModel {
                IdPersona = c.IdPersona,
                Nombre    = c.Nombre,
                Apellidos = c.Apellidos
            });
        }

        // GET: api/Personas/ConsultarPersona/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> ConsultarPersona([FromRoute] int id)
        {
            var persona = await _context.Personas.FindAsync(id);

            if (persona == null)
            {
                return NotFound();
            }
            return Ok(new PersonaViewModel {
                IdPersona = persona.IdPersona,
                Nombre    = persona.Nombre,
                Apellidos = persona.Apellidos
            });
        }

        // PUT: api/Personas/ActualizarPersona
        [HttpPut("[action]")]
        public async Task<IActionResult> ActualizarPersona([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.IdPersona < 0)
            {
                return BadRequest();
            }

            var persona = await _context.Personas.FirstOrDefaultAsync(c =>
            c.IdPersona == model.IdPersona);

            if (persona == null)
            {
                return NotFound();
            }

            persona.Nombre = model.Nombre;
            persona.Apellidos = model.Apellidos;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
            return Ok();
        }

        // POST: api/Personas/InsertarPersona
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertarPersona([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Persona persona = new Persona
            {
                IdPersona = model.IdPersona,
                Nombre    = model.Nombre,
                Apellidos = model.Apellidos,
               
            };

            _context.Personas.Add(persona);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE: api/Personas/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeletePersona([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoria = await _context.Personas.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Personas.Remove(categoria);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok(categoria);
        }

        private bool PersonaExists(int id)
        {
            return _context.Personas.Any(e => e.IdPersona == id);
        }
    }
}