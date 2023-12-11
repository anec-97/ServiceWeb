using ServiceWeb.Metodos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ServiceWeb
{
    public class EnlaceSqlServer
    {
        private static SqlConnection conexion = null;

        //Estado de la conexion
        public static SqlConnection Conexion
        { 
            get { return EnlaceSqlServer.conexion; } 
        }
        //conexión
        public static bool ConectarSqlServer()
        {
            bool estado = false;

            try
            {
                if (conexion ==null)
                {
                    conexion = new SqlConnection();
                    conexion.ConnectionString = "Data Source= " + DatosEnlace.ipBaseDatos +
                        "; Initial Catalog= " + DatosEnlace.nombreBaseDatos +
                        "; User ID=" + DatosEnlace.usuarioBaseDatos +
                        "; Password=" + DatosEnlace.passwordBaseDatos +
                        "; MultipleActiveResultSets=True";
                    System.Threading.Thread.Sleep(750);
                }
                if (conexion.State == System.Data.ConnectionState.Closed)
                {
                    conexion.Open();
                }
                if (conexion.State == System.Data.ConnectionState.Broken)
                {
                    conexion.Close();
                    conexion.Open();
                }
                if(conexion.State == System.Data.ConnectionState.Connecting)
                {
                    while (conexion.State == System.Data.ConnectionState.Connecting)
                    {
                        System.Threading.Thread.Sleep(500);
                    }
                }
                estado = true;
            }
            catch(Exception ex) 
            {
                estado = false;
                Funciones.Logs("EnlaceSQLServer", "Problemas al abrir conexión" + ex.Message);
                Funciones.Logs("EnlaceSQLServer_Debug", ex.StackTrace);
            }
            return estado;
        }

    }
}