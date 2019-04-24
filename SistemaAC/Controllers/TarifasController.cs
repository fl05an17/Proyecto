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
    public class TarifasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private TarifaModels tarifasModels;
        public TarifasController(ApplicationDbContext context)
        {
            _context = context;
            tarifasModels = new TarifaModels(context);
        }

        // GET: Tarifas
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public List<Actividades> getActividadesT()
        {
            return tarifasModels.getActividadesT();
        }
        public List<IdentityError> agregarTarifa(int id, double valorEst, double valorEmp, double valorFam, double valorGrad, int actividad, string funcion)
        {
            return tarifasModels.agregarTarifa(id, valorEst, valorEmp, valorFam, valorGrad, actividad, funcion);
        }
        public List<object[]> filtrarTarifa(int numPagina, string valor, string order)
        {
            return tarifasModels.filtrarTarifa(numPagina, valor, order);
        }
        public List<Tarifas> getTarifas(int id)
        {
            return tarifasModels.getTarifas(id);
        }
        public List<IdentityError> editarTarifa(int id, double valorEst, double valorEmp, double valorFam, double valorGrad, int actividad, int funcion)
        {
            return tarifasModels.editarTarifa(id, valorEst, valorEmp, valorFam, valorGrad, actividad, funcion);
        }
        public List<IdentityError> deleteTarifa(int id)
        {
            return tarifasModels.deleteTarifa(id);
        }
    }
}
