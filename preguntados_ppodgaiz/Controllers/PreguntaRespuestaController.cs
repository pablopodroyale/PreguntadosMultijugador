using Microsoft.AspNet.Identity;
using preguntados_ppodgaiz.Helpers;
using preguntados_ppodgaiz.Models;
using preguntados_ppodgaiz.Models.Dominio;
using preguntados_ppodgaiz.Models.Enums;
using preguntados_ppodgaiz.Models.ViewModels.Filtro;
using preguntados_ppodgaiz.Models.ViewModels.PreguntaRespuesta;
using preguntados_ppodgaiz.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace preguntados_ppodgaiz.Controllers
{
    [Authorize]
    public class PreguntaRespuestaController : Controller
    {
        private ApplicationDbContext db;

        private const int CANT_RESPUESTAS = 4;
        private const string ERROR_CANT_RESPUESTAS = "Error, la cantidad de respuestas debe ser 4";
        private const string ERROR_CANT_CORRECTAS = "Error, la cantidad de respuestas correctas debe ser una";
        private const string ERROR_RESPUESTA_REPETIDA = "Error, la respuesta ya existe";
        private const int CANT_CORRECTAS = 1;
        private const string ERROR_NOMBRE_VACIO = "Error, el nombre no debe estar vacio";
        private const string ERROR_CATEGORIA_VACIA = "Error, la categoría no debe estar vacía";

        public PreguntaRespuestaController()
        {
            this.db = new ApplicationDbContext();
        }
        // GET: PreguntaRespuesta
        public ActionResult Index()
        {
            SearcherViewModel model = new SearcherViewModel();
            return View(model);
        }

        [Authorize(Roles = RoleConst.Administrador + "," + RoleConst.Suscriptor)]
        public ActionResult Create()
        {
            var model = new PreguntaRespuestaABMViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(PreguntaRespuestaABMViewModel model)
        {
            var errores = new List<string>();
            if (model.Respuestas.Count != CANT_RESPUESTAS) {
                errores.Add(ERROR_CANT_CORRECTAS);
            }
            if (!verificarCantCorrectas(model.Respuestas)) {
                errores.Add(ERROR_CANT_CORRECTAS);
            }
            if (Guid.Empty == model.CategoriaSeleccionada)
            {
                errores.Add(ERROR_CATEGORIA_VACIA);
            }
            if (model.Nombre == null)
            {
                errores.Add(ERROR_NOMBRE_VACIO);
            }


            if (!errores.Any())
            {
                var preguntaRespuesta = new Pregunta(model, db);
                new Repositorio<Pregunta>(db).Crear(preguntaRespuesta);
             
                return Json(new { Result = "OK", Error = "" }, JsonRequestBehavior.AllowGet);
            }
            else {
                return Json(new { Result = "", Error = errores }, JsonRequestBehavior.AllowGet);
            }
        }

        private bool verificarCantCorrectas(List<RespuestaViewModel> respuestas)
        {
            int cont = 0;
            for (var i = 0; i < respuestas.Count; i++)
            {
                if (respuestas[i].EsCorrecta == true)
                {
                    cont = cont + 1;
                }
            }
            return respuestas.Count > 0 && cont == CANT_CORRECTAS;
        }

        public ActionResult GetPregunta(Guid id)
        {
            var pregunta = new Repositorio<Pregunta>(db).Traer(id);
            var model = new PreguntaRespuestaABMViewModel(pregunta);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetData()
        {
            var nombre = Request.QueryString["Nombre"];
            var categoriaSeleccionada = Request.QueryString["CategoriaSeleccionada"];
            var start = Convert.ToInt32(Request.QueryString["start"]);
            var cantidad = Convert.ToInt32(Request.QueryString["length"]);
            var orderColumn = Request.QueryString["order[0][column]"];
            var orderDir = Request.QueryString["order[0][dir]"];

            var query = new Repositorio<Pregunta>(db).TraerTodos();
            var cantidadTotal = query.Count();

            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(p => p.Nombre.Contains(nombre)).OrderBy(p => p.Nombre);
            }
            if (!string.IsNullOrEmpty(categoriaSeleccionada))
            {
                Guid categoriaGuid = new Guid(categoriaSeleccionada);
                query = query.Where(p => p.Categoria.Id == categoriaGuid);
            }

            var cantidadFiltradas = query.Count();

            if (orderDir == "asc")
            {
                if (orderColumn == "1")
                    query = query.OrderBy(r => r.Categoria);
                else
                    query = query.OrderBy(r => r.Nombre);
            }
            else
            {
                if (orderColumn == "0")
                    query = query.OrderByDescending(r => r.Categoria);
                else
                    query = query.OrderByDescending(r => r.Nombre);
            }

            var data = query.Skip(start).Take(cantidad).ToList().Select(p => new
            {
                Id = p.Id,
                Nombre = p.Nombre,
                CategoriaSeleccionada = p.Categoria.Nombre,
                Respuestas = p.Respuestas.Select(respuesta => new {
                    Id = respuesta.Id,
                    Nombre = respuesta.Nombre,
                    EsCorrecta = respuesta.EsCorrecta
                }).ToList()
            }).ToList();

            var result = new { data = data, recordsTotal = cantidadTotal, recordsFiltered = cantidadTotal };

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Delete  (Guid id)
        {
            var errores = new List<string>();
            var repo = new Repositorio<Pregunta>(db);
            repo.Eliminar(repo.Traer(id));
            return Json(new { Result = "OK", Error = "" }, JsonRequestBehavior.AllowGet);
            
        }

        public ActionResult Edit(Guid id)
        {
            return View(id);
        }

        [HttpPost]
        public ActionResult Edit(PreguntaRespuestaABMViewModel model)
        {
            var errores = new List<string>();
            if (model.Respuestas.Count != CANT_RESPUESTAS)
            {
                errores.Add(ERROR_CANT_CORRECTAS);
            }
            if (!verificarCantCorrectas(model.Respuestas))
            {
                errores.Add(ERROR_CANT_CORRECTAS);
            }
            if (!errores.Any())
            {
                var repo = new Repositorio<Pregunta>(db);
                var pregunta = repo.Traer(model.Id);
                pregunta.ModificarJs(model, db);
                repo.Modificar(pregunta);
                return Json(new { Result = "OK", Error = "" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = "", Error = errores }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}