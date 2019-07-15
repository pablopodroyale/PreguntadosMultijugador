using preguntados_ppodgaiz.Models.ViewModels.PreguntaRespuesta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.Dominio
{
    public class Player
    {
        public bool EnCola { get; set; } = false;
        public bool Jugando { get; set; } = false;
        public bool Owner { get; set; }
        public Usuario Usuario { get; set; }
        public int NroPreguntaRespondida { get; set; }
        public List<PreguntaRespuestaDto> PreguntaRespuestas { get; set; }


        public void setUsuario(Usuario usuario)
        {
            Usuario = usuario;
        }

        public void SetRespuestaSeleccionada(Guid idPregunta ,Guid idRespuesta, Guid respuestaSeleccionadaId)
        {
            var respuesta = new PreguntaRespuestaRespondidaPlayerViewModel() {
                IdPregunta = idPregunta,
                IdRespuesta =  idRespuesta,
                IdRespuestaSeleccionada = respuestaSeleccionadaId
            };
           // PreguntaRespuestas.Add(respuesta);


        }
    }
}