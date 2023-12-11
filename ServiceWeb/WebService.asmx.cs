using ServiceWeb.Metodos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ServiceWeb
{
    /// <summary>
    /// Descripción breve de WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola soy Luis Hernández";
        }

        [WebMethod]
        public string Saludo(string Nombre)
        {
            return "Hola tu " + Nombre;
        }
        [WebMethod]
        public string GuardarLog(string mensaje)
        {
            Funciones.Logs("NombreArchivo", mensaje);
            return "se genero el log";
        }
        [WebMethod]

        public List<Equipos> ObtenerEquipos()
        {
            List<Equipos> equipos = new List<Equipos>();
            equipos.Add(new Equipos() { nombre = "Milan", pais = "Italia" });
            equipos.Add(new Equipos() { nombre = "America", pais = "México" });
            equipos.Add(new Equipos() { nombre = "Barcelona", pais = "España" });
            return equipos;
        }

        [WebMethod]
        public string AgregarEquipos(Equipos[] equipos)
        {
            foreach (Equipos equipo in equipos)
            {
                Funciones.Logs("Equipos", equipo.nombre + "-" + equipo.pais);
            }
            return "proceso realizado con exito";
        }
        [WebMethod]
        public string ObtenerProductos()
        {
            List<Dictionary<string, string>> json = new List<Dictionary<string, string>>();

            if(!EnlaceSqlServer.ConectarSqlServer())
            {
                //error en la conexión
                return "Error en la conexión";
            }
            try
            {
                //consulta a tabla productos
                SqlCommand com = new SqlCommand("SELECT * FROM Productos", EnlaceSqlServer.Conexion);
                com.CommandType = System.Data.CommandType.Text;
                com.CommandTimeout = DatosEnlace.timeOutSqlServer;

                SqlDataReader record = com.ExecuteReader();

                if (record.HasRows)
                {
                    Dictionary<string, string> row;
                    while (record.Read())
                    {
                        row = new Dictionary<string, string>();
                        for (int i = 0; i <record.FieldCount; i++)
                        {
                            row.Add(record.GetName(i), record.GetValue(i).ToString());
                        }
                        json.Add(row);
                    }
                }
                //liberamos memoria
                record.Close();
                record.Dispose();
                record = null;
                //optimizamos el uso del server
                com.Dispose();
            }
            catch(Exception ex) 
            {
                Funciones.Logs("ObtenerProductos", ex.Message);
                Funciones.Logs("ObtenerProductos_DEBUG", ex.StackTrace);
            }

            //string jsonString = JsonSerializer.Seralize(json);
            return JsonConvert.SerializeObject(json);
        }
        
    }
}
