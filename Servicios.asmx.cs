using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace Servicios
{
    /// <summary>
    /// Descripción breve de Servicios
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Servicios : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }
        
        [WebMethod(Description = "Saluda a la persona")]
        public string Saludar( string nombre )
        {
            return $"Hola {nombre}";
        }

        [WebMethod]
        public string GuardarLog( string mensaje )
        {
            Funciones.Logs("LogServicios", mensaje);
            return "Ok";
        }

        [WebMethod]
        public int Sumar(int numero1, int numero2)
        {
            int suma = numero1 + numero2;
            return suma;
        }

        [WebMethod]
        public string[] Obtenerfrutas()
        {
            string[] frutas = new string[3];
            frutas[0] = "Fresa";
            frutas[1] = "Limón";
            frutas[2] = "Melón";
            return frutas;
        }

        [WebMethod]
        public string GuardarFrutas( string[] frutas)
        {
            foreach (var fruta in frutas)
            {
                Funciones.Logs("Frutas", fruta);
            }
            return "Proceso realizado con éxito";
        }

        [WebMethod]
        public List<Equipos> ObtenerEquipos()
        {
            var equipos = new List<Equipos>();

            equipos.Add(new Equipos { Nombre = "Milan", Pais = "Italia" });
            equipos.Add(new Equipos { Nombre = "AJAX", Pais = "Holanda" });

            return equipos;
        }

        [WebMethod]
        public string GuardarEquipos(Equipos[] equipos)
        {
            foreach (Equipos equipo in equipos)
            {
                Funciones.Logs("Equipos", $"{equipo.Nombre} - {equipo.Pais}");
            }
            return "Proceso realizado con éxito";
        }

        [WebMethod]
        public string GuardarXML(string xml)
        {

            XmlDocument dataXml = new XmlDocument();
            dataXml.LoadXml(xml);

            XmlNode documento = dataXml.SelectSingleNode("documento");
            string deporte = documento["deporte"].InnerText;
            Funciones.Logs("XML", $"Deporte: {deporte} Equipos: ");

            XmlNodeList nodeEquipos = dataXml.GetElementsByTagName("equipos");
            XmlNodeList equipos = ((XmlElement)nodeEquipos[0]).GetElementsByTagName("equipo");
            foreach (XmlElement equipo in equipos)
            {
                string nombre = equipo.GetElementsByTagName("nombre")[0].InnerText;
                string pais = equipo.GetElementsByTagName("pais")[0].InnerText;

                Funciones.Logs("XML", $"{nombre} - {pais}");
            }
            return "Proceso realizado con éxito";
        }

        [WebMethod]
        public string RetornarJson()
        {
            dynamic json = new Dictionary<string, dynamic>();
            json.Add("Deporte", "Futbol");

            List<Dictionary<string, string>> equipos = new List<Dictionary<string, string>>();
            Dictionary<string, string> equipo1 = new Dictionary<string, string>();

            equipo1.Add("Nombre", "Manchester");
            equipo1.Add("Pais", "Inglaterra");

            equipos.Add(equipo1);

            Dictionary<string, string> equipo2 = new Dictionary<string, string>();

            equipo2.Add("Nombre", "Valencia");
            equipo2.Add("Pais", "España");

            equipos.Add(equipo2); 

            json.Add("equipos", equipos);

            return JsonConvert.SerializeObject(json);

        }

        [WebMethod]
        public string GuardarJson(string json)
        {
            var dataJson = JsonConvert.DeserializeObject<DataJson>(json);
            Funciones.Logs("JSON", $"Deporte: {dataJson.Deporte}; Equipos: ");

            foreach (var equipo in dataJson.equipos)
            {
                Funciones.Logs("JSON", $"{equipo.Nombre} - {equipo.Pais}");

            }

            return "Proceso realizado con éxito";
        }

        [WebMethod]
        public string ObtenerProductos()
        {
            List<Dictionary<string, string>> json = new List<Dictionary<string, string>>();

            if (!EnlaceSqlServer.ConectarSqlServer())
            {
                return "";
            }

            try
            {
                using (var com = new SqlCommand("SELECT * FROM Productos", EnlaceSqlServer.Conexion))
                {
                    com.CommandType = CommandType.Text;
                    com.CommandTimeout = DatosEnlace.TimeOutSqlServer;

                    using (SqlDataReader record = com.ExecuteReader())
                    {
                        if (record.HasRows)
                        {
                            Dictionary<string, string> row;
                            while (record.Read())
                            {
                                row = new Dictionary<string, string>();
                                for (int f = 0; f < record.FieldCount; f++)
                                {
                                    row.Add(record.GetName(f), record.GetValue(f).ToString());
                                }
                                json.Add(row);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Funciones.Logs("ObtenerProductos", e.Message);
                Funciones.Logs("ObtenerProductos_DEBUG", e.StackTrace);
            }

            return JsonConvert.SerializeObject(json);
        }

    }
}
