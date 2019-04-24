using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SistemaAC.Data;
using SistemaAC.Models;

namespace SistemaAC.ModelsClass
{
    public class MaquinariaModels
    {
        private ApplicationDbContext context;
        private List<IdentityError> errorList = new List<IdentityError>();
        private string code = "", des = "";
        public MaquinariaModels(ApplicationDbContext context)
        {
            this.context = context;
        }

        internal List<Actividades> getActividadesM()
        {
            return context.Actividades.Where(c => c.Estado == true).ToList();
        }
        public List<Actividades> getActividad(int id)
        {
            return context.Actividades.Where(a => a.ActividadesID == id).ToList();
        }
        public List<Maquinaria> getMaquinaria(int id)
        {
            return context.Maquinaria.Where(c => c.MaquinariaID == id).ToList();
        }
        public List<IdentityError> agregarMaquinaria(int id, string nombre, string cantidad, int actividad, string funcion)
        {
            var maquinaria = new Maquinaria
            {
                Nombre = nombre,
                Cantidad = cantidad,
                ActividadesID = actividad,
            };
            try
            {
                context.Add(maquinaria);
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
        public List<object[]> filtrarMaquinaria(int numPagina, string valor, string order)
        {
            int cant, numRegistros = 0, inicio = 0, reg_por_pagina = 5;
            int can_paginas, pagina;
            string dataFilter = "", paginador = "", Estado = null;
            List<object[]> data = new List<object[]>();
            IEnumerable<Maquinaria> query;
            List<Maquinaria> maquinarias = null;
            switch (order)
            {
                case "nombre":
                    maquinarias = context.Maquinaria.OrderBy(c => c.Nombre).ToList();
                    break;
                case "cantidad":
                    maquinarias = context.Maquinaria.OrderBy(c => c.Cantidad).ToList();
                    break;
                case "actividad":
                    maquinarias = context.Maquinaria.OrderBy(c => c.ActividadesID).ToList();
                    break;
            }
            numRegistros = maquinarias.Count;
            inicio = (numPagina - 1) * reg_por_pagina;
            can_paginas = (numRegistros / reg_por_pagina);
            if ((numRegistros % reg_por_pagina) > 0)
            {
                can_paginas += 1;
            }
            if (valor == "null")
            {
                query = maquinarias.Skip(inicio).Take(reg_por_pagina);
            }
            else
            {
                query = maquinarias.Where(c => c.Nombre.StartsWith(valor) || c.Cantidad.StartsWith(valor)).Skip(inicio).Take(reg_por_pagina);
            }
            cant = query.Count();
            foreach (var item in query)
            {
                var actividad = getActividad(item.ActividadesID);
                dataFilter += "<tr>" +
                    "<td>" + item.Nombre + "</td>" +
                    "<td>" + item.Cantidad + "</td>" +
                    "<td>" + actividad[0].Nombre + "</td>" +
                    "<td>" +
                    "<a data-toggle='modal' data-target='#modalDS' onclick='editarMaquinaria(" + item.MaquinariaID + ',' + 1 + ")'  class='btn btn-success'>Editar</a>" +
                    "</td>" +
                    "<td>" +
                    "<a data-toggle='modal' data-target='#ModalDeleteDS' onclick='deleteMaquinaria(" + item.MaquinariaID + ")'  class='btn btn-danger'>Eliminar</a>" +
                    "</td>" +
                "</tr>";

            }
            if (valor == "null")
            {
                if (numPagina > 1)
                {
                    pagina = numPagina - 1;
                    paginador += "<a class='btn btn-default' onclick='filtrarMaquinaria(" + 1 + ',' + '"' + order + '"' + ")'> << </a>" +
                    "<a class='btn btn-default' onclick='filtrarMaquinaria(" + pagina + ',' + '"' + order + '"' + ")'> < </a>";
                }
                if (1 < can_paginas)
                {
                    paginador += "<strong class='btn btn-success'>" + numPagina + ".de." + can_paginas + "</strong>";
                }
                if (numPagina < can_paginas)
                {
                    pagina = numPagina + 1;
                    paginador += "<a class='btn btn-default' onclick='filtrarMaquinaria(" + pagina + ',' + '"' + order + '"' + ")'>  > </a>" +
                                 "<a class='btn btn-default' onclick='filtrarMaquinaria(" + can_paginas + ',' + '"' + order + '"' + ")'> >> </a>";
                }
            }
            object[] dataObj = { dataFilter, paginador };
            data.Add(dataObj);
            return data;
        }
        public List<IdentityError> editarMaquinaria(int id, string nombre, string cantidad, int actividad, int funcion)
        {
            var maquinaria = new Maquinaria
            {
                MaquinariaID = id,
                Nombre = nombre,
                Cantidad = cantidad,
                ActividadesID = actividad,
            };
            try
            {
                context.Update(maquinaria);
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
        internal List<IdentityError> deleteMaquinaria(int id)
        {
            var maquinaria = context.Maquinaria.SingleOrDefault(m => m.MaquinariaID == id);
            if (maquinaria == null)
            {
                code = "0";
                des = "Not";
            }
            else
            {
                context.Maquinaria.Remove(maquinaria);
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
