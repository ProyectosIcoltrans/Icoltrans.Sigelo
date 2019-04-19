using System;
using Icoltrans.Sigelo.Rules.ComProxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class VehiculosNovedadesTest
    {
        [TestMethod]
        public void ReporteCarreteraTest()
        {
            VehiculosNovedades instancia = new VehiculosNovedades();
            instancia.ReporteCarretera
                (
                new Guid("7B7EC4D2-D67E-4E4F-AEA3-3FD82D70EB6C"),
                0,
                DateTime.Now,
                1,
                1,
                "Carreta",
                "Aqui",
                new Guid[] 
                { 
                    new Guid("07D3A628-B037-4AFA-A4F2-DE10AF9793CB") ,
                    new Guid("354D028E-6416-496C-A437-B4D640805C1B") 
                }
                );
        }
    }
}
