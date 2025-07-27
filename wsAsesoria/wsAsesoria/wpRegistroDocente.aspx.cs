using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace wsAsesoria
{
    public partial class wpRegistroDocente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private async Task cargaDatos()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Configuración del Json que se enviará
                    String data = @"{
                                  ""idDocente"":""" + TextBox1.Text + "\"," +
                                  "\"nombre\":\"" + TextBox2.Text + "\"," +
                                  "\"paterno\":\"" + TextBox3.Text + "\"," +
                                  "\"materno\":\"" + TextBox4.Text + "\"," +
                                  "\"usuario\":\"" + TextBox5.Text + "\"," +
                                  "\"contrasenia\":\"" + TextBox6.Text + "\"" +
                                  "}";
                    // Configuración del contenido del <body> a enviar
                    HttpContent contenido = new StringContent
                                (data, Encoding.UTF8, "application/json");
                    // Ejecución de la petición HTTP
                    string apiUrl = "https://localhost:44355/full/usuario/spinsdocente";
                    // ----------------------------------------------
                    HttpResponseMessage respuesta =
                        await client.PostAsync(apiUrl, contenido);
                    // Validación de la respuesta recibida
                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado =
                                await respuesta.Content.ReadAsStringAsync();
                        JObject jRespuesta = new JObject();
                        jRespuesta = JsonConvert.DeserializeObject<JObject>(resultado);

                        Response.Write(jRespuesta["statusExec"].ToString());
                        Response.Write(jRespuesta["ban"].ToString());
                        // Mensaje de estatus del proceso
                        string ban = jRespuesta["ban"].ToString();
                        if (ban == "0")
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('Usuario registrado exitosamente');" +
                                           "</script>");
                            Response.Write("<script language='javascript'>" +
                                           "document.location.href='Formulario web1.aspx';" +
                                           "</script>");
                        }
                        if (ban == "1")
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('El nombre de usuario ya existe');" +
                                           "</script>");
                        }
                        if (ban == "2")
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('El usuario ya existe');" +
                                           "</script>");
                        }
                        if (ban == "3")
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('El tipo de usuario no existe');" +
                                           "</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>" +
                                       "alert('Error de conexión con el servicio');" +
                                       "</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>" +
                               "alert('Error de la aplicación, intentar nuevamente');" +
                               "</script>");
            }
        }


        protected async void Button1_Click(object sender, EventArgs e)
        {
            // Validación de datos de captura
            // Nombre
            if (TextBox1.Text == "")
            {
                Response.Write("<script language='javascript'>" +
                               "alert('El nombre no puede estar vacío');" +
                               "</script>");
            }
            else
            {
                // Apellido Paterno
                if (TextBox2.Text == "")
                {
                    Response.Write("<script language='javascript'>" +
                            "alert('El apellido paterno no puede estar vacío');" +
                            "</script>");
                }
                else
                {
                    // Apellido Materno
                    if (TextBox3.Text == "")
                    {
                        Response.Write("<script language='javascript'>" +
                                "alert('El apellido materno no puede estar vacío');" +
                                "</script>");
                    }
                    else
                    {
                        // Usuario
                        if (TextBox4.Text == "")
                        {
                            Response.Write("<script language='javascript'>" +
                                    "alert('El usuario no puede estar vacío');" +
                                    "</script>");
                        }
                        else
                        {
                            // Contraseña
                            if (TextBox6.Text == "")
                            {
                                Response.Write("<script language='javascript'>" +
                                        "alert('La contraseña no puede estar vacía');" +
                                        "</script>");
                            }
                             else
                             {
                              // Todo bien, ejecución del metodo
                              await cargaDatos();
                             }
                        
                        }
                    }
                }
            }
        }
    }
}