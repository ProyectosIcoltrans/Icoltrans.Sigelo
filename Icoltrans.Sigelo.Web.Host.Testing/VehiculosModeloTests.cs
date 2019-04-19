using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Icoltrans.Sigelo.Entity.Vehiculos;

namespace Icoltrans.Sigelo.Web.Host.Testing
{
    [TestClass]
    public class VehiculosModeloTests
    {
        [TestMethod]
        public void ObtenerSeguimientosTest()
        {
            ServicioVehiculos client = new ServicioVehiculos();

            var seguimientos = client.ObtenerSeguimientos(null, null, null);

            Assert.IsTrue(seguimientos.Length > 0);

            var nacional = client.ObtenerSeguimientos(TipoCaravana.Nacional, null, null);

            Assert.IsTrue(nacional.Length > 0);

            var regional = client.ObtenerSeguimientos(TipoCaravana.Regional, null, null);

            Assert.IsTrue(regional.Length > 0);

            var urbana = client.ObtenerSeguimientos(TipoCaravana.Urbana, null, null);

            Assert.IsTrue(urbana.Length > 0);//-2147483598
        }

        [TestMethod]
        public void ObtenerObtenerReportesYEstadosTest()
        {
            //ServicioVehiculos client = new ServicioVehiculos();
            int i = 0;
            for (i = 0; i < 1000; i++)
            {
                //ServicioVehiculos client = new ServicioVehiculos();
                //var seguimientos = client.ObtenerReportesYEstados(-2147483598);
                ControlSeguimientoWS.ServicioVehiculosClient client
                   = new ControlSeguimientoWS.ServicioVehiculosClient();
                client.Open();
                var seguimientos = client.ObtenerReportesYEstados(-2147483598);
                client.Close();
            }
            Assert.IsTrue(i == 1000);
        }
    }
}
