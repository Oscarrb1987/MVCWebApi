using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ConectarDatos;

namespace MVCWebApi.Controllers
{
    public class PersonasEjercicioController : ApiController
    {

        private PersonaSoapEntities dbPerson = new PersonaSoapEntities();


        //visualiza todos los registros (api/usuarios)
        [HttpGet]
        public IEnumerable<personasEjercicio> Get()
        {
            using (PersonaSoapEntities personE = new PersonaSoapEntities())
            {
                return personE.personasEjercicios.ToList();
            }

        }

         //--MÉTODO GET-- visualiza solo un registro 
         [HttpGet]
         public personasEjercicio Get(string nif)
         {
             using (PersonaSoapEntities personE = new PersonaSoapEntities())
            {
            return personE.personasEjercicios.FirstOrDefault(e => e.Nif == nif);
            }
         }
        
         // --METODO POST-- para la creación de nuevos registros
         [HttpPost]
         public IHttpActionResult AgregarPersona([FromBody]personasEjercicio per)
         {
            if (ModelState.IsValid)
            {
                dbPerson.personasEjercicios.Add(per);
                dbPerson.SaveChanges();
                return Ok(per);
            }
            else
            {
                return BadRequest();
            }
         }

        //METODO PUT para la modificación de registros
        [HttpPut]
        public IHttpActionResult ModificarUsuario(string nif, [FromBody]personasEjercicio p)
        {
            if (ModelState.IsValid)
            {
                var UsuarioExiste = dbPerson.personasEjercicios.Count(c => c.Nif == nif) > 0;

                if (UsuarioExiste)
                {
                    dbPerson.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    dbPerson.SaveChanges();

                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        //METODO DELETE
        [HttpDelete]
        public IHttpActionResult BorrarUsuario(string nif)
        {
            var p = dbPerson.personasEjercicios.Find(nif);

            if (p != null)
            {
                dbPerson.personasEjercicios.Remove(p);
                dbPerson.SaveChanges();

                return Ok(p);
            }
            else
            {
                return NotFound();
            }
        }




    }
}
