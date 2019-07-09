using preguntados_ppodgaiz.Models.ViewModels.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.ViewModels.Filtro
{
    public class SearcherViewModel:EntidadViewModel
    {
        public Categoria.CategoriaServerViewModel Categorias { get; set; }

        public SearcherViewModel()
        {
            this.Categorias = new Categoria.CategoriaServerViewModel();
        }
    }
}