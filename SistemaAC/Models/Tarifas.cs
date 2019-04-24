using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAC.Models
{
    public class Tarifas
    {
        [Key]
        public int TarifaID { get; set; }
        public int ActividadesID { get; set; }
        public double ValorEst { get; set; }
        public double ValorEmp { get; set; }
        public double ValorFam { get; set; }
        public double ValorGrad { get; set; }
        public Actividades Actividades { get; set; }
    }
}
