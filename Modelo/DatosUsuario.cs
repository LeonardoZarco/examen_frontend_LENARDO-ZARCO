using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class DatosUsuario
    {
        public int ID_Datos { get; set; }
        public Modelo.Usuario Usuario { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string FechaNacimiento { get; set; }
    }
}
