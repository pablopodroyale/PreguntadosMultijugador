using preguntados_ppodgaiz.Models;
using preguntados_ppodgaiz.Models.Dominio;
using preguntados_ppodgaiz.Models.Enums;
using preguntados_ppodgaiz.Models.ViewModels.Categoria;
using preguntados_ppodgaiz.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace preguntados_ppodgaiz.Controllers
{
    [Authorize]
    public class CategoriaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var categorias = new Repositorio<Categoria>(db).TraerTodos().ToList()
               .Select(m => new CategoriaGrillaViewModel(m)).ToList();

            return View("Index", categorias);
        }

        [Authorize(Roles = RoleConst.Administrador + "," + RoleConst.Suscriptor)]
        public ActionResult Create()
        {
            var model = new CategoriaAbmViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CategoriaAbmViewModel model)
        {
            if (ModelState.IsValid)
            {
                var categoria = new Categoria(model, db);

                new Repositorio<Categoria>(db).Crear(categoria);

                return RedirectToAction("Index");

            }

            return View(model);
        }

        public ActionResult Delete(Guid id)
        {
            var errores = new List<string>();
            var repo = new Repositorio<Categoria>(db);
            repo.Eliminar(repo.Traer(id));
            return RedirectToAction("Index");
        }

        public ActionResult Edit(Guid id)
        {
            var categoria = new Repositorio<Categoria>(db).Traer(id);
            var model = new CategoriaAbmViewModel(categoria);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CategoriaAbmViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = new Repositorio<Categoria>(db);
                var categoria = repo.Traer(model.Id);
                categoria.Modificar(model);
                repo.Modificar(categoria);

                return RedirectToAction("Index");

            }
            return View(model);
        }

        public ActionResult GetCategorias()
        {
            var categorias = new Repositorio<Categoria>(db).TraerTodos().ToList()
             .Select(m => new  {
                 fillStyle = m.Color,
                 text = m.Nombre
             }).ToArray();

            return Json(categorias, JsonRequestBehavior.AllowGet);
        }


    }
}