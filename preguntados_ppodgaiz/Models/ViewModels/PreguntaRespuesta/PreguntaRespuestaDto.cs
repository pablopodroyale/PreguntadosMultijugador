using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.ViewModels.PreguntaRespuesta
{
    public class PreguntaRespuestaDto
    {
        public Guid PlayerId { get; set; }
        public int NroPreguntarespondida { get; set; } = 0;
        public List<RespuestaDto> Respuestas { get; set; }
        public PreguntaRespuestaDto()
        {
            Respuestas = new List<RespuestaDto>();
        }
    }
}