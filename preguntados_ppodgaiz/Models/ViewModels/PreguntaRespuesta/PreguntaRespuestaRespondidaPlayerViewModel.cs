using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.ViewModels.PreguntaRespuesta
{
    public class PreguntaRespuestaRespondidaPlayerViewModel
    {
        public Guid IdPregunta { get; set; }
        public Guid IdRespuesta { get; set; }
        public Guid IdRespuestaSeleccionada { get; set; }
    }
}