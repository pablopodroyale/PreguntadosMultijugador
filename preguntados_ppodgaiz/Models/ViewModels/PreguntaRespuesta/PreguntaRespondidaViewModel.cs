using preguntados_ppodgaiz.Models.ViewModels.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.ViewModels.PreguntaRespuesta
{
    public class PreguntaRespondidaViewModel:EntidadViewModel
    {
        public Guid CategoriaId { get; set; }
        public Guid RespuestaSeleccionadaId { get; set; }
        public Guid UsuarioId { get; set; }
    }
}