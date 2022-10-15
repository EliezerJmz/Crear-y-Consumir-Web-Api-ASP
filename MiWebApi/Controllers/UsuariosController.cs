using ConectarDatos;

using System.Collections.Generic;
using System.Linq;

using System.Web.Http;
using System.Data.Entity;
using System.Web.Http.Cors;

//Instalar el complemento:
using Microsoft.AspNetCore.JsonPatch;


namespace MiWebApi.Controllers
{
    ////aquí indicamos los clientes que tendrán acceso a la api
    [EnableCors(origins: "https://localhost:44392/", headers:"*", methods:"*")]
    public class UsuariosController : ApiController
    {
        private UsuariosDBEntities dbContext = new UsuariosDBEntities();

        //Mostrar todos los registros (api/usuarios)
        [HttpGet]
        public IEnumerable<usuario> Get()
        {
            using (UsuariosDBEntities db = new UsuariosDBEntities())
            {
                return db.usuarios.ToList();
            }
        }

        //Obtener un registro por su Id:
        [HttpGet]
        public usuario Get(int id)
        {
            using(UsuariosDBEntities db = new UsuariosDBEntities()){

                return db.usuarios.FirstOrDefault(u => u.int_id == id);
            }
        }

        //Crear nuevos Registros
        [HttpPost]
        public IHttpActionResult AgregaUsuario([FromBody]usuario user)
        {
            if (ModelState.IsValid)
            {
                dbContext.usuarios.Add(user);
                dbContext.SaveChanges();
                return Ok(user);
            }
            else
            {
                return BadRequest();
            }
        }

        //Actualizar Registros
        [HttpPut]
        public IHttpActionResult ActulizarUsuario(int id, [FromBody]usuario user)
        {
            if (ModelState.IsValid)
            {
                var UsuarioExiste = dbContext.usuarios.Count(c => c.int_id == id) > 0;

                if (UsuarioExiste)
                {
                    dbContext.Entry(user).State = EntityState.Modified;
                    dbContext.SaveChanges();

                    return Ok();
                }
                else { return NotFound(); }
            }
            else { return BadRequest(); }
        }




        //Actuaizacion Parcial   
        //recibe un json unicamente con los campos a actualizar, no todo el registro
        [HttpPatch]
        public IHttpActionResult ActualizacionParcialUsuario(int id, [FromBody] JsonPatchDocument<usuario> patchUser)
        {

            if (patchUser != null)
            {
                var userDeLaDB = dbContext.usuarios.FirstOrDefault(x => x.int_id == id);

                if (userDeLaDB != null)
                {
                    patchUser.ApplyTo(userDeLaDB);

                    dbContext.SaveChanges();

                    return Ok(userDeLaDB);
                }
            }
        
            return NotFound();
        }



        //Eliminar Registros
        [HttpDelete]
        public IHttpActionResult EliminarUsuario(int id)
        {
            var user = dbContext.usuarios.Find(id);

            if(user != null)
            {
                dbContext.usuarios.Remove(user);
                dbContext.SaveChanges();

                return Ok(user);
            }
            else { return NotFound(); }
        }

        //Obtener los registros por departamento
        [HttpGet]
        public IEnumerable<usuario> Departamento(string depa)
        {
            using (UsuariosDBEntities db = new UsuariosDBEntities())
            {
                var departamento = db.usuarios.Where(u => u.str_departamento == depa).ToList();
                return departamento;
            }
        }

        //Obtener los registros por nombre
        [HttpGet]
        public IEnumerable<usuario> Nombre(string nomb)
        {
            using (UsuariosDBEntities db = new UsuariosDBEntities())
            {
                var nombre = db.usuarios.Where(u => u.str_nombre == nomb).ToList();
                return nombre;
            }
        }

        //Intervalo mostrar los usuarios entre un intervalo de ids
        [HttpGet]
        public IEnumerable<usuario> Intervalo(int id1, int id2)
        {
            using (UsuariosDBEntities db = new UsuariosDBEntities())
            {
                var intervalo = db.usuarios.Where(u => u.int_id >= id1 && u.int_id <= id2).ToList();
                return intervalo;
            }
        }

