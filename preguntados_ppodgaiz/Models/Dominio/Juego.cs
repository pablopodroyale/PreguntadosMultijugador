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

        public Juego()
        {
            Id = new Guid();
        }

        public void setPreguntas(List<Pregunta> preguntas)
        {
            Preguntas = preguntas;
        }

        public void setCategoria(Categoria categoria)
        {
            Categoria = categoria;
        }

        public void setPlayer(Player player)
        {
            Players.Add(player);
        }
    }
}