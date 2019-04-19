using Icoltrans.Sigelo.Rules.Vehiculos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject
{
    
    
    /// <summary>
    ///This is a test class for ControladorVehiculosTest and is intended
    ///to contain all ControladorVehiculosTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ControladorVehiculosTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for CambiarRutaCaravana
        ///</summary>
        [TestMethod()]
        public void CambiarRutaCaravanaTest()
        {
            ControladorVehiculos target = new ControladorVehiculos(); // TODO: Initialize to an appropriate value
            Guid idCaravana = new Guid("EE1BF5F7-5E53-4109-9ED3-EB34FEFA523E"); // TODO: Initialize to an appropriate value
            int idRuta = -2147483354; // TODO: Initialize to an appropriate value
            string descripcion ="Prueba de Jaimir Guerrero"; // TODO: Initialize to an appropriate value
            string ubicacion = "BOGOTA"; // TODO: Initialize to an appropriate value
            target.CambiarRutaCaravana(idCaravana, idRuta, descripcion, ubicacion);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
