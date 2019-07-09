using preguntados_ppodgaiz.Models.ViewModels.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.ViewModels.Player
{
    public class PlayerViewModel:EntidadViewModel
    {
        public bool Enviado { get; set; } = false;
    }
}