using preguntados_ppodgaiz.Models.ViewModels.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using preguntados_ppodgaiz.Models.Dominio;
using System.ComponentModel.DataAnnotations;

namespace preguntados_ppodgaiz.Models.ViewModels.Categoria
{
    public class CategoriaAbmViewModel:EntidadViewModel
    {
        [Required]
        public int CantidadPreguntasAResponder { get; set; }
        [Required]
        public string Color { get; set; }

        public CategoriaAbmViewModel(Dominio.Categoria model)
        {
            Id = model.Id;
            Nombre = model.Nombre;
            this.CantidadPreguntasAResponder = model.CantidadPreguntasAResponder;
            this.Color = model.Color;
        }
        public CategoriaAbmViewModel()
        {

        }

    }
}