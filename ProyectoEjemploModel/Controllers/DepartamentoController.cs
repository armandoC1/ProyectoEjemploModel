using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        //accion que muestra
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

        //accion que muestra el formulario
        public async Task<IActionResult> Edit(int id)
        {
            var departamento = await contextoDatos.Departamentos.SingleOrDefaultAsync(d => d.Id == id);
            return View(departamento);
        }

        //accion que recibe los datos modificados del formulario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                var temp = await contextoDatos.Departamentos.SingleOrDefaultAsync(d => d.Id == departamento.Id);
                temp.Nombre = departamento.Nombre;

                contextoDatos.Departamentos.Update(temp);
                await contextoDatos.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
                return View(departamento);
        }

        //acccion que muestra el formulario de eliminar
        public async Task<IActionResult> Delete(int id)
        {
            var departamento = await contextoDatos.Departamentos.SingleOrDefaultAsync(d => d.Id == id);
            return View(departamento);
        }

        //accion que elimina el departamneto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Departamento departamento)
        {
            var temp = await contextoDatos.Departamentos.SingleOrDefaultAsync(d => d.Id == departamento.Id);

             if(temp != null)
            {
                contextoDatos.Departamentos.Remove(temp);
                await contextoDatos.SaveChangesAsync();
                return RedirectToAction("Index");
            }
             else
                return View(temp);  
        }
    }
}
