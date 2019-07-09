using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.Dominio
{
    public class LogEvento : Entidad
    {
        public Enums.AccionesLogEnum Accion { get; set; }
        public Enums.EntidadLogEnum Entidad { get; set; }
        public Guid EntidadId { get; set; }
        public string usuarioId { get; set; }
    }
}