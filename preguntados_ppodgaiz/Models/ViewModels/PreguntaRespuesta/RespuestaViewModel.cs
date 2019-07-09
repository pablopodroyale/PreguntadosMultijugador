using preguntados_ppodgaiz.Models.ViewModels.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.ViewModels.PreguntaRespuesta
{
    public class RespuestaViewModel:EntidadViewModel
    {
        public bool EsCorrecta { get; set; }
        public bool Eliminada { get; set; }
        public bool Editada { get; set; }
        public bool Nueva { get; set; }
    }
}