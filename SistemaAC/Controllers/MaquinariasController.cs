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
    public class MaquinariasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private MaquinariaModels maquinariaModels;

        public MaquinariasController(ApplicationDbContext context)
        {
            _context = context;
            maquinariaModels = new MaquinariaModels(context);
        }

        // GET: Maquinarias
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public List<Actividades> getActividadesM()
        {
            return maquinariaModels.getActividadesM();
        }
        public List<IdentityError> agregarMaquinaria(int id, string nombre, string cantidad, int actividad, string funcion)
        {
            return maquinariaModels.agregarMaquinaria(id, nombre, cantidad, actividad, funcion);
        }
        public List<object[]> filtrarMaquinaria(int numPagina, string valor, string order)
        {
            return maquinariaModels.filtrarMaquinaria(numPagina, valor, order);
        }
        public List<Maquinaria> getMaquinaria(int id)
        {
            return maquinariaModels.getMaquinaria(id);
        }
        public List<IdentityError> editarMaquinaria(int id, string nombre, string cantidad, int actividad, int funcion)
        {
            return maquinariaModels.editarMaquinaria(id, nombre, cantidad, actividad, funcion);
        }
        public List<IdentityError> deleteMaquinaria(int id)
        {
            return maquinariaModels.deleteMaquinaria(id);
        }
    }
}
