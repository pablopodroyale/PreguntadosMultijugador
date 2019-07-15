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

        //public ResultadoMultijugadorViewModel GetResultadosPorJugador()
        //{
        //    ResultadoMultijugadorViewModel model = new ResultadoMultijugadorViewModel();
        //    model.Categoria = Categoria.Nombre;
        //    ResultadoPorJugadorViewModel resultadoPorJugadorViewModel = new ResultadoPorJugadorViewModel();
        //    foreach (var item in Players)
        //    {
        //        item.PreguntaRespuestas.Select(p => p.Respuestas.Select(r => ResultadoPorJugadorViewModel(){

        //        }));
        //    }

        //    return model;
        //}

        public bool VerificarFinal()
        {
            int cantJugadores = Players.Count();
            int cantidadTerminaron = 0;
            foreach (var item in Players)
            {
                if (item.NroPreguntaRespondida == (CantidadPreguntas - 1))
                {
                    cantidadTerminaron++;
                }
            }

            return cantidadTerminaron == cantJugadores;
        }

        public  ResultadoMultijugadorViewModel GetResultados()
        {
            ResultadoMultijugadorViewModel model = new ResultadoMultijugadorViewModel();
            model.Categoria = Categoria.Nombre;
            var resultadosPorJugador = Players.SelectMany(p => p.PreguntaRespuestas.Select(r =>  new ResultadoPorJugadorViewModel() {
                IdPlayer = r.PlayerId,
                Respuestas = r.Respuestas.Select(t => new RespuestaResultadoViewModel() {
                    IdRespuesta = t.IdRespuesta,
                    RespuestaSeleccionada = t.RespuestaSeleccionada,
                    TextoRespuesta = t.TextoRespuesta
                }).ToList()
            })).ToList();
            //trear las preguntas y la respues contestada

            return model;
        }

        public Guid getRespuestaCorrecta(Guid preguntaId)
        {
            var correcta = Preguntas.SelectMany(p => p.Respuestas.Where(r => r.EsCorrecta)).FirstOrDefault().Id;
            return correcta;
        }
    }
}