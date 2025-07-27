using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//--------------------
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using apiRESTAsesorias.Models;
using MySql.Data.MySqlClient;
//----------------

namespace apiRESTAsesorias.Controllers
{
    public class AdminBdController : ApiController
    {
        private string cadCnn = ConfigurationManager.
           ConnectionStrings["bdProyectoIntegrador"].
           ConnectionString;

        //Endpoint para validacion de conexion (Mysql)
        [HttpGet]
        [Route("full/adminbd/checkmysqlconnection")]
        public clsApiStatus checkMySqlConnection()
        {
            // -----------------------------------------
            clsApiStatus objRespuesta = new clsApiStatus();
            JObject jsonResp = new JObject();
            // ----------------------------
            MySqlConnection cnn = new MySqlConnection(cadCnn);
            // Hacer prueba de conexion
            try
            {
                cnn.Open();
                cnn.Close();
                //-------------------------------------
                //configurar objeto de salida
                objRespuesta.statusExec = true;
                objRespuesta.msg = "Conexion exitosa (MySql) - control_acceso";
                objRespuesta.ban = 1;
                jsonResp.Add("msgData", "Conexion exitosa (MySql)-control_acceso");
                objRespuesta.datos = jsonResp;

            }
            catch (Exception ex)
            {
                //configurar objeto de salida
                objRespuesta.statusExec = false;
                objRespuesta.msg = "Cnexion fallida (MySql) - control_acceso";
                objRespuesta.ban = 0;
                jsonResp.Add("msgData", ex.Message.ToString());
                jsonResp.Add("msgList", ex.InnerException.ToString());
                objRespuesta.datos = jsonResp;
            }
            // Salida del objeto tipo clsStatus
            return objRespuesta;
        }
    }
}