        //Obtener las direcciones que contengan un texto en espesifico
        [HttpGet]
        public IEnumerable<usuario> ContieneDireccion(string dir)
        {
            using (UsuariosDBEntities db = new UsuariosDBEntities())
            {
                var direccion = db.usuarios.Where(u => u.str_direccion.Contains(dir)).ToList();    
                return direccion;
            }
        }


        //Convertir campo a mayuscula que contenga un texto en espedifico
        [HttpGet]
        public IEnumerable<usuario> NombreMayuscula(string nombupper)
        {
            using (UsuariosDBEntities db = new UsuariosDBEntities())
            {
                //usando lambda
                //var NombreUpper = db.usuarios.Where(u => u.str_nombre.Contains(nombupper)).ToList();

                //Usando linq
                var NombreUpper = (from u in db.usuarios
                          where u.str_nombre.Contains(nombupper)
                          select u).ToList();

                //Recorremos la lista y asignamos a mayusculas los campos nombre y departamento
                NombreUpper.ForEach(u =>
                {
                    u.str_nombre = u.str_nombre.ToUpper();
                    u.str_departamento = u.str_departamento.ToUpper();
                });

                return NombreUpper;

            }
        }

        //Order by
        //Para diferenciar los metodos de la api podemos indicar una Route
        //si no indicamos ruta se pueden diferenciar por el nombre del parametro que reciben
        //pero si son iguales o no reciben parametro debemos indicarles un nombre de ruta
        [HttpGet]
        [Route("api/OrderByNombre")]
        public IEnumerable<usuario> OrderByNombre()
        {
            using(UsuariosDBEntities db = new UsuariosDBEntities())
            {
                var OrderByNombre = db.usuarios.OrderBy(u => u.str_nombre).ToList();

                return OrderByNombre;
            }
        }


        //OrderByDescending
        //Para diferenciar los metodos de la api podemos indicar una Route
        //ni no indicamos rta se pueden diferenciar el nombre del parametro que reciben
        //pero si son iguales o no reciben parametro debemos indicarles un nombre de ruta
        [HttpGet]
        [Route("api/OrderByDescendingNombre")]
        public IEnumerable<usuario> OrderByDescendingNombre()
        {
            using (UsuariosDBEntities db = new UsuariosDBEntities())
            {
                var OrderByDescendingNombre = db.usuarios.OrderByDescending(u => u.str_nombre).ToList();

                return OrderByDescendingNombre;
            }
        }


        //OrderBy ThenBy  mas de un campo:
        //Para diferenciar los metodos de la api podemos indicar una Route
        //ni no indicamos rta se pueden diferenciar el nombre del parametro que reciben
        //pero si son iguales o no reciben parametro debemos indicarles un nombre de ruta
        [HttpGet]
        [Route("api/OrderByNombreDepartamento")]
        public IEnumerable<usuario> OrderByNombreDepartamento()
        {
            using (UsuariosDBEntities db = new UsuariosDBEntities())
            {
                var OrderByNombreDepartamento = db.usuarios.OrderBy(u => u.str_nombre).ThenBy(u => u.str_departamento) .ToList();

                return OrderByNombreDepartamento;
            }
        }

        [HttpGet]
        [Route("api/TakeNombres")]
        public IEnumerable<usuario> TakeNombres(int cantidad)
        {
            using (UsuariosDBEntities db = new UsuariosDBEntities())
            {
                var TakeNombre = db.usuarios.OrderBy(u => u.str_nombre).Take(cantidad).ToList();

                return TakeNombre;
            }
        }

        [HttpGet]
        [Route("api/SkipCincoNombres")]
        public IEnumerable<usuario> SkipCincoNombres()
        {
            using (UsuariosDBEntities db = new UsuariosDBEntities())
            {
                var SkipNombre = db.usuarios.OrderBy(u => u.str_nombre).Skip(5).ToList();

                return SkipNombre;
            }
        }

        [HttpGet]
        [Route("api/Skipt10Take2Nombres")]
        public IEnumerable<usuario> Skipt10Take2Nombres()
        {
            using (UsuariosDBEntities db = new UsuariosDBEntities())
            {
                var Skipt10Take2Nombres = db.usuarios.OrderBy(u => u.str_nombre).Skip(10).Take(2).ToList();

                return Skipt10Take2Nombres;
            }
        }


    }
}
