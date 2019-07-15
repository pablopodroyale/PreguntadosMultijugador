using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.ViewModels.Resultado
{
    public class RespuestaResultadoViewModel
    {
        public Guid IdRespuesta { get; set; }
        public string TextoRespuesta { get; set; }
        public Guid RespuestaSeleccionada { get; set; }
    }
}