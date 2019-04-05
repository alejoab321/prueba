using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sistema.Entidades.Personas
{
    public class Persona
    {
        [Required]
        public int IdPersona { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener maximo" +
           "50 caracteres y minimo tres caracteres")]
        public string Nombre { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener maximo" +
           "50 caracteres y minimo tres caracteres")]
        public string Apellidos { get; set; }
    }
}
