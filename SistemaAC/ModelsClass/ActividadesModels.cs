using Microsoft.AspNetCore.Identity;
using SistemaAC.Data;
using SistemaAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAC.ModelsClass
{
    public class ActividadesModels
    {
        private ApplicationDbContext context;
        private Boolean estados;

        public ActividadesModels(ApplicationDbContext context)
        {
            this.context = context;
            //filtrarDatos(1, "Android");
        }

        public List<IdentityError> guardarActividad(string nombre, string cantidad, string descripcion, string estado)
        {
            var errorList = new List<IdentityError>();
            var actividad = new Actividades
            {
                Nombre = nombre,
                Cantidad = cantidad,
                Descripcion = descripcion,
                Estado = Convert.ToBoolean(estado)
            };
            context.Add(actividad);
            context.SaveChanges();
            errorList.Add(new IdentityError
            {
                Code = "Save",
                Description = "Save"
            });
            return errorList;
        }
        public List<object[]> filtrarDatos(int numPagina, string valor, string order)
        {
            int cant, numRegistros = 0, inicio = 0, reg_por_pagina = 5;
            int can_paginas, pagina;
            string dataFilter = "", paginador = "", Estado = null;
            List<object[]> data = new List<object[]>();
            IEnumerable<Actividades> query;
            List<Actividades> actividades = null;
            switch (order)
            {
                case "nombre":
                    actividades = context.Actividades.OrderBy(a => a.Nombre).ToList();
                    break;
                case "can":
                    actividades = context.Actividades.OrderBy(a => a.Cantidad).ToList();
                    break;
                case "des":
                    actividades = context.Actividades.OrderBy(a => a.Descripcion).ToList();
                    break;
                case "estado":
                    actividades = context.Actividades.OrderBy(a => a.Estado).ToList();
                    break;
            }
            numRegistros = actividades.Count;
            inicio = (numPagina - 1) * reg_por_pagina;
            can_paginas = (numRegistros / reg_por_pagina);
            if ((numRegistros % reg_por_pagina) > 0)
            {
                numRegistros += 1;
            }
            if (valor == "null")
            {
                query = actividades.Skip(inicio).Take(reg_por_pagina);
            }
            else
            {
                query = actividades.Where(a => a.Nombre.StartsWith(valor) || a.Descripcion.StartsWith(valor)).Skip(inicio).Take(reg_por_pagina);
            }
            cant = query.Count();
            foreach (var item in query)
            {
                if (item.Estado == true)
                {
                    Estado = "<a data-toggle='modal' data-target='#ModaEstado' onclick = 'editarEstado(" + item.ActividadesID + ',' + 0 + ")' class='btn btn-success'>Activa</a>";
                }
                else
                {
                    Estado = "<a data-toggle='modal' data-target='#ModaEstado' onclick = 'editarEstado(" + item.ActividadesID + ',' + 0 + ")' class='btn btn-danger'>No activa</a>";
                }
                dataFilter += "<tr>" + 
                    "<td>" + item.Nombre + "</td>" +
                    "<td>" + item.Descripcion + "</td>" +
                    "<td>" + item.Cantidad + "</td>" + 
                    "<td>" + Estado + " </td>" + 
                    "<td>" +
                    "<a data-toggle='modal' data-target='#modalAC' onclick = 'editarEstado(" + item.ActividadesID + ',' + 1 + ")' class='btn btn-success'>Editar</a>" + 
                    "</td>" + 
                  "</tr>";
            }
            if (valor == "null")
            {
                if (numPagina > 1)
                {
                    pagina = numPagina - 1;
                    paginador += "<a class='btn btn-default' onclick='filtrarDatos(" + 1 + ',' + '"' + order + '"' + ")'> << </a>" +
                    "<a class='btn btn-default' onclick='filtrarDatos(" + pagina + ',' + '"' + order + '"' + ")'> < </a>";
                }
                if (1 < can_paginas)
                {
                    paginador += "<strong class='btn btn-success'>" + numPagina + ".de." + can_paginas + "</strong>";
                }
                if (numPagina < can_paginas)
                {
                    pagina = numPagina + 1;
                    paginador += "<a class='btn btn-default' onclick='filtrarDatos(" + pagina + ',' + '"' + order + '"' + ")'>  > </a>" +
                                 "<a class='btn btn-default' onclick='filtrarDatos(" + can_paginas + ',' + '"' + order + '"' + ")'> >> </a>";
                }
            }
            object[] dataObj = { dataFilter, paginador };
            data.Add(dataObj);
            return data;
        }
        
        public List<Actividades> getActividades(int id)
        {
            return context.Actividades.Where(a => a.ActividadesID == id).ToList();
        }
        public List<IdentityError> editarActividad(int idActividad, string nombre, string cantidad, string descripcion, Boolean estado, int funcion)
        {
            var errorList = new List<IdentityError>();
            string code = "", des = "";
            switch (funcion)
            {
                case 0:
                    if (estado)
                    {
                        estados = false;
                    }
                    else
                    {
                        estados = true;
                    }
                    break;
                case 1:
                    estados = estado;
                    break;
            }
            var actividad = new Actividades()
            {
                ActividadesID = idActividad,
                Nombre = nombre,
                Cantidad = cantidad,
                Descripcion = descripcion,
                Estado = estados
            };
            try
            {
                context.Update(actividad);
                context.SaveChanges();
                code = "Save";
                des = "Save";
            }
            catch (Exception ex)
            {
                code = "Error";
                des = ex.Message;
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
