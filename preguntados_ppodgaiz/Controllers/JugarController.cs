using Microsoft.AspNet.Identity;
using preguntados_ppodgaiz.Helpers;
using preguntados_ppodgaiz.Models;
using preguntados_ppodgaiz.Models.Dominio;
using preguntados_ppodgaiz.Models.Enums;
using preguntados_ppodgaiz.Models.ViewModels.Categoria;
using preguntados_ppodgaiz.Models.ViewModels.Juego;
using preguntados_ppodgaiz.Models.ViewModels.PreguntaRespuesta;
using preguntados_ppodgaiz.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace preguntados_ppodgaiz.Controllers
{
    [Authorize(Roles = RoleConst.Suscriptor)]
    public class JugarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Jugar
        public ActionResult Index()
        {
            var repoCategorias = new Repositorio<Categoria>();
            var repoPreguntas = new Repositorio<Pregunta>();
           // var categorias = repoCategorias.TraerTodos().Where(c => !c.Eliminado);
            var categorias = repoCategorias.TraerTodos().Where(c => c.Preguntas.Count(e => !e.Eliminado) >= c.CantidadPreguntasAResponder);
            List<CategoriaGrillaViewModel> model = new List<CategoriaGrillaViewModel>();
            foreach (var item in categorias)
            {
                model.Add(new CategoriaGrillaViewModel(item));
            }
            return View(model);
        }

        public ActionResult Jugar(string id)
        {
            var idGuid = new Guid(id);
            var userId = User.Identity.GetUserId();
            var repoUsuario = new Repositorio<Usuario>(db);
            var repoPregunta = new Repositorio<Pregunta>(db);
            var repoCategoria = new Repositorio<Categoria>(db);
            var repoPreguntasRespondidas = new Repositorio<PreguntasRespondidas>(db);
            Categoria categoriaSeleccionada = null;
            //Traer la categoria
            if (id != null)
            {
                categoriaSeleccionada = repoCategoria.TraerTodos().Where(c => c.Id == idGuid).FirstOrDefault();
            }
           
            var preguntas = repoPregunta.TraerTodos().Where(p => p.Categoria.Id == categoriaSeleccionada.Id).ToList();
            var preguntasRespondidas = repoPreguntasRespondidas.TraerTodos().Where(p => p.Usuario.ApplicationUser.Id == userId && p.Categoria.Id == categoriaSeleccionada.Id).ToList();
            
            //chequear cuantas tiene la categoria
            int cantidadAContestar = preguntas.Where(c => c.Categoria.Id == categoriaSeleccionada.Id).FirstOrDefault().Categoria.CantidadPreguntasAResponder;
            //cuantas contesto
            int cantidadContestadas = preguntasRespondidas != null ? preguntasRespondidas.Count() : 0;
            //que tiene el juego menos las que contesto
            if (cantidadContestadas < cantidadAContestar)
            {
                var preguntasId = preguntas.Select(p => p.Id);
                var preguntasContestadasId = preguntasRespondidas != null ? preguntasRespondidas.Select(p => p.Pregunta.Id) : new List<Guid>();
                //traer una pregunta except entre pregutnas y contestadas
                var preguntasFiltradas = preguntasId.Except(preguntasContestadasId).ToList();
                var preguntaId = preguntasFiltradas.ElementAt(new Random().Next(0,preguntasFiltradas.Count() -1));
                Pregunta preguntaRandom = preguntas.Where(p => p.Id == preguntaId).FirstOrDefault();
                var usuarioId = repoUsuario.TraerTodos().Where(p => p.ApplicationUser.Id == userId).FirstOrDefault().Id;
                PreguntaJuegoViewModel model = new PreguntaJuegoViewModel(preguntaRandom, usuarioId);
              
                return View("Juego", model);
            }
            int cantidadCorrectas = repoPreguntasRespondidas.TraerTodos().Count(p => p.Respuesta.EsCorrecta);
            var resultados = repoPreguntasRespondidas.TraerTodos().Where(p => p.Usuario.ApplicationUser.Id == userId && p.Categoria.Id == categoriaSeleccionada.Id).Select(r => new ResultadoJuegoViewModel
            {
                Id = r.Id,
                Nombre = r.Pregunta.Nombre,
                CategoriaId = r.Categoria.Id,
                Usuarioid = r.Usuario.Id,
                RespuestaContestada = r.Respuesta.Id,
                RespuestaCorrecta = r.Pregunta.Respuestas.Where(q => q.EsCorrecta).FirstOrDefault().Id,
                //Resultado = cantidadCorrectas + "/" + cantidadAContestar,
                Respuestas = r.Pregunta.Respuestas.Select(p => new RespuestaResultadoViewModel
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    EsCorrecta = p.EsCorrecta
                }).ToList()
            }).ToList();
            ResultadoWraperJuegoViewModel resultadoWraper = new ResultadoWraperJuegoViewModel();
            resultadoWraper.Resultado = resultados;
            resultadoWraper.Score = cantidadCorrectas + "/" +  cantidadAContestar;
            return View("Resultados", resultadoWraper);
        }

        public ActionResult JugarMultijugador(Guid idPlayer)
        {
            return View();
        }


        [HttpPost]
        public ActionResult Save(PreguntaRespondidaViewModel model)
        {
            var repoPreguntaRespondida = new Repositorio<PreguntasRespondidas>(db);
            var repoPregunta = new Repositorio<Pregunta>(db);
            PreguntasRespondidas preguntaRespondida = new PreguntasRespondidas(model, db);
            repoPreguntaRespondida.Crear(preguntaRespondida);
            var respuestaCorrectaId = repoPregunta.TraerTodos().Where(p => p.Id == model.Id).FirstOrDefault().Respuestas.Where(r => r.EsCorrecta).FirstOrDefault().Id;
            LoggerEventos.LogearEvento("Pregunta respondida: " + model.Nombre, User.Identity.GetUserId(), model.Id, AccionesLogEnum.RESPONDER_PREGUNTA, EntidadLogEnum.PREGUNTA_RESPONDIDA);
            return Json(respuestaCorrectaId, JsonRequestBehavior.AllowGet);
        }
    }
}