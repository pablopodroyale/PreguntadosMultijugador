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

        public void SetToQueue(Guid idPlayer)
        {
            var player = players.Where(p => p.Usuario.Id == idPlayer).FirstOrDefault();
            player.EnCola = true;
        }

        public void SetOwner(Guid id)
        {
            var player = players.Where(p => p.Usuario.Id == id).FirstOrDefault();
            player.Owner = true;
        }

        public bool VerificarEnEspera(Guid idPlayer)
        {
            return players.Where(p => p.Usuario.Id == idPlayer).FirstOrDefault().EnCola;
        }

        public void CrearJuego(Categoria categoria,Guid idPlayer, List<Pregunta> preguntas)
        {
            var player = players.Where(p => p.Usuario.Id == idPlayer).FirstOrDefault();
            var juego = new Juego();
            player.Owner = true;
            juego.setPlayer(player);
            juego.setCategoria(categoria);
            juego.setPreguntas(preguntas);
            juegos.Add(juego);
        }
    }
}