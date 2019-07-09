using preguntados_ppodgaiz.Models;
using preguntados_ppodgaiz.Models.Dominio;
using preguntados_ppodgaiz.Models.Enums;
using preguntados_ppodgaiz.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace preguntados_ppodgaiz.Helpers
{
    public class LoggerEventos
    {
        public static void LogearEvento(string nombre, string usuarioId, Guid entidadGuid, AccionesLogEnum accion, EntidadLogEnum tipoEntidad)
        {
            var log = new LogEvento()
            {
                Nombre = nombre,
                usuarioId = usuarioId,
                EntidadId = entidadGuid,
                Accion = accion,
                Entidad = tipoEntidad
            };


            var t = new Thread(() => GuardarEvento(log));
            t.Start();
        }

        public static void GuardarEvento(LogEvento evento)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                new Repositorio<LogEvento>(db).Crear(evento);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}