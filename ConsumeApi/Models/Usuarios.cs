using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumeApi.Models
{
    public class Usuarios
    {
        public int int_id { get; set; }
        public string str_status { get; set; }
        public DateTime dat_fecha { get; set; }
        public string str_nombre { get; set; }
        public string str_direccion { get; set; }
        public string str_departamento { get; set; }
        public int int_telefono { get; set; }
    }
}