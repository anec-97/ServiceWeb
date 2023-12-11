using ServiceWeb;
using ServiceWeb.Metodos;
using System;
using System.Data.SqlClient;

internal static class EnlaceSqlServerHelpers
{

    public static SqlConnection Conexion
    {
        get { return EnlaceSqlServer.conexion; }
    }

    public static bool ConectarSqlServer()
    {
        bool estado = false;

        try
        {
            if (conexion == null)
            {
                conexion = new SqlConnection();
                conexion.ConnectionString = "Data Source =" + DatosEnlace.ipBaseDatos +
                    "; Initial Catalog= " + DatosEnlace.nombreBaseDatos +
                    "; User ID= " + DatosEnlace.usuarioBaseDatos +
                    "; Password= " + DatosEnlace.passwordBaseDatos +
                    "; MultipleActiveResultSets= true";
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
            if (conexion.State == System.Data.ConnectionState.Connecting)
            {
                while (conexion.State == System.Data.ConnectionState.Connecting)
                {
                    System.Threading.Thread.Sleep(500);
                }
            }
            estado = true;
        }
        catch (Exception ex)
        {
            estado = false;
            Funciones.Logs("EnlaceSQLServer", "Problemas al abrir la conexion" + ex.Message);
            Funciones.Logs("EnlaceSQLServer_Debug", ex.StackTrace);
        }
        return estado;
    }
}