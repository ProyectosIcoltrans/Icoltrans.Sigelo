using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icoltrans.Sigelo.Entity.Produccion.ComPlus
{
    public class PuntoEntregaCaravana
    {
        public bool Unificar { get; set; }
        public string fvcDescripcion { get; set; }
        public string fvcDireccion { get; set; }
        public string fvcTelefono { get; set; }
        public string fvcCodigoEDI { get; set; }
        public int fnuManifiesto { get; set; }
        public Guid fidPuntoEntrega { get; set; }
        public int fsmOrden { get; set; }
        public decimal frlKilos { get; set; }
        public int finUnidades { get; set; }
    }
}
