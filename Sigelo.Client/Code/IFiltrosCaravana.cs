using System;
using System.Collections.Generic;
using Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    /// <summary>
    /// Interfaz para preservar los valor en la barra de filtros
    /// </summary>
    public interface IFiltrosCaravana
    {
        bool FiltroRojo { get; set; }
        bool FiltroAmarillo { get; set; }
        bool FiltroVerde { get; set; }
        bool FiltroNacional { get; set; }
        bool FiltroRegional { get; set; }
        bool FiltroUrbana { get; set; }
        bool FiltroMarcados { get; set; }
        CiudadRuta FiltroOrigen { get; set; }
        CiudadRuta FiltroDestino { get; set; }
        string PlacaMapa { get; set; }
        IEnumerable<Guid> Marcados { get;  }
    }
}
