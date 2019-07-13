using preguntados_ppodgaiz.Models.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.Singleton
{
    public class PlaySingleton
    {
        private List<Player> players;
        private static PlaySingleton instance;
        private List<Juego> juegos;


        private PlaySingleton()
        {
            players = new List<Player>();
            juegos = new List<Juego>();

        }

        public static PlaySingleton GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PlaySingleton();
                }

                return instance;
            }
        }

        public void AgregarJugador(Player player)
        {
            if (!players.Any(p => p.Usuario.Nombre == player.Usuario.Nombre))
            {
                players.Add(player);
            }
        }

        public ICollection<Player> GetPlayers()
        {
            return players;
        }

        public void SetToQueue(Guid idPlayer, Guid idOwner)
        {
            var juego = juegos.Where(j => j.Players.Any(p => p.Usuario.Id == idOwner)).FirstOrDefault();
            var player = players.Where(p => p.Usuario.Id == idPlayer).FirstOrDefault();
            if (juego != null && player != null)
            {
                player.EnCola = true;
                juego.setPlayer(player);
            }
        }

        public void SetOwner(Guid id)
        {
            var player = players.Where(p => p.Usuario.Id == id).FirstOrDefault();
            player.Owner = true;
        }

        public string VerificarEstado(Guid idPlayer)
        {
            string estado = null;
            var player =  players.Where(p => p.Usuario.Id == idPlayer).FirstOrDefault();
            if (player != null && player.EnCola)
            {
                estado = "encola";
            }
            else if (player != null && player.Jugando)
            {
                estado = "jugando";
            }
            else {
                estado = "ensala";
            }
            return estado;


        }

        public void CrearJuego(Categoria categoria, Guid idPlayer, List<Pregunta> preguntas)
        {
            var player = players.Where(p => p.Usuario.Id == idPlayer).FirstOrDefault();
            var juego = new Juego();
            player.Owner = true;
            juego.setPlayer(player);
            juego.setCategoria(categoria);
            juego.setPreguntas(preguntas);
            juegos.Add(juego);
        }

        public void EmpezarPartida(Guid idPlayer)
        {
            var owner = juegos.SelectMany(j => j.Players.Where(p => p.Usuario.Id == idPlayer)).FirstOrDefault();
            Juego juego = null;
            if (owner != null && owner.Owner == true)
            {
                juego = juegos.Where(j => j.Players.Any(p => p.Usuario.Id == idPlayer)).FirstOrDefault();
                juego.EnJuego = true;
                juego.setEnJuego();
            }


        }
    }
}