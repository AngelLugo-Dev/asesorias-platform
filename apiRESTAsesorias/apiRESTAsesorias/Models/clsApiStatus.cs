﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// -----------------
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
// -----------

namespace apiRESTAsesorias.Models
{
    public class clsApiStatus
    {
        // Estado de ejecución del endpoint (método)
        public bool statusExec { get; set; }
        // Descripción del resultado
        public string msg { get; set; }
        // Código de ejecución del endpoint (método)
        public int ban { get; set; }
        // Objeto Json para envío de datos
        public JObject datos { get; set; }
    }
}