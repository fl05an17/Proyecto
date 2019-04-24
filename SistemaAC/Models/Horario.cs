using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAC.Models
{
    public class Horario
    {
        [Key]
        public int HorarioID { get; set; }
        public int ActividadesID { get; set; }
        public string Dia { get; set; }
        public string Hora { get; set; }
        public Actividades Actividades { get; set; }
    }
}
