﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.ViewModels.PreguntaRespuesta
{
    public class RespuestaDto
    {
        //public Guid IdPregunta { get; set; }
        //public string TextoPregunta { get; set; }
        public Guid IdRespuesta { get; set; }
        public string  TextoRespuesta { get; set; }
        public bool RespuestaCorrecta { get; set; }
        //public Guid RespuestaCorrectaId { get; set; }
        //public Guid RespuestaSeleccionada { get; set; }
        //public List<RespuestaJuegoViewModel> Respuestas { get; set; }
    }
}