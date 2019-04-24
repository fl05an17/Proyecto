using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAC.Models
{
    public class Actividades
    {
        [Key]
        public int ActividadesID { get; set; }
        public string Nombre { get; set; }
        public string Cantidad { get; set; }
        public string Descripcion { get; set; }
        public Boolean Estado { get; set; } = true;
        public ICollection<Horario> Horario { get; set; }
        public ICollection<Tarifas> Tarifas { get; set; }
        public ICollection<Maquinaria> Maquinaria { get; set; }
    }
}
