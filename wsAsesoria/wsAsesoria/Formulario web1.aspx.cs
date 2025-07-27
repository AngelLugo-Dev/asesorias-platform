using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wsAsesoria
{
    public partial class Formulario_web1 : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            // Configurar evento PageIndexChanging en gridview1
            GridView1.PageIndexChanging += GridView1_PageIndexChanging;
            await cargaDatosRptUsuario();
        }

        private void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //Actualizar el indice de paginacion
            GridView1.PageIndex = e.NewPageIndex;
            //Actualizar los datos del gridview1
            GridView1.DataBind();
        }

        private async Task cargaDatosRptUsuario()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // configuracion de la url del enpoint
                    string apiUrl = "https://localhost:44355/full/usuario/vwasesoria";

                    //ejecucion de la peticion http
                    HttpResponseMessage respuesta =
                        await client.GetAsync(apiUrl);

                    //validacion de la respuesta recibidad
                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado =
                            await respuesta.Content.ReadAsStringAsync();
                        DataSet dsRespuesta = new DataSet();
                        dsRespuesta = JsonConvert.DeserializeObject<DataSet>(resultado);
                        // Llenado del Gridview con los datos del DataSet
                        GridView1.DataSource = dsRespuesta.Tables[0];
                        GridView1.DataBind();
                    }
                    else
                    {
                        // Ejecucion no exitosa del endpoint 
                        Response.Write("<script language='javascript'>" +
                            "alert('Error de conexión con el servicio');" +
                            "</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>" +
                            "alert('Error inesperado en la aplicacion');" +
                            "</script>");
            }

        }
    }
}