using preguntados_ppodgaiz.Models.ViewModels.Resultado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.Dominio
{
    public class Juego:Entidad
    {
        public Categoria Categoria { get; set; }
        public List<Player> Players { get; set; }
        public List<Pregunta> Preguntas { get; set; }
        public Guid Id { get; set; }
        public bool EnJuego { get; set; } = false;
        public int CantidadPreguntas { get; set; }
        public Juego()
        {
            Players = new List<Player>();
            Id = Guid.NewGuid();
        }

        public void setPreguntas(List<Pregunta> preguntas)
        {
            Preguntas = preguntas;
            CantidadPreguntas = preguntas.Count();
        }

        public void setCategoria(Categoria categoria)
        {
            Categoria = categoria;
        }

        public void setPlayer(Player player)
        {
            Players.Add(player);
        }

        public void setEnJuego()
        {
            foreach (var item in Players)
            {
                item.EnCola = false;
                item.Jugando = true;
            }
        }

        public Pregunta GetPreguntaJuego(int nroPreguntaRespondida)
        {
            return Preguntas.OrderBy(p => p.Id).ToList()[nroPreguntaRespondida];
        }

        public Player GetPlayer(Guid idPlayer)
        {
            return Players.Where(p => p.Usuario.Id == idPlayer).FirstOrDefault();
        }

        public ResultadoMultijugadorViewModel GetResultados()
        {
            ResultadoMultijugadorViewModel model = new ResultadoMultijugadorViewModel();
            model.Categoria = Categoria.Nombre;
            foreach (var item in Players)
            {
                
            }
        }
    }
}