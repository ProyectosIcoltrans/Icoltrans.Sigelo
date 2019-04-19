using System;
using Icoltrans.Sigelo.Data;
using Icoltrans.Sigelo.Entity.Maestros;

namespace Icoltrans.Sigelo.Rules.Maestros
{
    /// <summary>
    /// Controlador de reglas de datos para el módulo de Maestros
    /// </summary>
    /// <remarks><code>
    /// *----------------------------------------------------------------------------------*
    /// * Copyright (C) 2008 Michael Guerrero Ltda, Todos los Derechos Reservados
    /// * http://www.michaelguerrero.com - mailto:gerencia@michaelguerrero.com
    /// *
    /// * Archivo:      ControladorMaestros.cs
    /// * Tipo:         Regla de negocio
    /// * Autor:        jguerrero
    /// * Fecha:        2012 Feb 25
    /// * Propósito:    Controlador de reglas de datos para el módulo de Maestros
    /// *----------------------------------------------------------------------------------*
    /// </code></remarks>
    public sealed class ControladorMaestros : IDisposable
    {
        #region Variables
        private MaestrosModelo modelo;
        #endregion

        #region Constructor / Destructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ControladorMaestros"/> class.
        /// </summary>
        public ControladorMaestros()
        {
            this.modelo = new MaestrosModelo();
        }
        public void Dispose()
        {
            if (this.modelo != null)
                modelo.Dispose();
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Obtener los parametros necesario para la pantalla de control de seguimiento.
        /// </summary>
        /// <returns>Los parametros en un objeto de tipo <see cref="ParametrosPantalla"/></returns>
        public ParametrosPantalla ObtenerParametros()
        {
            return modelo.ObtenerParametros();
        }
        #endregion
    }
}
