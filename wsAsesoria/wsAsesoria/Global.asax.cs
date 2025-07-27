using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace wsAsesoria
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Application["nomEmpresa"] = "ITP Gestioón de usuarios de APP";
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["docNombre"] = "";
            Session["docUsuario"] = "";
            Session["estNombre"] = "";
            Session["estSemestre"] = "";
            Session["estCarrera"] = "";
            Session["estUsuario"] = "";
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}