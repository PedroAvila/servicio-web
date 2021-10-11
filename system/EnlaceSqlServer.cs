using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;

namespace Servicios
{
    public class EnlaceSqlServer
    {
        private static SqlConnection _conexion;

        public static SqlConnection Conexion
        {
            get { return EnlaceSqlServer._conexion; }
        }

        public static bool ConectarSqlServer()
        {
            bool estado = false;

            try
            {
                if (_conexion == null )
                {
                    _conexion = new SqlConnection();
                    _conexion.ConnectionString = string.Concat("Data Source=", DatosEnlace.IpBaseDatos, "; Initial Catalog=", DatosEnlace.NombreBaseDatos, "; User ID=", DatosEnlace.UsuarioBaseDatos, "; Password=", DatosEnlace.PasswordBaseDatos);
                    Thread.Sleep(750);
                }

                if (_conexion.State == ConnectionState.Closed)
                {
                    _conexion.Open();
                }

                if (_conexion.State == ConnectionState.Broken)
                {
                    _conexion.Close();
                    _conexion.Open();
                }

                if (_conexion.State == ConnectionState.Connecting)
                {
                    while (_conexion.State == ConnectionState.Connecting)
                    {
                        Thread.Sleep(500);
                    }
                }

                estado = true;
            }
            catch (Exception e)
            {
                estado = false;
                Funciones.Logs("ENLACESQLSERVER", $"Problemas de conexión; Captura Error: {e.Message}");
                Funciones.Logs("ENLACESQLSERVERDBUG", e.StackTrace);
            }

            return estado;
        }
    }
}