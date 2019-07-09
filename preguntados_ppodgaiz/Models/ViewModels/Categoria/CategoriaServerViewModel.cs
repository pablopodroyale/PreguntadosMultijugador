using preguntados_ppodgaiz.Models.ViewModels.General;
using preguntados_ppodgaiz.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.ViewModels.Categoria
{
    public class CategoriaServerViewModel
    {
        public List<SelectBoxItemViewModel> CategoriasDisponibles { get; set; }
        public Guid CategoriaSeleccionada { get; set; }
        public string Nombre { get; set; }
        public Guid Id { get; set; }

        public CategoriaServerViewModel()
        {
            LlenarListas();
        }

        public CategoriaServerViewModel(Dominio.Categoria model):this()
        {
            Id = model.Id;
            Nombre = model.Nombre;
        }


        public void LlenarListas()
        {
            var db = new ApplicationDbContext();
            CategoriasDisponibles = new Repositorio<Dominio.Categoria>(db).TraerTodos().Select(s => new SelectBoxItemViewModel()
            {
                Id = s.Id,
                Nombre = s.Nombre
            }).ToList();
        }
    }
}