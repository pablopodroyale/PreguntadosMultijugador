
using preguntados_ppodgaiz.Models.ViewModels.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.ViewModels.Juego
{
    public class ResultadoJuegoViewModel:EntidadViewModel
    {
        public Guid CategoriaId { get; set; }

        public List<RespuestaResultadoViewModel> Respuestas { get; set; }
        public Guid Usuarioid { get; set; }
        public Guid RespuestaContestada { get; set; }
        public Guid RespuestaCorrecta { get; set; }
        //public string Resultado { get; set; }
        
        public ResultadoJuegoViewModel()
        {
            this.Respuestas = new List<RespuestaResultadoViewModel>();
        }
    }
}