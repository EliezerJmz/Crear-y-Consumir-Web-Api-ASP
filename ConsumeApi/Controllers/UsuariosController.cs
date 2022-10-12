using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//complementos a utilizar:
using ConsumeApi.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace ConsumeApi.Controllers
{
    public class UsuariosController : Controller
    {
        //URL base de la web api, variable global
        string UrlBase = "https://localhost:44355/";

        // GET: Usuarios
        //Usa un metodo asyncrono y una tarea de tipo AcntionResult
        public async Task<ActionResult> Index()
        {
            //creamos una lista donde guardaremos la informacion de los usuarios
            List<Usuarios> InfoUsuarios = new List<Usuarios>();

            //realizamos la conexion utilizanod HttpClient()
            using (var client = new HttpClient())
            {
                //URL base de la api
                client.BaseAddress = new Uri(UrlBase);
                client.DefaultRequestHeaders.Clear();
                
                //indicamos el encabezado de la api que seran json
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                //hacemos una peticion await a la parte de la api que lista a tosos ls usuarios usando Httpclient
                HttpResponseMessage Respuesta = await client.GetAsync("api/usuarios");

                //validamos si obtenemos una Respuesta con exito
                if (Respuesta.IsSuccessStatusCode)
                {
                    //si Respuesta = true entra y asigna los datos a la varaible UsuariosRespuesta
                    var UsuariosRespuesta = Respuesta.Content.ReadAsStringAsync().Result;

                    //Deserializar el api y asigna los datos de UsuariosRespuesta a la lista InfoUsuarios
                    InfoUsuarios = JsonConvert.DeserializeObject<List<Usuarios>>(UsuariosRespuesta);
                }
                //Retorna la lista de todos los usuarios
                return View(InfoUsuarios);
            }
        }

        //GET USUARIO POR ID
        //realizamos una busqueda del registro por medio de su ID, usamos un GET
        public ActionResult Detail(int id)
        {
            string api = "api/usuarios/";
            Usuarios usuario = new Usuarios();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlBase);
                //HTTP GET
                var responseTask = client.GetAsync(api + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Usuarios>();
                    readTask.Wait();
                    usuario = readTask.Result;
                }
            }
            return View(usuario);
        }


        //Crea nuevo usuario
        //carga el formulario y llama al metodo HttpPost Create
        public ActionResult Create()
        {
            return View();
        }

        //POST Usuarios
        [HttpPost]
        public ActionResult Create(Usuarios usuarios)
        {
            string api = "api/usuarios";
            //Usamos una conexion hacia la api
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlBase + api);

                //aqui indicamos al metodo post el registro de tipo usuarios que vamos a crear
                var postTask = client.PostAsJsonAsync<Usuarios>("usuarios", usuarios);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(String.Empty, "Error, al crear un registro.");
            return View(usuarios);
        }

        //EDIT
        //Primero realizamos una busqueda del registro a editar por medio de su ID, usamos un GET
        public ActionResult Edit(int id)
        {
            string api = "api/usuarios/";
            Usuarios usuarios = new Usuarios();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlBase);
                //HTTP GET
                var responseTask = client.GetAsync(api + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Usuarios>();
                    readTask.Wait();
                    usuarios = readTask.Result;
                }
            }
            return View(usuarios);
        }

        [HttpPost]
        public ActionResult Edit(Usuarios usuarios)
        {
            string api = "api/usuarios/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlBase);
                //HTTP POST
                var putTask = client.PutAsJsonAsync(api + usuarios.int_id.ToString(), usuarios);
                putTask.Wait();
                
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //si la actualizacion fue correcta nos redirecciona al index
                    return RedirectToAction("Index");
                }
            }
            return View(usuarios);
        }

        //DELETE
        //BUSCAR REGISTRO A ELIMINAR POR ID
        public ActionResult Delete(int id)
        {
            //api
            string api = "api/usuarios/";
            //modelo
            Usuarios usuarios = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlBase);
                //HTTP GET
                var responseTask = client.GetAsync(api + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Usuarios>();
                    readTask.Wait();
                    usuarios = readTask.Result;
                }
            }
            return View(usuarios);
        }

        [HttpPost]
        public ActionResult Delete(Usuarios usuarios, int id)
        {
            //api
            string api = "api/usuarios/";

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(UrlBase);
                //HTTP DELETE
                var deleteTask = cliente.DeleteAsync(api + id.ToString());
                deleteTask.Wait();  
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(usuarios);
        }



    }
}