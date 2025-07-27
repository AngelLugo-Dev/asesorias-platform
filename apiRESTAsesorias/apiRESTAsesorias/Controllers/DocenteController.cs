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
    public class DocenteController : ApiController
    {
        [HttpPost]
        [Route("full/usuario/spinsdocente")]
        public clsApiStatus spInsDocente([FromBody] clsDocente modelo)
        {
            clsApiStatus objRespuesta = new clsApiStatus();
            JObject jsonResp = new JObject();
            try
            {
                // Creacion del objeto usuario para la insercion 
                clsDocente objUsuario = new clsDocente(modelo.idDocente,
                                                       modelo.Nombre,
                                                       modelo.paterno,
                                                       modelo.materno,
                                                       modelo.usuario,
                                                       modelo.contrasenia);
                DataSet ds = new DataSet();
                //Ejecucion del metodo de insercion y recepion de resultados
                ds = objUsuario.spInsDocente();
                //Configuracion de los atributos de salida
                objRespuesta.statusExec = true;
                objRespuesta.msg = "Registro de Docente exitoso (proyecto_integrador)";
                objRespuesta.ban = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                jsonResp.Add("msgData", "Registro de Docente exitoso (proyecto_integrador)");
                objRespuesta.datos = jsonResp;

            }
            catch (Exception ex)
            {
                objRespuesta.statusExec = false;
                objRespuesta.msg = "Registro de Docente fallido (proyecto_integrador)";
                objRespuesta.ban = -1;
                jsonResp.Add("msgData", ex.Message.ToString());
                objRespuesta.datos = jsonResp;
            }
            return objRespuesta;
        }

        [HttpPost]
        [Route("full/usuario/spvalidaraccesodocente")]
        public DataSet spValidarAccesoDocente([FromBody] clsDocente modelo)
        {
            DataSet ds = new DataSet();
            try
            {
                // Creacion del objeto usuario para la insercion 
                clsDocente objUsuario = new clsDocente(modelo.usuario,
                                                       modelo.contrasenia);

                //Ejecucion del metodo de insercion y recepion de resultados
                ds = objUsuario.spValidarAccesoDocente();
            }
            catch (Exception ex)
            {
                //Configurar el DataSet para salida
                //(Formateo con clsApiStatus
                DataTable dt = new DataTable("spValidarAccesoDocente");
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

        // vwRptUsuario
        [HttpGet] //estas son directivas
        [Route("full/usuario/vwasesoria")]
        public DataSet vwAsesoria()
        {
            DataSet ds = new DataSet();
            try
            {
                // Creacion del objeto usuario para la insercion 
                clsDocente objUsuario = new clsDocente();

                //Ejecucion del metodo de insercion y recepion de resultados
                ds = objUsuario.vwAsesoria();
            }
            catch (Exception ex)
            {
                //Configurar el DataSet para salida
                //(Formateo con clsApiStatus
                DataTable dt = new DataTable("vwAsesoria");
                dt.Columns.Add("statusExec");
                dt.Columns.Add("msg");
                dt.Columns.Add("ban");
                dt.Columns.Add("msgData");
                dt.Columns.Add("msgException");
                //Formateo de los datos de salida
                DataRow dr = dt.NewRow();
                dr["statusExec"] = "false";
                dr["msg"] = "Error en reporte de ususaios, verificar ...";
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
