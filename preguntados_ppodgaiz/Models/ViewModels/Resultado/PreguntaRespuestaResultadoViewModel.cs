using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.ViewModels.Resultado
{
    public class PreguntaRespuestaResultadoViewModel
    {
        public Guid IdPregunta { get; set; }
        public string TextoPregunta{ get; set; }
        public List<RespuestaResultadoViewModel> Respuestas { get; set; }
    }
}