using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_AutoRenta.Entities
{
    public class Lavadoo
    {
        [Key]
        public int PkLavado { get; set; }
        public string Estado { get; set; }

        [ForeignKey("Vehiculos")]
        public int FkVehiculos { get; set; }
        public Vehiculos Vehiculos { get; set; }

    }
}
