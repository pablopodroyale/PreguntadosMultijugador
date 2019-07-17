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

        public Player AgregarJugador(Player player)
        {

            if (!players.Any(p => p.Usuario.Nombre == player.Usuario.Nombre))
            {
                players.Add(player);
                return null;
            }
            return players.Where(p => p.Usuario.Nombre == player.Usuario.Nombre).FirstOrDefault();
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

        public Guid GetIdJuego(Guid idPlayer)
        {

            var juego = juegos.Where(j => j.Players.Any(p => p.Usuario.Id == idPlayer)).FirstOrDefault();
            return juego != null ? juego.Id : Guid.Empty;

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

        public Guid EmpezarPartida(Guid idPlayer)
        {
            var owner = juegos.SelectMany(j => j.Players.Where(p => p.Usuario.Id == idPlayer)).FirstOrDefault();
            Juego juego = null;
            if (owner != null && owner.Owner == true)
            {
                juego = juegos.Where(j => j.Players.Any(p => p.Usuario.Id == idPlayer)).FirstOrDefault();
                juego.EnJuego = true;
                juego.setEnJuego();
            }
            return juego.Id;

        }

        public void ResetearSingleton(string idJuego)
        {
            var id = new Guid(idJuego);
            var juego = juegos.Where(j => j.Id == id).FirstOrDefault();
            if (juego != null)
            {
                juego.Players.ToList().ForEach(p => {
                    p.EnCola = false;
                    p.Jugando = false;
                    p.Owner = false;
                    p.PreguntaRespuestas = new List<ViewModels.PreguntaRespuesta.PreguntaRespuestaDto>();
                    p.NroPreguntaRespondida = 0;
                    
                });

                juegos.Remove(juego);
                juego = null;
            }
            
        }

        public Juego GetJuego(Guid idJuego)
        {
            return juegos.Where(j => j.Id == idJuego).FirstOrDefault();
        }

        public Player GetPlayer(Guid idJugador)
        {
           return players.Where(p => p.Usuario.Id == idJugador).FirstOrDefault();
        }
    }
}