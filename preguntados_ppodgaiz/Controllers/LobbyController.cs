using preguntados_ppodgaiz.Models.Dominio;
using preguntados_ppodgaiz.Models.Enums;
using preguntados_ppodgaiz.Models.Singleton;
using preguntados_ppodgaiz.Models.ViewModels.Player;
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

        public ActionResult Ingresar()
        {
            PlayerViewModel model = new PlayerViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Ingresar(PlayerViewModel model)
        {
            Player player = new Player() {
                Nombre = model.Nombre,
                Id = new Guid(),

            };
            PlaySingleton.GetInstance.AgregarJugador(player);
            return RedirectToAction("Ingresar");
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
            var model = PlaySingleton.GetInstance.GetPlayers().Where(u => u.Nombre != usuario.EMail && u.Nombre != User.Identity.Name).ToList();
            return View(model);
        }

        public ActionResult GetPlayers()
        {
            var players = PlaySingleton.GetInstance.GetPlayers().Where(u => u.Nombre != User.Identity.Name).Select(p => new Player() {
                Nombre = p.Nombre
            }).ToList();
            return Json(players, JsonRequestBehavior.AllowGet);
        }
    }
}