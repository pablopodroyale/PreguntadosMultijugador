using preguntados_ppodgaiz.Models.ViewModels.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.ViewModels.Juego
{
    public class ResultadoWraperJuegoViewModel:EntidadViewModel
    {
        public List<ResultadoJuegoViewModel>  Resultado { get; set; }
        public string Score { get; set; }
        public ResultadoWraperJuegoViewModel()
        {
            this.Resultado = new List<ResultadoJuegoViewModel>();
        }
    }
}