using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.ViewModels.PreguntaRespuesta
{
    public class PreguntaRespuestaDto
    {
        public Guid PlayerId { get; set; }
        public int NroPreguntarespondida { get; set; } = 0;
        public List<RespuestaDto> Respuestas { get; set; }
        public Guid IdPregunta { get; set; }
        public string TextoPregunta { get; set; }
        public Guid RespuestaSeleccionada { get; set; }
        public Guid RespuestaCorrecta { get; set; }
        public PreguntaRespuestaDto()
        {
            Respuestas = new List<RespuestaDto>();
        }
    }
}