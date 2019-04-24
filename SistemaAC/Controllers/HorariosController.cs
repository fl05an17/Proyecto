using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaAC.Data;
using SistemaAC.Models;
using SistemaAC.ModelsClass;

namespace SistemaAC.Controllers
{
    public class HorariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private HorarioModels horarioModels;
        public HorariosController(ApplicationDbContext context)
        {
            _context = context;
            horarioModels = new HorarioModels(context);
        }

        // GET: Horarios
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public List<Actividades> getActividades()
        {
            return horarioModels.getActividades();
        }
        public List<IdentityError> agregarHorario(int id, string dia, string hora, int actividad, string funcion)
        {
            return horarioModels.agregarHorario(id, dia, hora, actividad, funcion);
        }
        public List<object[]> filtrarHorario(int numPagina, string valor, string order)
        {
            return horarioModels.filtrarHorario(numPagina, valor, order);
        }
        public List<Horario> getHorario(int id)
        {
            return horarioModels.getHorario(id);
        }
        public List<IdentityError> editarHorario(int id, string dia, string hora, int actividad, int funcion)
        {
            return horarioModels.editarHorario(id, dia, hora, actividad, funcion);
        }
        public List<IdentityError> deleteHorario(int id)
        {
            return horarioModels.deleteHorario(id);
        }
    }
}
