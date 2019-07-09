using preguntados_ppodgaiz.Models.ViewModels.General;
using preguntados_ppodgaiz.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.ViewModels.PreguntaRespuesta
{
    public class PreguntaRespuestaABMViewModel : EntidadViewModel
    {
        public List<SelectBoxItemViewModel> CategoriasDisponibles { get; set; }
        public Guid CategoriaSeleccionada { get; set; }
        public List<RespuestaViewModel> Respuestas { get; set;}
        public string Pregunta { get; set; }
        

        public PreguntaRespuestaABMViewModel()
        {
            LlenarListas();
        }

        public PreguntaRespuestaABMViewModel(Dominio.Pregunta model):this()
        {
            Id = model.Id;
            Nombre = model.Nombre;
            CategoriaSeleccionada = model.Categoria.Id;
            Respuestas = model.Respuestas.Select(r => new RespuestaViewModel{
                Nombre = r.Nombre,
                EsCorrecta = r.EsCorrecta,
                Id = r.Id
            }).ToList();
        }

        private void LlenarListas()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            CategoriasDisponibles = new Repositorio<Dominio.Categoria>(db).TraerTodos().Select(s => new SelectBoxItemViewModel()
            {
                Id = s.Id,
                Nombre = s.Nombre
            }).ToList();
        }
       
    }
}