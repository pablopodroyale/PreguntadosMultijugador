using preguntados_ppodgaiz.Models.Dominio;
using preguntados_ppodgaiz.Models.ViewModels.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.ViewModels.PreguntaRespuesta
{
    public class PreguntaJuegoViewModel : EntidadViewModel
    {
        public Guid CategoriaId { get; set; }
       
        public List<RespuestaJuegoViewModel> Respuestas { get; set; }
        public Guid Usuarioid { get; set; }

        public PreguntaJuegoViewModel()
        {

        }

        public PreguntaJuegoViewModel(Pregunta pregunta, Guid usuarioId)
        {
            this.Respuestas = new List<RespuestaJuegoViewModel>();
            this.Id = pregunta.Id;
            this.Nombre = pregunta.Nombre;
            this.Respuestas = pregunta.Respuestas.Select(p => new RespuestaJuegoViewModel
            {
                Id = p.Id,
                Nombre = p.Nombre
            }).ToList();
            this.CategoriaId = pregunta.Categoria.Id;
            this.Usuarioid = usuarioId;
        }
    }
}