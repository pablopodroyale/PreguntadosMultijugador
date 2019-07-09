using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using preguntados_ppodgaiz.Models.ViewModels.PreguntaRespuesta;
using preguntados_ppodgaiz.Repositorio;

namespace preguntados_ppodgaiz.Models.Dominio
{
    public class Pregunta:Entidad
    {

        public virtual Categoria Categoria { get; set; }
        public virtual ICollection<Respuesta> Respuestas { get; set; }

        public Pregunta()
        {
            this.Respuestas = new List<Respuesta>();
        }

        public Pregunta(PreguntaRespuestaABMViewModel model, ApplicationDbContext db)
        {
            Nombre = model.Nombre;
            Categoria = new Repositorio<Categoria>(db).Traer(model.CategoriaSeleccionada);
            Respuestas = model.Respuestas.Select(r => new Respuesta {
                Nombre = r.Nombre,
                EsCorrecta = r.EsCorrecta,
            }).ToList();

        }

        internal void Modificar(PreguntaRespuestaABMViewModel model, ApplicationDbContext db)
        {
            Nombre = model.Nombre;
            if (model.CategoriaSeleccionada != Guid.Empty)
                Categoria = new Repositorio<Categoria>(db).Traer(model.CategoriaSeleccionada);
            else
                Categoria = null;

            var RespuestasAnteriores = Respuestas.Select(a => a.Id).ToList();
            foreach (var respuesta in RespuestasAnteriores)
            {
                var item = Respuestas.First(a => a.Id == respuesta);
                Respuestas.Remove(item);
            }

        }

        public void ModificarJs(PreguntaRespuestaABMViewModel model, ApplicationDbContext db)
        {
            var repoRespuesta = new Repositorio<Respuesta>(db);
            Nombre = model.Nombre;
            if (model.CategoriaSeleccionada != Guid.Empty)
            {
                Categoria = new Repositorio<Categoria>(db).Traer(model.CategoriaSeleccionada);
            }
            foreach (var item in model.Respuestas)
            {
                if (item.Eliminada == true)
                {
                    var res = Respuestas.First(a => a.Id == item.Id);
                    Respuestas.Remove(res);
                }
                else if (item.Editada == true)
                {
                    var respuestaAux = Respuestas.FirstOrDefault(a => a.Id == item.Id);
                    respuestaAux.Modificar(item);
                }
                else if(item.Nueva){
                    Respuesta respuesta = new Respuesta(item);
                    repoRespuesta.Crear(respuesta);
                    Respuestas.Add(respuesta);

                }
               
            }
        }
    }
}