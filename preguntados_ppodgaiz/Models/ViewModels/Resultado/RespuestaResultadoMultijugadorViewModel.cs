using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.ViewModels.Resultado
{
    public class RespuestaResultadoMultijugadorViewModel
    {
        public Guid IdRespuesta { get; set; }
        public string TextoRespuesta { get; set; }
        public bool EsCorrecta { get; set; }
    }
}