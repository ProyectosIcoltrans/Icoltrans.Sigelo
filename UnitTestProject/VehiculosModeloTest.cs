using System;
using Icoltrans.Sigelo.Data;
using Icoltrans.Sigelo.Entity.Vehiculos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Icoltrans.Sigelo.Web.Host;

namespace UnitTestProject
{
    [TestClass]
    public sealed class VehiculosModeloTest
    {
        [TestMethod]
        public void TestObtenerRutasTipo()
        {
            Ruta[] resp = null;
            using (VehiculosModelo modelo = new VehiculosModelo())
            {
                resp = modelo.ObtenerRutasTipo(TipoCaravana.Urbana);
            }
            Assert.IsTrue(resp != null && resp.Length > 0);
        }
        [TestMethod]
        public void TestObtenerVehiculosPorCaravana()
        {
            VehiculoCaravana[] resp = null;
            using (VehiculosModelo modelo = new VehiculosModelo())
                resp= modelo.ObtenerVehiculosPorCaravana(new Guid("3C31D685-AB1F-4048-A435-01829AC88E97"));

            Assert.IsNotNull(resp);
            Assert.AreNotEqual<int>(resp.Length, 0);
        }
        [TestMethod]
        public void TestObtenerSeguimientos()
        {
            ServicioVehiculos instancia = new ServicioVehiculos();

            Seguimiento[] seguimiento = instancia.ObtenerSeguimientos(null, null, null);

            instancia.ObtenerReportesYEstados(seguimiento[5].finIdRuta);

            //int a  = int.MaxValue;- 2147483599
            //VehiculoCaravana[] resp = null;
            //using (VehiculosModelo modelo = new VehiculosModelo())
            //    modelo.ObtenerSeguimientos(null, null, null);

            //Assert.IsNotNull(resp);
            //Assert.AreNotEqual<int>(resp.Length, 0);
        }
    }
}
