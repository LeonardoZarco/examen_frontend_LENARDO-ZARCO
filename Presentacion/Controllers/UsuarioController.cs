using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            Modelo.Usuario usuario = new Modelo.Usuario();
            usuario.Usuarios = new List<Object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);

                var responseTask = client.GetAsync("Usuario/GetUsuarioDatos");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Modelo.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        Modelo.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<Modelo.Usuario>(resultItem.ToString());
                        usuario.Usuarios.Add(resultItemList);
                    }
                }
            }
            return View(usuario);
        }
        [HttpPost]
        public ActionResult Form(Modelo.Usuario usuario)
        {
            Modelo.Result result = new Modelo.Result();

            if (ModelState.IsValid)
            {
                if (usuario.ID == 0)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);

                        var postTask = client.PostAsJsonAsync<Modelo.Usuario>("Usuario/AgregarUsuario_y_datos", usuario);
                        postTask.Wait();

                        var resultProducto = postTask.Result;
                        if (resultProducto.IsSuccessStatusCode)

                        {
                            ViewBag.Message = "El usuario se registro correctamente";
                        }
                        else
                        {
                            ViewBag.Message = "El usuario no se ha registrado correctamente" + result.ErrorMessage;
                        }
                    }
                }
                else
                {

                    using (var client = new HttpClient())
                    {

                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);


                        var postTask = client.PostAsJsonAsync<Modelo.Usuario>("Usuario/ModificarUsuarioDatos/" + usuario.ID, usuario);
                        postTask.Wait();

                        var resultUsario = postTask.Result;

                        if (resultUsario.IsSuccessStatusCode)
                        {
                            ViewBag.Message = "El usuario se ha actualizado correctamente";
                        }
                        else
                        {
                            ViewBag.Message = "El usuario no se ha actualizado correctamente" + result.ErrorMessage;
                        }
                    }
                }
            }
            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {


            Modelo.Result resultUsuario = new Modelo.Result();


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);

                var postTask = client.GetAsync("Usuario/EliminarUsuario/" + ID);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "El usuario ha sido eliminada";

                }
                else
                {
                    ViewBag.Message = "El usuario no pudo ser eliminada" + resultUsuario.ErrorMessage;
                }
            }

            return PartialView("Modal");


        }






        //        ////**_______________________________________________________**//
        [HttpGet]
        public ActionResult Form(int? ID)
        {
            Modelo.Usuario usuario = new Modelo.Usuario();
            //Modelo.Result resultAcceso = Negocio.Datos.GetAll();
            //usuario.Datos = new Modelo.Dato();

            if (ID == null) //Add
            {
                
                return View(usuario);
            }
            else
            {
                Modelo.Result result = new Modelo.Result();

                using (var client = new HttpClient())
                    try
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);
                        var responseTask = client.GetAsync("Usuario/GetByUsuarioDatos/" + ID);
                        responseTask.Wait();

                        var resultAPI = responseTask.Result;
                        if (resultAPI.IsSuccessStatusCode)
                        {
                            var readTask = resultAPI.Content.ReadAsAsync<Modelo.Result>();
                            readTask.Wait();

                            Modelo.Usuario resultItemList = new Modelo.Usuario();
                            resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<Modelo.Usuario>(readTask.Result.Object.ToString());
                            result.Object = resultItemList;


                            usuario = ((Modelo.Usuario)result.Object);

                          

                            return View(usuario);
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en la tabla";
                        }
                    }

                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                    }

                return View();
            }
        }
    }
}