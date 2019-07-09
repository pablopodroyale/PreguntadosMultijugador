using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using preguntados_ppodgaiz.Models.ViewModels.PreguntaRespuesta;
using preguntados_ppodgaiz.Repositorio;

namespace preguntados_ppodgaiz.Models.Dominio
{
    public class PreguntasRespondidas:Entidad
    {
        public virtual Usuario Usuario { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual Pregunta Pregunta { get; set; }
        public virtual Respuesta Respuesta { get; set; }
        private const  string NOMBRE = "PreguntaRespondida";

        public PreguntasRespondidas()
        {

        }
        public PreguntasRespondidas(PreguntaRespondidaViewModel model, ApplicationDbContext db)
        {

            this.Nombre = NOMBRE;
            setUsuario(model.UsuarioId,db);
            setCategoria(model.CategoriaId,db);
            setPregunta(model.Id, db);
            setRespuesta(model.RespuestaSeleccionadaId,db);
        }

        private void setPregunta(Guid id, ApplicationDbContext db)
        {
            var repoPregunta = new Repositorio<Pregunta>(db);
            this.Pregunta = repoPregunta.Traer(id);
        }

        private void setRespuesta(Guid respuestaSeleccionadaId, ApplicationDbContext db)
        {
            var repoRespuesta = new Repositorio<Respuesta>(db);
            this.Respuesta = repoRespuesta.Traer(respuestaSeleccionadaId);
        }

        private void setCategoria(Guid categoriaId, ApplicationDbContext db)
        {
            var repoCategoria = new Repositorio<Categoria>(db);
            this.Categoria = repoCategoria.Traer(categoriaId);
        }

        private void setUsuario(Guid usuarioId, ApplicationDbContext db)
        {
            var repoUsuario = new Repositorio<Usuario>(db);
            this.Usuario = repoUsuario.Traer(usuarioId);
        }
    }
}