using Icoltrans.Sigelo.Entity.Vehiculos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icoltrans.Sigelo.Web.Host.Test
{
    [TestClass()]
    public class ServicioVehiculosTests
    {
        [TestMethod()]
        public void ObtenerListaRutasTest()
        {
            ServicioVehiculos instancia = new ServicioVehiculos();
            var result = instancia.ObtenerListaRutas(TipoCaravana.Nacional);
            Assert.IsNotNull(result);
        }
    }
}
