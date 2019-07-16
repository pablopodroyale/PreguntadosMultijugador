using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.ViewModels.Resultado
{
    public class PreguntaRespuestaMultijugadorResultadoViewModel
    {
      
        public Guid IdPregunta { get; set; }
        public string TextoPregunta { get; set; }

        public Guid RespuestaSeleccionada { get; set; }
        public Guid RespuestaCorrecta { get; set; }
        public List<RespuestaResultadoMultijugadorViewModel> Respuestas { get; set; }
    }
}