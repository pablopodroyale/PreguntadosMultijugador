using preguntados_ppodgaiz.Models.ViewModels.Juego;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.ViewModels.Resultado
{
    public class ResultadoMultijugadorViewModel
    {
        public string Categoria { get; set; }
        public ResultadoPorJugadorViewModel ResultadosPorJugador { get; set; }

        public ResultadoMultijugadorViewModel()
        {
        }
    }
}