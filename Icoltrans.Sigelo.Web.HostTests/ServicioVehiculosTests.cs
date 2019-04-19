using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Icoltrans.Sigelo.Web.Host;

namespace Icoltrans.Sigelo.Web.Host.Test
{
    [TestClass()]
    public class ServicioVehiculosTests
    {
        [TestMethod()]
        public void ObtenerListaRutasTest()
        {
            ServicioVehiculos instacia = new ServicioVehiculos();
            instacia.ObtenerListaRutas(Tipo)
            Assert.Fail();
        }

      
    }
}
