using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using preguntados_ppodgaiz.Models.ViewModels.Usuario;

namespace preguntados_ppodgaiz.Models.Dominio
{
    public class Usuario : Entidad
    {
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string NickName { get; set; }
        public string EMail { get; set; }

        public Usuario()
        {
           
        }

        internal void Modificar(UsuarioABMViewModel model)
        {
            this.Nombre = model.Nombre;
            this.NickName = model.NickName;
            this.EMail = model.Email;
        }
    }
}