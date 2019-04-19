using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icoltrans.Sigelo.Win.Vehiculos.Code
{

    public class BuscarPlacaMapaEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        public BuscarPlacaMapaEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos.FuncionEscolta[] Result
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return ((Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos.FuncionEscolta[])(this.results[0]));
            }
        }
    }
}
