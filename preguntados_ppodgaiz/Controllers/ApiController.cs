using Newtonsoft.Json;
using preguntados_ppodgaiz.Models;
using preguntados_ppodgaiz.Models.Dominio;
using preguntados_ppodgaiz.Models.ViewModels.API;
using preguntados_ppodgaiz.Repositorio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace preguntados_ppodgaiz.Controllers
{
    public class ApiController : Controller
    {
        private ApplicationDbContext db;

        public ApiController()
        {
            db = new ApplicationDbContext();
        }
        public async Task<List<APIPreguntasElement>> GetListadoApi()
        {
            string url = "https://opentdb.com/api.php?amount=50&type=multiple&encode=url3986&difficulty=easy";
            string json = await LlamarAPI(url);
            var listado = JsonConvert.DeserializeObject<APIPreguntas>(json);

            return listado.results;
        }

        public static async Task<string> LlamarAPI(string url)
        {
            using (var cliente = new HttpClient())
            {
                var minutos = 60;
                cliente.Timeout = new TimeSpan(0, minutos, 0);
                var result = await cliente.GetAsync(url);
                return await result.Content.ReadAsStringAsync();
            }
        }

        public async Task<ActionResult> CreateApiAsync()
        {
            Random rnd = new Random();
            Color randomColor;
            var repoCategoria = new Repositorio<Categoria>(db);
            var repoPreguntas = new Repositorio<Pregunta>(db);
            var categorias = repoCategoria.TraerTodos();
            var preguntas = repoPreguntas.TraerTodos();
            //Traer listado de api
            var listadoApi = await GetListadoApi();
            var listadoCategoriaNombres = listadoApi.Select(p => p.category).Distinct().ToList();
            var listadoPreguntasNombre = listadoApi.Select(p => p.question).Distinct().ToList();
            foreach (var item in listadoCategoriaNombres)
            {
                if (!categorias.Any(p => p.Nombre == item))
                {
                    randomColor = Color.FromArgb(0,rnd.Next(50,140), rnd.Next(50, 140), rnd.Next(50, 140)   );
                    var categoria = new Categoria(item, randomColor.Name, db);
                    repoCategoria.Crear(categoria);
                }
            }
            Pregunta pregunta;
            //Recorrer el listado y por cada elemento crear una categoria, si no esta creada!!REcuperar el id de categoria
            foreach (var item in listadoApi)
            {
                if (!preguntas.Any(p => p.Nombre == item.question))
                {
                    List<Respuesta> respuestas = new List<Respuesta>();
                    respuestas.Add(new Respuesta(item.correct_answer,true));
                    foreach (var respuesta in item.incorrect_answers)
                    {
                        respuestas.Add(new Respuesta(respuesta,false));
                    }
                    pregunta = new Pregunta
                    {
                        Nombre = item.question,
                        Respuestas = respuestas, 
                        Categoria = repoCategoria.TraerTodos().Where(p => p.Nombre == item.category).FirstOrDefault()
                    };
                    repoPreguntas.Crear(pregunta);
                }
            }
            return RedirectToAction("Index", "PreguntaRespuesta");
        }
    }
}