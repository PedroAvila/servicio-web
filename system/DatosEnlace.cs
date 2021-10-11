using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Servicios
{
    public class DatosEnlace
    {
        public static string IpBaseDatos = ConfigurationManager.AppSettings["IpBaseDatos"].ToString();
        public static string NombreBaseDatos = ConfigurationManager.AppSettings["NombreBaseDatos"].ToString();
        public static string UsuarioBaseDatos = ConfigurationManager.AppSettings["UsuarioBaseDatos"].ToString();
        public static string PasswordBaseDatos = ConfigurationManager.AppSettings["PasswordBaseDatos"].ToString();
        public static int TimeOutSqlServer = int.Parse(ConfigurationManager.AppSettings["TimeOutSqlServer"]);

    }
}