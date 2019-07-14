using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.ViewModels.PreguntaRespuesta
{
    public class PreguntaRespondidaMultijugadorViewModel
    {
        public Guid PreguntaId { get; set; }
        public Guid RespuestaSeleccionadaId { get; set; }
        public Guid PlayerId { get; set; }
        public Guid JuegoId { get; set; }

    }
}