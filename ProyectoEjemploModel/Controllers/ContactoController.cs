using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoEjemploModel.Models;
using X.PagedList;

namespace ProyectoEjemploModel.Controllers
{
    public class ContactoController : Controller
    {
        private readonly ContextoDatos contextoDatos;

        //esta es la inyeccion de contexo de datos
        public ContactoController(ContextoDatos contextoDatos)
        {
            this.contextoDatos = contextoDatos;
        }

        //pagina principal
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 10; //numero de registros por pagina
            int pageNumber = (page ?? 1); //numero de pagina en la que se va ubicar al inicio

            var contactos = await contextoDatos.Contactos.OrderByDescending(c => c.Id).ToPagedListAsync(pageNumber, pageSize);
            return View(contactos);
        }

        //accion que muestra los datos
        public async Task<IActionResult>  Details(int id)
        {
            var contacto = await contextoDatos.Contactos.SingleOrDefaultAsync(c => c.Id == c.Id);
            return View(contacto);
        }

        //accion que edita los datos recibidos
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                contextoDatos.Contactos.Add(contacto);
                await contextoDatos.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
                return View(contacto);
        }

        //accion que muestra el formulario de editar
        public async Task<IActionResult> Edit(int id)
        {
            var contacto = await contextoDatos.Contactos.SingleOrDefaultAsync(c => c.Id == c.Id);
            return View(contacto);
        }

        //accion que edita los datos de la vista del formulario

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Edit(int id, Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                var t = await contextoDatos.Contactos.SingleOrDefaultAsync(c => c.Id == contacto.Id);
                t.Nombre = contacto.Nombre;
                t.Apellido = contacto.Apellido;
                t.Email = contacto.Email;
                t.Telefono = contacto.Telefono;

                contextoDatos.Contactos.Update(t);
                await contextoDatos.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
                return View(contacto);
        }

        //accion que muestra el formulario de eliminar
        public async Task<IActionResult> Delete(int id)
        {
            var contacto = await contextoDatos.Contactos.SingleOrDefaultAsync(c => c.Id == id);
            return View(contacto);
        }

        //accion que elimina los datos obtenidos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Contacto contacto)
        {
            var t = await contextoDatos.Contactos.SingleOrDefaultAsync(c => c.Id == contacto.Id);

            if (t != null)
            {
                contextoDatos.Contactos.Remove(t);
                await contextoDatos.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
                return View(t);
        }


    }
}
