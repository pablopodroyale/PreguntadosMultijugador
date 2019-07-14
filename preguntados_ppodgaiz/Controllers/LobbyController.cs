﻿using preguntados_ppodgaiz.Models;
using preguntados_ppodgaiz.Models.Dominio;
using preguntados_ppodgaiz.Models.Enums;
using preguntados_ppodgaiz.Models.Singleton;
using preguntados_ppodgaiz.Models.ViewModels.Player;
using preguntados_ppodgaiz.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace preguntados_ppodgaiz.Controllers
{
    [Authorize]
    public class LobbyController : Controller
    {
        private bool empezar;
        private ApplicationDbContext db;
        private List<Juego> juegos;

        public LobbyController()
        {
            db = new ApplicationDbContext();
            empezar = false;
            juegos = new List<Juego>();
        }
        public ActionResult Ingresar()
        {
            PlayerViewModel model = new PlayerViewModel();
            return View(model);
        }

        //[HttpPost]
        //public ActionResult Ingresar(PlayerViewModel model)
        //{
        //    Player player = new Player() {
        //        Nombre = model.Nombre,
        //        Id = new Guid(),

        //    };
        //    PlaySingleton.GetInstance.AgregarJugador(player);
        //    return RedirectToAction("Ingresar");
        //}
        public ActionResult VerificarEstado(Guid idPlayer)
        {
            Guid idJuego = PlaySingleton.GetInstance.GetIdJuego(idPlayer);
            string estado = PlaySingleton.GetInstance.VerificarEstado(idPlayer);
            return Json(new { estado = estado, idJuego = idJuego }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmpezarPartida(Guid idPlayer)
        {
            var idJuego = PlaySingleton.GetInstance.EmpezarPartida(idPlayer);
            return Json(idJuego,JsonRequestBehavior.AllowGet);
        }


        [Authorize(Roles = RoleConst.Suscriptor)]
        public ActionResult Lobby(Usuario usuario)
        {
            Player player = new Player();
            player.setUsuario(usuario);
            PlaySingleton.GetInstance.AgregarJugador(player);
            return View(player);
        }

        public ActionResult GetPlayers(Guid idPlayer)
        {
            var players = PlaySingleton.GetInstance.GetPlayers().Where(u => u.Usuario.Id != idPlayer && u.EnCola == false && u.Jugando == false && u.Owner == false).ToList();
            return Json(players, JsonRequestBehavior.AllowGet);
        }

        //Agregar un jugador a la espera
        public ActionResult SetToQueue(Guid idPlayer, Guid idOwner)
        {
            //Guid id = new Guid(idPlayer);
            PlaySingleton.GetInstance.SetToQueue(idPlayer, idOwner);
            return Json("ok",JsonRequestBehavior.AllowGet);
        }

        public ActionResult CrearJuego(Guid idPlayer,string nombreCategoria)
        {
            var repoPregunta = new Repositorio<Pregunta>();
            var repoUsuario = new Repositorio<Usuario>(db);
            var repoCategoria = new Repositorio<Categoria>(db);
            var categoria = repoCategoria.TraerTodos().Where(c => c.Nombre == nombreCategoria).FirstOrDefault();
            var preguntas = repoPregunta.TraerTodos().Where(p => p.Categoria.Id == categoria.Id).Take(4).ToList();
            PlaySingleton.GetInstance.CrearJuego(categoria,idPlayer, preguntas);
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
    }
}