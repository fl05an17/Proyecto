using Microsoft.AspNetCore.Identity;
using SistemaAC.Data;
using SistemaAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAC.ModelsClass
{
    public class TarifaModels
    {
        private ApplicationDbContext context;
        private List<IdentityError> errorList = new List<IdentityError>();
        private string code = "", des = "";
        public TarifaModels(ApplicationDbContext context)
        {
            this.context = context;
        }
        internal List<Actividades> getActividadesT()
        {
            return context.Actividades.Where(c => c.Estado == true).ToList();
        }
        public List<Actividades> getActividad(int id)
        {
            return context.Actividades.Where(c => c.ActividadesID == id).ToList();
        }
        public List<Tarifas> getTarifas(int id)
        {
            return context.Tarifas.Where(c => c.TarifaID == id).ToList();
        }
        public List<IdentityError> agregarTarifa(int id, double valorEst, double valorEmp, double valorFam, double valorGrad, int actividad, string funcion)
        {
            var tarifa = new Tarifas
            {
                ValorEst = valorEst,
                ValorEmp = valorEmp,
                ValorFam = valorFam,
                ValorGrad = valorGrad,
                ActividadesID = actividad,
            };
            try
            {
                context.Add(tarifa);
                context.SaveChanges();
                code = "Save";
                des = "Save";
            }
            catch (Exception e)
            {

                code = "error";
                des = e.Message;
            }
            errorList.Add(new IdentityError
            {
                Code = code,
                Description = des
            });
            return errorList;
        }
        public List<object[]> filtrarTarifa(int numPagina, string valor, string order)
        {
            int cant, numRegistros = 0, inicio = 0, reg_por_pagina = 5;
            int can_paginas, pagina;
            string dataFilter = "", paginador = "", Estado = null;
            List<object[]> data = new List<object[]>();
            IEnumerable<Tarifas> query;
            List<Tarifas> tarifas = null;
            switch (order)
            {
                case "valorEst":
                    tarifas = context.Tarifas.OrderBy(c => c.ValorEst).ToList();
                    break;
                case "valorEmp":
                    tarifas = context.Tarifas.OrderBy(c => c.ValorEmp).ToList();
                    break;
                case "valorFam":
                    tarifas = context.Tarifas.OrderBy(c => c.ValorFam).ToList();
                    break;
                case "valorGrad":
                    tarifas = context.Tarifas.OrderBy(c => c.ValorGrad).ToList();
                    break;
                case "actividad":
                    tarifas = context.Tarifas.OrderBy(c => c.ActividadesID).ToList();
                    break;

            }
            numRegistros = tarifas.Count;
            inicio = (numPagina - 1) * reg_por_pagina;
            can_paginas = (numRegistros / reg_por_pagina);
            if ((numRegistros % reg_por_pagina) > 0)
            {
                can_paginas += 1;
            }
            if (valor == "null")
            {
                query = tarifas.Skip(inicio).Take(reg_por_pagina);
            }
            else
            {
                query = tarifas.Where(c => c.ValorEst.Equals(valor) || c.ValorEmp.Equals(valor)).Skip(inicio).Take(reg_por_pagina);
            }
            cant = query.Count();
            foreach (var item in query)
            {
                var actividad = getActividad(item.ActividadesID);
                dataFilter += "<tr>" +
                    "<td>" + actividad[0].Nombre + "</td>" +
                    "<td>" + item.ValorEst + "</td>" +
                    "<td>" + item.ValorEmp + "</td>" +
                    "<td>" + item.ValorFam + "</td>" +
                    "<td>" + item.ValorGrad + "</td>" +
                    "<td>" +
                    "<a data-toggle='modal' data-target='#modalES' onclick='editarTarifa(" + item.TarifaID + ',' + 1 + ")'  class='btn btn-success'>Editar</a>" +
                    "</td>" +
                    "<td>" +
                    "<a data-toggle='modal' data-target='#ModalDeleteES' onclick='deleteTarifa(" + item.TarifaID + ")'  class='btn btn-danger'>Eliminar</a>" +
                    "</td>" +
                "</tr>";

            }
            if (valor == "null")
            {
                if (numPagina > 1)
                {
                    pagina = numPagina - 1;
                    paginador += "<a class='btn btn-default' onclick='filtrarTarifa(" + 1 + ',' + '"' + order + '"' + ")'> << </a>" +
                    "<a class='btn btn-default' onclick='filtrarTarifa(" + pagina + ',' + '"' + order + '"' + ")'> < </a>";
                }
                if (1 < can_paginas)
                {
                    paginador += "<strong class='btn btn-success'>" + numPagina + ".de." + can_paginas + "</strong>";
                }
                if (numPagina < can_paginas)
                {
                    pagina = numPagina + 1;
                    paginador += "<a class='btn btn-default' onclick='filtrarTarifa(" + pagina + ',' + '"' + order + '"' + ")'>  > </a>" +
                                 "<a class='btn btn-default' onclick='filtrarTarifa(" + can_paginas + ',' + '"' + order + '"' + ")'> >> </a>";
                }
            }
            object[] dataObj = { dataFilter, paginador };
            data.Add(dataObj);
            return data;
        }
        public List<IdentityError> editarTarifa(int id, double valorEst, double valorEmp, double valorFam, double valorGrad, int actividad, int funcion)
        {
            var tarifa = new Tarifas
            {
                TarifaID = id,
                ValorEst = valorEst,
                ValorEmp = valorEmp,
                ValorFam = valorFam,
                ValorGrad = valorGrad,
                ActividadesID = actividad,
            };
            try
            {
                context.Update(tarifa);
                context.SaveChanges();
                code = "Save";
                des = "Save";
            }
            catch (Exception ex)
            {
                code = "error";
                des = ex.Message;
            }
            errorList.Add(new IdentityError
            {
                Code = code,
                Description = des
            });

            return errorList;
        }
        internal List<IdentityError> deleteTarifa(int id)
        {
            var tarifas = context.Tarifas.SingleOrDefault(m => m.TarifaID == id);
            if (tarifas == null)
            {
                code = "0";
                des = "Not";
            }
            else
            {
                context.Tarifas.Remove(tarifas);
                context.SaveChanges();
                code = "1";
                des = "Dlete";
            }
            errorList.Add(new IdentityError
            {
                Code = code,
                Description = des
            });
            return errorList;
        }
    }
}
