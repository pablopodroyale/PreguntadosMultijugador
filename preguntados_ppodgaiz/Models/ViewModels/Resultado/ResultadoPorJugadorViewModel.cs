using preguntados_ppodgaiz.Models.ViewModels.PreguntaRespuesta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.ViewModels.Resultado
{
    public class ResultadoPorJugadorViewModel
    {
        public Guid IdPlayer { get; set; }
        public string NickPlayer { get; set; }
        public List<PreguntaRespuestaMultijugadorResultadoViewModel> Preguntas { get; set; }
    }
}