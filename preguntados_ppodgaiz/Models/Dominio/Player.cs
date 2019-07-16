using preguntados_ppodgaiz.Models.ViewModels.PreguntaRespuesta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.Dominio
{
    public class Player
    {
        public bool EnCola { get; set; } = false;
        public bool Jugando { get; set; } = false;
        public bool Owner { get; set; }
        public Usuario Usuario { get; set; }
        public int NroPreguntaRespondida { get; set; } = 0;
        public List<PreguntaRespuestaDto> PreguntaRespuestas { get; set; }

        public Player()
        {

            PreguntaRespuestas = new List<PreguntaRespuestaDto>();
        }
        public void setUsuario(Usuario usuario)
        {

            Usuario = usuario;
        }

        public PreguntaRespuestaDto SetRespuestaSeleccionada(Guid idPregunta ,Guid idRespuesta, Guid respuestaSeleccionadaId, Guid idRespuestaCorrecta, string TextoPregunta)
        {
            PreguntaRespuestaDto preguntaRespuestaDto = new PreguntaRespuestaDto() {
                RespuestaSeleccionada =respuestaSeleccionadaId,
                RespuestaCorrecta = idRespuestaCorrecta,
                IdPregunta = idPregunta,
                TextoPregunta = TextoPregunta
            };
            PreguntaRespuestas.Add(preguntaRespuestaDto);

            return preguntaRespuestaDto;
        }
    }
}