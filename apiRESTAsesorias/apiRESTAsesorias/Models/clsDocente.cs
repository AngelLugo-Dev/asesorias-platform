using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//------------------------
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MySql.Data.MySqlClient;
using System.Data;
//--------------------------
namespace apiRESTAsesorias.Models
{
    public class clsDocente
    {
        public int idDocente { get; set; }
        public string Nombre { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
        public string usuario { get; set; }
        public string contrasenia { get; set; }

        public clsDocente()
        {

        }

        public clsDocente(string usuario, string contrasenia)
        {
            this.usuario = usuario;
            this.contrasenia = contrasenia;
        }

        public clsDocente(int idDocente, string nombre, string paterno, string materno, string usuario, string contrasenia)
        {
            this.idDocente = idDocente;
            this.Nombre = nombre;
            this.paterno = paterno;
            this.materno = materno;
            this.usuario = usuario;
            this.contrasenia = contrasenia;
        }
        // atributos de ambiente
        private string cadCnn = ConfigurationManager.
            ConnectionStrings["bdProyectoIntegrador"].
            ConnectionString;
        // Metodos de procesos

        public DataSet spInsDocente()
        {
            // Crear el comando SQL

            string cadSQL = "";
            cadSQL = "CALL spInsDocente('" + this.idDocente + "', '"
                                           + this.Nombre + "', '"
                                           + this.paterno + "', '"
                                           + this.materno + "',  '"
                                           + this.usuario + "', '"
                                           + this.contrasenia + "');";

            // Configuracion de objetos de conexion
            MySqlConnection cnn = new MySqlConnection(cadCnn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSQL, cadCnn); //da es por datadatos
            DataSet ds = new DataSet();

            //ejecucion y salida
            da.Fill(ds, "spInsDocente"); // <----------------
            return ds;
        }
        public DataSet spValidarAccesoDocente() // ***********Checar en la BD **************
        {
            // Crear el comando SQL
            string cadSQL = "";
            cadSQL = "CALL spValidarAccesoDocente('" + this.usuario + "', '"
                                           + this.contrasenia + "');";

            // Configuracion de objetos de conexion
            MySqlConnection cnn = new MySqlConnection(cadCnn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSQL, cadCnn); //da es por datadatos
            DataSet ds = new DataSet();
            //ejecucion y salida
            da.Fill(ds, "spValidarAccesoDocente"); // <----------------
            return ds;
            //ds es igual al dataset
        }
        //Creacion de una vista para vwRptUsuario
        public DataSet vwAsesoria()
        {
            // Crear el comando SQL
            string cadSQL = "";
            cadSQL = "select * from vwAsesoria;";
            // Configuracion de objetos de conexion
            MySqlConnection cnn = new MySqlConnection(cadCnn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSQL, cadCnn); //da es por datadatos
            DataSet ds = new DataSet();
            //ejecucion y salida
            da.Fill(ds, "vwAsesoria"); // <----------------
            return ds;
            //ds es igual al dataset
        }

        //Creacion de una vista para vwTipoUsuario
        public DataSet vwMateria()
        {
            // Crear el comando SQL
            string cadSQL = "";
            cadSQL = "select * from vwMateria;";

            // Configuracion de objetos de conexion
            MySqlConnection cnn = new MySqlConnection(cadCnn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSQL, cadCnn); //da es por datadatos
            DataSet ds = new DataSet();
            //ejecucion y salida
            da.Fill(ds, "vwMateria"); // <----------------
            return ds;
            //ds es igual al dataset
        }

        public DataSet vwRptUsuarioFiltro(string nomFiltro) // ***********Checar en la BD **************
        {
            // Crear el comando SQL
            string cadSQL = "";
            cadSQL = "select * from vwRptUsuario where nombre like '%" + nomFiltro + "%' ;";

            // Configuracion de objetos de conexion
            MySqlConnection cnn = new MySqlConnection(cadCnn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSQL, cadCnn); //da es por datadatos
            DataSet ds = new DataSet();
            //ejecucion y salida
            da.Fill(ds, "vwRptUsuarioFiltro"); // <----------------
            return ds;
            //ds es igual al dataset
        }
    }
}