using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAC.Models
{
    public class Persona
    {
        [Key]
        public int ID { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public Boolean Estado { get; set; } = true;
    }
}
