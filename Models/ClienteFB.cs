using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseDatos2crudFirebase.Models
{
    public class ClienteFB
    {

        public string IdClienteFB { get; set; }
        public string Nombre { get; set; }
        public string TipoCliente { get; set; }
        public string EstadoCivil { get; set; }
        public string Genero { get; set; }
        public string Direcion { get; set; }

       
    }
}