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
    public class clsEstudiante
    {
        public int num_control { get; set; }
        public string nombre { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
        public int semestre { get; set; }
        public string carrera { get; set; }
        public string usuario { get; set; }
        public string contrasenia { get; set; }


    public clsEstudiante()
    {

    }

        public clsEstudiante (string usuario, string contrasenia)
        {
            this.usuario = usuario;
            this.contrasenia = contrasenia;
        }

        public clsEstudiante(int num_control, string nombre, string paterno, string materno, int semestre, string carrera, string usuario, string contrasenia)
        {
            this.num_control = num_control; 
            this.nombre = nombre;
            this.paterno = paterno;
            this.materno = materno;
            this.semestre = semestre;
            this.carrera = carrera;            
            this.usuario = usuario;
            this.contrasenia = contrasenia;
        }
           
        // atributos de ambiente
        private string cadCnn = ConfigurationManager.
            ConnectionStrings["bdProyectoIntegrador"].
            ConnectionString;
        // Metodos de procesos

        public DataSet spInsEstudiante()
        {
            // Crear el comando SQL

            string cadSQL = "";
            cadSQL = "CALL spInsEstudiante('" + this.num_control + "', '"
                                           + this.nombre + "', '"
                                           + this.paterno + "', '" 
                                           + this.materno + "',  '" 
                                           + this.semestre + "',  '" 
                                           + this.carrera + "', '" 
                                           + this.usuario + "', '" 
                                           + this.contrasenia + "');";

            // Configuracion de objetos de conexion
            MySqlConnection cnn = new MySqlConnection(cadCnn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSQL, cadCnn); //da es por datadatos
            DataSet ds = new DataSet();

            //ejecucion y salida
            da.Fill(ds, "spInsEstudiante"); // <----------------
            return ds;
        }

        public DataSet spValidarAccesoEstudiante()
        {
            // Crear el comando SQL
            string cadSQL = "";
            cadSQL = "CALL spValidarAccesoEstudiante('" + this.usuario + "', '"
                                           + this.contrasenia + "');";

            // Configuracion de objetos de conexion
            MySqlConnection cnn = new MySqlConnection(cadCnn);
            MySqlDataAdapter da = new MySqlDataAdapter(cadSQL, cadCnn); //da es por datadatos
            DataSet ds = new DataSet();
            //ejecucion y salida
            da.Fill(ds, "spValidarAccesoEstudiante"); // <----------------
            return ds;
            //ds es igual al dataset
        }
    }
}