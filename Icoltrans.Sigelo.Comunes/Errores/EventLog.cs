using System;
using System.Globalization;
using Sys = System.Diagnostics;

namespace Icoltrans.Sigelo.Comunes
{
    /// <summary>
    /// Registra eventos en el Event Log del sistema. Posee un metodo para cada tipo de mensaje
    /// </summary>
    /// <remarks><code>
    /// *----------------------------------------------------------------------------------*
    /// * Copyright (C) 2008 Michael Guerrero Ltda, Todos los Derechos Reservados
    /// * http://www.michaelguerrero.com - mailto:gerencia@michaelguerrero.com
    /// *
    /// * Archivo:      EventLog.cs
    /// * Tipo:         Componente de soporte
    /// * Autor:        jguerrero
    /// * Fecha:        2006 Junio 11
    /// * Propósito:    Registra eventos en el Event Log del sistema. Posee un metodo para cada tipo de mensaje
    /// *----------------------------------------------------------------------------------*
    /// </code></remarks>
    public sealed class EventLog : IDisposable
    {
        #region Variables Privadas
        private readonly string fuente = "Sigelo";
        private string usuario = Comun.Usuario;
        private string appName = Comun.Aplicativo;
        #endregion

        #region Contructor Destructor
        /// <summary>
        /// Constructor con la sección del Event Log donde se desea escribir
        /// </summary>
        public EventLog()
        {
            InitialiceSource();
        }
        #region IDisposable Members
        /// <summary>
        /// Destructor de la clase
        /// </summary>
        public void Dispose()
        {
        }

        #endregion

        #endregion

        #region Funciones privadas
        private void InitialiceSource()
        {
            try
            {
                if (!Sys.EventLog.SourceExists(this.fuente))
                    Sys.EventLog.CreateEventSource(this.fuente, this.fuente);
            }
            catch (Exception exError)
            {
                throw new ErrorException("No se pudo crear la sección " + this.fuente + " en el Event Log",
                    "Debe terner permisos Administrativos ó crearlo previamente", exError);
            }
        }
        private void GrabarLog(string mensaje, Sys.EventLogEntryType type)
        {
            try
            {
                using (Sys.EventLog obLogEntry = new Sys.EventLog())
                {
                    obLogEntry.Source = this.fuente;
                    if (mensaje.Length >= short.MaxValue)
                        mensaje = mensaje.Remove(short.MaxValue - 1);

                    Random obRnd = new Random();
                    obLogEntry.WriteEntry(mensaje, type, obRnd.Next(65535));
                }
            }
            catch (Exception exError)
            {
                throw new ErrorException("No fue posible registrar el mensaje sobre el EventLog",
                    "El usuario que ejecuta la aplicación debe tener permiso para registrar Eventos en el Log", exError);
            }
        }
        private string CompletarMensaje(string mensaje)
        {
            return string.Format(CultureInfo.InvariantCulture,
                "\r\n Usuario: {0}" +
                "\r\n Aplicacion: {1}" +
                "\r\n Mensaje: {2}",
                usuario, appName, mensaje.Trim());
        }
        #endregion

        #region Metodos Publicos

        /// <summary>
        /// Regitra un evento como un error
        /// </summary>
        /// <param name="error">Excepcion que se desea registrar</param>
        public void LogError(Exception error)
        {
            GrabarLog(error.ToFormatString(), Sys.EventLogEntryType.Error);
        }

        /// <summary>
        /// Registra un evento como un Warning
        /// </summary>
        /// <param name="mensaje">Mensaje que se desea reportar</param>
        public void LogWarning(string mensaje)
        {
            if (mensaje == null) throw new ArgumentNullException("mensaje");
            if (mensaje.Length > 0)
                GrabarLog(CompletarMensaje(mensaje), Sys.EventLogEntryType.Warning);
        }

        /// <summary>
        /// Registra un evento como una Falla de Auditoria
        /// </summary>
        /// <param name="mensaje">Mensaje que se desea reportar</param>
        public void LogFailureAudit(string mensaje)
        {
            if (mensaje == null) throw new ArgumentNullException("mensaje");
            if (mensaje.Length > 0)
                GrabarLog(CompletarMensaje(mensaje), Sys.EventLogEntryType.FailureAudit);
        }

        /// <summary>
        /// Registra un evento como Informativo
        /// </summary>
        /// <param name="mensaje">Mensaje que se desea reportar</param>
        public void LogInformation(string mensaje)
        {
            if (mensaje == null) throw new ArgumentNullException("mensaje");
            if (mensaje.Length > 0)
                GrabarLog(CompletarMensaje(mensaje), Sys.EventLogEntryType.Information);
        }

        /// <summary>
        /// Registra un evento como una operación Exitosa de auditoria
        /// </summary>
        /// <param name="mensaje">Mensaje que se desea reportar</param>
        public void LogSuccessAudit(string mensaje)
        {
            if (mensaje == null) throw new ArgumentNullException("mensaje");
            if (mensaje.Length > 0)
                GrabarLog(CompletarMensaje(mensaje), Sys.EventLogEntryType.SuccessAudit);
        }
        #endregion
    }
}
