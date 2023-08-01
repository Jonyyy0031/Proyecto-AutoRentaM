using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_AutoRenta.Entities
{
    public class AtenCliente
    {
        [Key]
        public int IDTicket { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string Descripcion { get; set; }
        public string Estatus { get; set; }
    }
}
