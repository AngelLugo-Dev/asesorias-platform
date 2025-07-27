using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//----------------------------------
using System.Net.Http;// peticiones para el postman
using System.Threading.Tasks; // hilos, para que se ejecute de manera asincrona
using System.Text;// usar metodos para manipular cadenas, para formatear el texto
// las primeras 3 son para la manipulacion
using Newtonsoft.Json;// manejo de objetos json
using Newtonsoft.Json.Linq;
using System.Data;// manejador de datos
//----------------------------------

namespace wsAsesoria
{
    public partial class wpAccesoEstudiante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            await cargarDatosApi();
        }

        // Método para ejecucion de endpoint (webApi)
        private async Task cargarDatosApi()
        {
            try
            {
                // Monitorear la configuracion del objeto tipo
                //httprequest
                using (HttpClient client = new HttpClient())
                {
                    // Configuración del Json que se enviará
                    String data = @"{
                                    ""usuario"":""" + TextBox1.Text + "\"," +
                                    "\"contrasenia\":\"" + TextBox2.Text + "\"" +
                                "}";
                    //Configuración del contenido del <body> que se enviará
                    HttpContent contenido = new StringContent(data, Encoding.UTF8, "application/json");
                    // Ejecucion del httpReques
                    string apiURL = "https://localhost:44355/full/usuario/spvalidaraccesoestudiante";
                    HttpResponseMessage respuesta = await client.PostAsync(apiURL, contenido);
                    // validacion de la respuesta recibida
                    if (respuesta.IsSuccessStatusCode)
                    {
                        //Estatus 200-OK
                        // Extraer los datos recibidos (DataSet)
                        string resultado = await respuesta.Content.ReadAsStringAsync();
                        DataSet ds = new DataSet();
                        ds = JsonConvert.DeserializeObject<DataSet>(resultado);
                        // Envío de datos de salida (Pagina Acceso)
                        Response.Write(ds.Tables[0].Rows[0][0].ToString());
                        // Validacion de bandera para actualizar la sesion
                        string ban = ds.Tables[0].Rows[0][0].ToString();
                        if (ban == "1")
                        {
                            Session["estNombre"] = ds.Tables[0].Rows[0][1].ToString();
                            Session["estSemestre"] = ds.Tables[0].Rows[0][2].ToString();
                            Session["estCarrera"] = ds.Tables[0].Rows[0][3].ToString();
                            Session["estUsuario"] = ds.Tables[0].Rows[0][4].ToString();
                            Response.Write("<script language = 'javascript'>" +
                                "alert('Bienvenido(a): " + Session["estNombre"] + "');" +
                                "</script>");

                            Response.Write("<script language = 'javascript'>" +
                                "document.location.href = 'Formulario web1.aspx';" +
                                "</script>");
                        }
                        else
                        {
                            //usuario no valido
                            //se reinicia por seguridad, se borra
                            Session["docNombre"] = "";
                            Session["docUsuario"] = "";
                            Session["estNombre"] = "";
                            Session["estSemestre"] = "";
                            Session["estCarrera"] = "";
                            Session["estUsuario"] = "";
                            Response.Write("<script language = 'javascript'>" +
                                "alert('Acceso denegado, validar sus datos...');" +
                                "</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script languge = 'javscript'>" + "alert('Conexion Fallida con el servicio');" + "</script>");

                    }
                }
            }
            catch (Exception ex)
            {
                // Falla General (Interna / Externa)
                Response.Write("<script languge = 'javscript'>" + "alert('Error general del servicio, contactar a su administrador');" + "</script>");
            }


        }
    }
}