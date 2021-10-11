using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Servicios
{
    public class Funciones
    {
        public static void Logs( string nombreArchivo, string descripcion )
        {
            string directorio = AppDomain.CurrentDomain.BaseDirectory + "logs/" +
                DateTime.Now.Year.ToString() + "/" +
                DateTime.Now.Month.ToString() + "/" +
                DateTime.Now.Day.ToString();

            if (!Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }

            StreamWriter miArchivo = new StreamWriter(String.Concat(directorio, "/", nombreArchivo, ".txt"), true);

            var cadena = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " >>> " + descripcion;

            miArchivo.WriteLine(cadena);
            miArchivo.Close();

        }
    }
}