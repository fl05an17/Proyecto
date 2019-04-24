using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SistemaAC.Data;
using SistemaAC.Models;

namespace SistemaAC.ModelsClass
{
    public class HorarioModels
    {
        private ApplicationDbContext context;
        private List<IdentityError> errorList = new List<IdentityError>();
        private string code = "", des = "";
        public HorarioModels(ApplicationDbContext context)
        {
            this.context = context;
        }

        internal List<Actividades> getActividades()
        {
            return context.Actividades.Where(c => c.Estado == true).ToList();
        }
        public List<Actividades> getActividad(int id)
        {
            return context.Actividades.Where(a => a.ActividadesID == id).ToList();
        }
        public List<Horario> getHorario(int id)
        {
            return context.Horario.Where(c => c.HorarioID == id).ToList();
        }
        public List<IdentityError> agregarHorario(int id, string dia, string hora, int actividad, string funcion)
        {
            var horario = new Horario
            {
                Dia = dia,
                Hora = hora,
                ActividadesID = actividad,
            };
            try
            {
                context.Add(horario);
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
        public List<object[]> filtrarHorario(int numPagina, string valor, string order)
        {
            int cant, numRegistros = 0, inicio = 0, reg_por_pagina = 5;
            int can_paginas, pagina;
            string dataFilter = "", paginador = "", Estado = null;
            List<object[]> data = new List<object[]>();
            IEnumerable<Horario> query;
            List<Horario> horarios = null;
            switch (order)
            {
                case "dia":
                    horarios = context.Horario.OrderBy(c => c.Dia).ToList();
                    break;
                case "hora":
                    horarios = context.Horario.OrderBy(c => c.Hora).ToList();
                    break;
                case "actividad":
                    horarios = context.Horario.OrderBy(c => c.ActividadesID).ToList();
                    break;

            }
            numRegistros = horarios.Count;
            inicio = (numPagina - 1) * reg_por_pagina;
            can_paginas = (numRegistros / reg_por_pagina);
            if ((numRegistros % reg_por_pagina) > 0)
            {
                can_paginas += 1;
            }
            if (valor == "null")
            {
                query = horarios.Skip(inicio).Take(reg_por_pagina);
            }
            else
            {
                query = horarios.Where(c => c.Dia.StartsWith(valor) || c.Hora.StartsWith(valor)).Skip(inicio).Take(reg_por_pagina);
            }
            cant = query.Count();
            foreach (var item in query)
            {
                var actividad = getActividad(item.ActividadesID);
                dataFilter += "<tr>" +
                    "<td>" + actividad[0].Nombre + "</td>" +
                    "<td>" + item.Dia + "</td>" +
                    "<td>" + item.Hora + "</td>" +
                    "<td>" +
                    "<a data-toggle='modal' data-target='#modalCS' onclick='editarHorario(" + item.HorarioID + ',' + 1 + ")'  class='btn btn-success'>Editar</a>" +
                    "</td>" +
                    "<td>" +
                    "<a data-toggle='modal' data-target='#ModalDeleteCS' onclick='deleteHorario(" + item.HorarioID + ")'  class='btn btn-danger'>Eliminar</a>" +
                    "</td>" +
                "</tr>";

            }
            if (valor == "null")
            {
                if (numPagina > 1)
                {
                    pagina = numPagina - 1;
                    paginador += "<a class='btn btn-default' onclick='filtrarHorario(" + 1 + ',' + '"' + order + '"' + ")'> << </a>" +
                    "<a class='btn btn-default' onclick='filtrarHorario(" + pagina + ',' + '"' + order + '"' + ")'> < </a>";
                }
                if (1 < can_paginas)
                {
                    paginador += "<strong class='btn btn-success'>" + numPagina + ".de." + can_paginas + "</strong>";
                }
                if (numPagina < can_paginas)
                {
                    pagina = numPagina + 1;
                    paginador += "<a class='btn btn-default' onclick='filtrarHorario(" + pagina + ',' + '"' + order + '"' + ")'>  > </a>" +
                                 "<a class='btn btn-default' onclick='filtrarHorario(" + can_paginas + ',' + '"' + order + '"' + ")'> >> </a>";
                }
            }
            object[] dataObj = { dataFilter, paginador };
            data.Add(dataObj);
            return data;
        }
        public List<IdentityError> editarHorario(int id, string dia, string hora, int actividad, int funcion)
        {
            var horario = new Horario
            {
                HorarioID = id,
                Dia = dia,
                Hora = hora,
                ActividadesID = actividad,
            };
            try
            {
                context.Update(horario);
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
        internal List<IdentityError> deleteHorario(int id)
        {
            var horario = context.Horario.SingleOrDefault(m => m.HorarioID == id);
            if (horario == null)
            {
                code = "0";
                des = "Not";
            }
            else
            {
                context.Horario.Remove(horario);
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
