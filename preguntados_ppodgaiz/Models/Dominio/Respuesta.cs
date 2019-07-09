using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using preguntados_ppodgaiz.Models.ViewModels.PreguntaRespuesta;

namespace preguntados_ppodgaiz.Models.Dominio
{
    public class Respuesta:Entidad
    {
       
        public virtual Pregunta Pregunta { get; set; }
        public bool EsCorrecta { get; set; }

        public Respuesta()
        {

        }

        public Respuesta(RespuestaViewModel item)
        {
            Nombre = item.Nombre;
            EsCorrecta = item.EsCorrecta;
        }

        public Respuesta(string correct_answer, bool esCorrecta)
        {
            this.Nombre = correct_answer;
            this.EsCorrecta = esCorrecta ;
        }

        internal void Modificar(RespuestaViewModel item)
        {
            Nombre = item.Nombre;
            EsCorrecta = item.EsCorrecta;
        }
    }
}