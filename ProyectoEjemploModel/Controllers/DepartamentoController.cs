using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoEjemploModel.Models;
using X.PagedList;

namespace ProyectoEjemploModel.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly ContextoDatos contextoDatos;

        //esta es la inyeccion de contexo de datos
        public DepartamentoController(ContextoDatos contextoDatos)
        {
            this.contextoDatos = contextoDatos;
        }

        //accion que muestra la pagina principal para administrar deprtamentos
        public async Task<IActionResult> Index(int? page)
        {   
            int  pageSize = 2; //numero de registros por pagina
            int pageNumber = (page ?? 1);//numero de pagina actual, predeterminado 1 s entra vacio

            var departamentos = await contextoDatos.Departamentos.OrderByDescending(d => d.Id).ToPagedListAsync(pageNumber, pageSize);
            return View(departamentos);
        }

        //accion que edita
        public async Task<IActionResult> Details(int id)
        {
            var departamento = await contextoDatos.Departamentos.SingleOrDefaultAsync(d => d.Id == id);
            return View(departamento);
        }

        //accion qye muestra el formulario para crear 
        public IActionResult Create()
        {
            return View();
        }

        //accion que recibe los datos del formulario para guardarlos en la base de datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                contextoDatos.Departamentos.Add(departamento);
                await contextoDatos.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
                return View(departamento);
        }

        public async Task<IActionResult> Edit(int id)
        {

        }
    }
}
