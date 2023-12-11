using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ServiceWeb.Metodos
{
    public class Funciones
    {
        public static void Logs(string Nombre_archivo, string Descripcion)
        {
            //almacenaremos en el directorio raiz del servicio web
            string directorio = AppDomain.CurrentDomain.BaseDirectory + "Logs/" +
                DateTime.Now.Year.ToString() + "/" +
                 DateTime.Now.Month.ToString() + "/" +
                  DateTime.Now.Day.ToString();

            if (!Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }
            StreamWriter mi_archivo = new StreamWriter(directorio + "/" + Nombre_archivo + ".txt", true);
            string cadena = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ">>>" + Descripcion;
            //almacenamos en mi archivo

            mi_archivo.WriteLine(cadena);
            mi_archivo.Close();
        }

    }
}