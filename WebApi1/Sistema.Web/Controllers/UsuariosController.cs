using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Personas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController:ControllerBase
    {
        private readonly DbContextSistema context;

        public UsuariosController(DbContextSistema context)
        {
            this.context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Persona>> Get()
        {
            return context.Personas.ToList();
        }
        [HttpGet("{id}",Name ="ObtenerAutor")]
        public ActionResult<Persona> Get(int Id)
        {
            var persona = context.Personas.FirstOrDefault(x => x.IdPersona == Id);
            if(persona == null)
            {
                return NotFound();
            }
            else
            {
                return persona;
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody]Persona persona)
        {
            context.Personas.Add(persona);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerAutor", new {  Id = persona.IdPersona }, persona);
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id,[FromBody]Persona value)
        {
            if(id != value.IdPersona)
            {
                return BadRequest();
            }
            else
            {
                context.Entry(value).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
        }
        [HttpDelete("{id}")]
        public ActionResult<Persona> Delete(int id)
        {
            var autor = context.Personas.FirstOrDefault(x => x.IdPersona == id);
            if (autor == null)
            {
                return NotFound();
            }
            else
            {
                context.Personas.Remove(autor);
                context.SaveChanges();
                return autor;
            }
        }
    }
}
