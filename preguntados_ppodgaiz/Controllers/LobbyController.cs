using preguntados_ppodgaiz.Models;
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

        public LobbyController()
        {
            db = new ApplicationDbContext();
            empezar = false;
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
        public ActionResult VerificarComienzo()
        {
            if (empezar == true)
            {
                return Json("ok",JsonRequestBehavior.AllowGet);
            }
            return Json("wait", JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmpezarPartida(string categoria)
        {
            var repoCategoria = new Repositorio<Categoria>(db);
            var id = repoCategoria.TraerTodos().Where(c => c.Nombre == categoria).FirstOrDefault().Id;
            empezar = true;
            return Json(id,JsonRequestBehavior.AllowGet);
        }


        [Authorize(Roles = RoleConst.Suscriptor)]
        public ActionResult Lobby(Usuario usuario)
        {
            Player player = new Player()
            {
                Nombre = usuario.Nombre,
                Id = usuario.Id,
            };
            PlaySingleton.GetInstance.AgregarJugador(player);
            //var model = PlaySingleton.GetInstance.GetPlayers().Where(u => u.Nombre != usuario.EMail && u.Nombre != User.Identity.Name && u.EnCola == false && u.Jugando == false && u.Owner == false).ToList();
            return View(player);
        }

        public ActionResult GetPlayers()
        {
            var players = PlaySingleton.GetInstance.GetPlayers().Where(u => u.Nombre != User.Identity.Name && u.EnCola == false && u.Jugando == false && u.Owner == false).Select(p => new Player() {
                Nombre = p.Nombre,
                Id = p.Id
            }).ToList();
            return Json(players, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SetToQueue(Guid id)
        {
            PlaySingleton.GetInstance.SetToQueue(id);
            return Json("ok",JsonRequestBehavior.AllowGet);
        }

        public ActionResult SetOwner(Guid id)
        {
            PlaySingleton.GetInstance.SetOwner(id);
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
    }
}