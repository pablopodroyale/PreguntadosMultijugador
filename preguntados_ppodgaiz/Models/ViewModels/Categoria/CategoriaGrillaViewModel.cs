using preguntados_ppodgaiz.Models.ViewModels.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.ViewModels.Categoria
{
    public class CategoriaGrillaViewModel:EntidadViewModel
    {
        public int CantidadPreguntasAResponder { get; set; }
        public string Color { get; set; }

        public CategoriaGrillaViewModel(Models.Dominio.Categoria model)
        {
            Nombre = model.Nombre;
            Id = model.Id;
            Color = model.Color;
            CantidadPreguntasAResponder = model.CantidadPreguntasAResponder;
        }
    }
}