﻿using System.Net;
using System.Web.Http;

namespace Servicios.Controllers
{
    public class UsuarioController : ApiController
    {

        [HttpGet]
        [Route("api/Usuario/GetUsuarios")]

        public IHttpActionResult GetAll()
        {
            Modelo.Result result = Negocio.Usuario.GetAll();

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }
        [HttpGet]
        [Route("api/Usuario/GetByUsuarioDatos/{ID}")]
        public IHttpActionResult GetById(int ID)
        {
            Modelo.Result result = Negocio.Usuario.GetById(ID);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }


        }

        [HttpPost]
        [Route("api/Usuario/AgregarUsuario_y_datos")]
        public IHttpActionResult Add([FromBody] Modelo.Usuario usuario)
        {
            Modelo.Result result = Negocio.Usuario.Add(usuario);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }


        }

        [HttpPost]
        [Route("api/Usuario/ModificarUsuarioDatos/{ID}")]
        public IHttpActionResult Put(int ID, [FromBody] Modelo.Usuario usuario)
        {
            Modelo.Result result = Negocio.Usuario.Update(usuario);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }

        [HttpGet]
        [Route("api/Usuario/EliminarUsuario/{ID}")]
        public IHttpActionResult Delete(int ID)
        {

            Modelo.Result result = Negocio.Usuario.Delete(ID);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }


    }
}