﻿using System;
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


        public void setUsuario(Usuario usuario)
        {
            Usuario = usuario;
        }
    }
}