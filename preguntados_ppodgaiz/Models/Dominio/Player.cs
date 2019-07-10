using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.Dominio
{
    public class Player:Entidad
    {
        public bool EnCola { get; set; } = false;
        public bool Jugando { get; set; } = false;
        public bool Owner { get; set; }
    }
}