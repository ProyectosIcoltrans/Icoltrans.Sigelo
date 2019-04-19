using Icoltrans.Sigelo.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public sealed  class NominaModeloTest
    {
        [TestMethod]
        public void TestObtenerInformacionUsuario()
        {
            string dato = "32";

            int cant = dato.Split('|').Length; 

            //using (NominaModelo instancia = new NominaModelo())
            //{
            //    var result = instancia.EmpleadosGrupoCargo(0, 0);
            //    Assert.IsNotNull(result);
            //}
        }
    }
}