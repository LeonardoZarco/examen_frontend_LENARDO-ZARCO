using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Usuario
    {

        public static Modelo.Result Add(Modelo.Usuario usuario)
        {
            Modelo.Result result = new Modelo.Result();

            try
            {
                using (AccesoDatos.examen_backend_Leonardo_ZarcoEntities context = new AccesoDatos.examen_backend_Leonardo_ZarcoEntities())

                {
                    var query = context.AgregarUsuario_y_datos(usuario.Nombre,
                        usuario.ApellidoPaterno,
                        usuario.ApellidoMaterno,
                        usuario.Direccion,
                        usuario.Telefono,
                        usuario.FechaNacimiento,
                        usuario.UserName,
                        usuario.Email,
                        usuario.Password);
                    //s
                    

                    if (query > 0) result.Correct = true;
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
                result.Ex = e;
                throw;
            }

            return result;
        }
        public static Modelo.Result Update(Modelo.Usuario usuario)
        {
            Modelo.Result result = new Modelo.Result();
            try
            {
                using (AccesoDatos.examen_backend_Leonardo_ZarcoEntities context = new AccesoDatos.examen_backend_Leonardo_ZarcoEntities())
                {
                    var query = context.ModificarUsuarioDatos(usuario.ID,
                        usuario.Nombre,
                        usuario.ApellidoPaterno,
                        usuario.ApellidoMaterno,
                        usuario.Direccion,
                        usuario.Telefono,
                        usuario.FechaNacimiento,
                        usuario.UserName,
                        usuario.Email,
                        usuario.Password);

                    if (query > 0) result.Correct = true;
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.Ex = e;
                result.ErrorMessage = e.Message;
                throw;
            }

            return result;
        }

        public static Modelo.Result Delete(int id)
        {

            Modelo.Result result = new Modelo.Result();

            try
            {
                using (AccesoDatos.examen_backend_Leonardo_ZarcoEntities context = new AccesoDatos.examen_backend_Leonardo_ZarcoEntities())
                {
                    var query = context.EliminarUsuario(id);
                    if (query > 0) result.Correct = true;
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.Ex = e;
                result.ErrorMessage = e.Message;
                throw;
            }

            return result;
        }


        public static Modelo.Result GetAll()
        {
            Modelo.Result result = new Modelo.Result();

            try
            {
                using (AccesoDatos.examen_backend_Leonardo_ZarcoEntities context = new AccesoDatos.examen_backend_Leonardo_ZarcoEntities())
                {
                    var query = context.GetUsuarioDatos();
                    result.Objects = new List<object>();

                    foreach (AccesoDatos.GetUsuarioDatos_Result resultado in query.ToList())
                    {
                        Modelo.Usuario usuario = new Modelo.Usuario();

                        usuario.ID = resultado.ID;
                        usuario.Nombre = resultado.Nombre;
                        usuario.ApellidoPaterno = resultado.ApellidoPaterno;
                        usuario.ApellidoMaterno = resultado.ApellidoMaterno;
                        usuario.Direccion = resultado.Direccion;
                        usuario.Telefono = resultado.Telefono;
                        usuario.FechaNacimiento = resultado.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                        usuario.UserName = resultado.UserName;
                        usuario.Email = resultado.Email;
                        usuario.Password = resultado.Password;


                        result.Objects.Add(usuario);

                    }

                    if (result.Objects.Count > 0) result.Correct = true;
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.Ex = e;
                result.ErrorMessage = e.Message;

                throw;
            }
            return result;
        }





        public static Modelo.Result GetById(int id)
        {
            Modelo.Result result = new Modelo.Result();

            try
            {
                using (AccesoDatos.examen_backend_Leonardo_ZarcoEntities context = new AccesoDatos.examen_backend_Leonardo_ZarcoEntities())

                {
                    var query = context.GetByUsuarioDatos(id).FirstOrDefault();

                    result.Objects = new List<object>();
                    Modelo.Usuario usuario = new Modelo.Usuario();
                    if (query != null)
                    {

                        Modelo.Usuario usuarios = new Modelo.Usuario();

                        usuario.ID = query.ID;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Direccion = query.Direccion;
                        usuario.Telefono = query.Telefono;
                        usuario.FechaNacimiento = query.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                        usuario.UserName = query.UserName;
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;



                        result.Object = usuario;
                    }

                    if (result.Object != null) result.Correct = true;
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.Ex = e;
                result.ErrorMessage = e.Message;
                throw;
            }

            return result;
        }


    }
}