using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using preguntados_ppodgaiz.Models.ViewModels.Categoria;
using preguntados_ppodgaiz.Repositorio;

namespace preguntados_ppodgaiz.Models.Dominio
{
    public class Categoria: Entidad
    {
        private const int CANT_PREGUNTAS = 4;
        public string Color { get; set; } 
        public int CantidadPreguntasAResponder { get; set; }
        public virtual ICollection<Pregunta> Preguntas { get; set; }
        public Categoria()
        {

        }

        public Categoria(CategoriaAbmViewModel model, ApplicationDbContext db)
        {
            SetValues(model);
        }

        public Categoria(string category, string randomColor, ApplicationDbContext db)
        {
            this.Nombre = category;
            this.Color = "#" +  randomColor;
            this.CantidadPreguntasAResponder = CANT_PREGUNTAS;
        }

        private void SetValues(CategoriaAbmViewModel model)
        {
            this.Nombre = model.Nombre;
            this.CantidadPreguntasAResponder = model.CantidadPreguntasAResponder;
            this.Color = model.Color;
        }

        public void Modificar(CategoriaAbmViewModel model)
        {
            this.Nombre = model.Nombre;
            this.Id = model.Id;
            this.CantidadPreguntasAResponder = model.CantidadPreguntasAResponder;
            this.Color = model.Color;
        }
    }


}