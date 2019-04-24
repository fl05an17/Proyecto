using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAC.Models
{
    public class Maquinaria
    {
        [Key]
        public int MaquinariaID { get; set; }
        public int ActividadesID { get; set; }
        public string Nombre { get; set; }
        public string Cantidad { get; set; }
        public Actividades Actividades { get; set; }
    }
}
