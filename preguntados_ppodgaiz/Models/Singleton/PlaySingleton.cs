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

        private PlaySingleton()
        {
            players = new List<Player>();
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
            if (!players.Any(p => p.Nombre == player.Nombre))
            {
                players.Add(player);
            }
        }

        public ICollection<Player> GetPlayers()
        {
            return players;
        }

    }
}