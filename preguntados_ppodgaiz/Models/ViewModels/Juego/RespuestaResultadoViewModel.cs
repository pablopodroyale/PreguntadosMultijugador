using preguntados_ppodgaiz.Models.ViewModels.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.ViewModels.Juego
{
    public class RespuestaResultadoViewModel:EntidadViewModel
    {
        public bool EsCorrecta { get; set; }
        public bool Contestada { get; set; }
    }
}