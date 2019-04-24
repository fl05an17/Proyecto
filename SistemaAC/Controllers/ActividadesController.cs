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
    public class ActividadesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ActividadesModels actividadesModels;

        public ActividadesController(ApplicationDbContext context)
        {
            _context = context;
            actividadesModels = new ActividadesModels(_context);
        }

        // GET: Actividades
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public List<object[]> filtrarDatos(int numPagina, string valor, string order)
        {
            return actividadesModels.filtrarDatos(numPagina, valor, order);
        }
        public List<Actividades> getActividades(int id)
        {
            return actividadesModels.getActividades(id);
        }
        public List<IdentityError> editarActividad(int id, string nombre, string cantidad, string descripcion, Boolean estado, int funcion)
        {
            return actividadesModels.editarActividad(id, nombre, cantidad, descripcion, estado, funcion);
        }
        public List<IdentityError> guardarActividad(string nombre, string cantidad, string descripcion, string estado)
        {
            return actividadesModels.guardarActividad(nombre, cantidad, descripcion, estado);
        }
    }
}
