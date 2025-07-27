using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//----------
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using apiRESTAsesorias.Models;
//-------------

namespace apiRESTAsesorias.Controllers
{
    public class EstudianteController : ApiController
    {
        // GET: api/Estudiante
        [HttpPost]
        [Route("full/usuario/spinsestudiante")]
        public clsApiStatus spInsEstudiante([FromBody] clsEstudiante modelo)
        {
            clsApiStatus objRespuesta = new clsApiStatus();
            JObject jsonResp = new JObject();
            try
            {
                // Creacion del objeto usuario para la insercion 
                clsEstudiante objUsuario = new clsEstudiante(modelo.num_control,
                                                       modelo.nombre,
                                                       modelo.paterno,
                                                       modelo.materno,
                                                       modelo.semestre,
                                                       modelo.carrera,
                                                       modelo.usuario,
                                                       modelo.contrasenia);
                DataSet ds = new DataSet();
                //Ejecucion del metodo de insercion y recepion de resultados
                ds = objUsuario.spInsEstudiante();
                //Configuracion de los atributos de salida
                objRespuesta.statusExec = true;
                objRespuesta.msg = "Registro de Estudiante exitoso (proyecto_integrador)";
                objRespuesta.ban = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                jsonResp.Add("msgData", "Registro de Estudiante exitoso (proyecto_integrador)");
                objRespuesta.datos = jsonResp;

            }
            catch (Exception ex)
            {
                objRespuesta.statusExec = false;
                objRespuesta.msg = "Registro de Estudiante fallido (proyecto_integrador)";
                objRespuesta.ban = -1;
                jsonResp.Add("msgData", ex.Message.ToString());
                objRespuesta.datos = jsonResp;
            }
            return objRespuesta;
        }
        [HttpPost]
        [Route("full/usuario/spvalidaraccesoestudiante")]
        public DataSet spValidarAccesoEstudiante([FromBody] clsEstudiante modelo)
        {
            DataSet ds = new DataSet();
            try
            {
                // Creacion del objeto usuario para la insercion 
                clsEstudiante objUsuario = new clsEstudiante(modelo.usuario,
                                                       modelo.contrasenia);

                //Ejecucion del metodo de insercion y recepion de resultados
                ds = objUsuario.spValidarAccesoEstudiante();
            }
            catch (Exception ex)
            {
                //Configurar el DataSet para salida
                //(Formateo con clsApiStatus
                DataTable dt = new DataTable("spValidarAccesoEstudiante");
                dt.Columns.Add("statusExec");
                dt.Columns.Add("msg");
                dt.Columns.Add("ban");
                dt.Columns.Add("msgData");
                dt.Columns.Add("msgException");
                //Formateo de los datos de salida
                DataRow dr = dt.NewRow();
                dr["statusExec"] = "false";
                dr["msg"] = "Error en control de acceso, verificar ...";
                dr["ban"] = "-1";
                dr["msgData"] = ex.Message.ToString();
                dr["msgException"] = ex.InnerException.ToString();
                //asignar datos de salida
                dt.Rows.Add(dr);
                ds.Tables.Add(dt);

            }
            //Return del DataSet con los datos recibidos
            // (o formateados dentro del catch
            return ds;
        }
    }
}
