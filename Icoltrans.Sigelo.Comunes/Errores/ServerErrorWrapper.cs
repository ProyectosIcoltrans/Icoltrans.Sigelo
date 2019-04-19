using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Globalization;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Icoltrans.Sigelo.Comunes
{
    /// <summary>
    /// Clase para generar y transportar los errores desde cualquiera de las capas de software
    /// </summary>
    /// <remarks><code>
    /// *-----------------------------------------------------------------------------------------*
    /// * Copyright (C) 2008 Michael Guerrero Ltda, Todos los Derechos Reservados
    /// * http://www.michaelguerrero.com - mailto:gerencia@michaelguerrero.com
    /// *
    /// * Archivo:      ServerErrorWrapper.cs
    /// * Tipo:         Componente de soporte
    /// * Autor:        Jaimir Guerrero
    /// * Fecha:        2008 Mar 03
    /// * Propósito:    Clase para generar y transportar los errores desde cualquiera de las capas de software
    /// *-----------------------------------------------------------------------------------------*
    /// </code></remarks>
    [DataContract]
    public sealed class ServerErrorWrapper
    {
        #region Propiedades
        /// <summary>
        /// Aplicativo que generó la excepción.
        /// </summary>
        [DataMember]
        public string Aplicativo { get; private set; }
        /// <summary>
        /// Datos adicionales del Error
        /// </summary>
        [DataMember]
        public string Data { get; private set; }
        /// <summary>
        /// Excepción interna
        /// </summary>
        [DataMember]
        public ServerErrorWrapper InnerServerError { get; private set; }
        /// <summary>
        /// Colección de mensajes generados por el servidor de base de datos.  Solo para SQL Server
        /// </summary>
        [DataMember]
        public Collection<string> MensajesSql { get; private set; }
        /// <summary>
        /// Mensaje descriptivo del Error
        /// </summary>
        [DataMember]
        public string Message { get; private set; }
        /// <summary>
        /// Entrega la solucion documentada para el error
        /// </summary>
        [DataMember]
        public string Solucion { get; private set; }
        /// <summary>
        ///  Fuente del Error
        /// </summary>
        [DataMember]
        public string Source { get; private set; }
        /// <summary>
        /// Pila de llamadas durante la generación del error
        /// </summary>
        [DataMember]
        public string StackTrace { get; private set; }
        /// <summary>
        /// Metodo donde se generó el error
        /// </summary>
        [DataMember]
        public string Target { get; private set; }
        /// <summary>
        /// Data Type del Error Interno
        /// </summary>
        [DataMember]
        public string Tipo { get; private set; }
        /// <summary>
        /// Usuario que generó la excepción
        /// </summary>
        [DataMember]
        public string Usuario { get; private set; }
        /// <summary>
        /// Ip de la maquina donde se generó el error
        /// </summary>
        [DataMember]
        public string IPMaquina { get; private set; }
        #endregion

        #region Constructores
        /// <summary>
        /// Constructor para la excepción original
        /// </summary>
        /// <param name="innerException">Excepcion que se desea encapsular</param>
        public ServerErrorWrapper(Exception innerException)
        {
            this.Solucion = "Revise el detalle y comuniquese con su soporte t\u00E9cnico";
            this.Usuario = Comun.Usuario;
            this.Aplicativo = Comun.Aplicativo;
            this.IPMaquina = Comun.IPMaquina;
            this.MensajesSql = new Collection<string>();
            GenerarPropiedades(innerException);
        }
        /// <summary>
        /// Constructor para el mensaje y la excepción original Asigna la propiedad base InnerExcption
        /// </summary>
        /// <param name="mensaje">Mensaje de error</param>
        /// <param name="innerException">Excepcion que se desea encapsular</param>
        public ServerErrorWrapper(string mensaje, Exception innerException)
        {
            this.Solucion = "Revise el detalle y comuniquese con su soporte t\u00E9cnico";
            this.Usuario = Comun.Usuario;
            this.Aplicativo = Comun.Aplicativo;
            this.IPMaquina = Comun.IPMaquina;
            this.MensajesSql = new Collection<string>();
            GenerarPropiedades(innerException);
            this.Message = mensaje;
        }
        /// <summary>
        /// Constructor para el mensaje, la solución y la excepción original Asigna la propiedad base InnerExcption
        /// </summary>
        /// <param name="mensaje">Mensaje de error</param>
        /// <param name="solucion">Descripción de la solución</param>
        /// <param name="innerException">Excepcion que se desea encapsular</param>
        public ServerErrorWrapper(string mensaje, string solucion, Exception innerException)
        {
            this.Solucion = "Revise el detalle y comuniquese con su soporte t\u00E9cnico";
            this.Usuario = Comun.Usuario;
            this.Aplicativo = Comun.Aplicativo;
            this.MensajesSql = new Collection<string>();
            this.IPMaquina = Comun.IPMaquina;
            GenerarPropiedades(innerException);
            this.Message = mensaje;
            this.Solucion = solucion;
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Copia la informacion del origen en la instancia actual.
        /// </summary>
        /// <param name="origen">Objeto instanciado de Origen</param>
        public void Copy(ServerErrorWrapper origen)
        {
            if (origen == null) throw new ArgumentNullException("origen");
            this.Solucion = origen.Solucion;
            this.Usuario = origen.Usuario;
            this.Aplicativo = origen.Aplicativo;
            this.Message = origen.Message;
            this.Tipo = origen.Tipo;
            this.Data = origen.Data;
            this.IPMaquina = origen.IPMaquina;
            this.Source = origen.Source;
            this.StackTrace = origen.StackTrace;
            this.Target = origen.Target;
            this.MensajesSql = origen.MensajesSql;
            if (origen.InnerServerError != null)
                this.InnerServerError = origen.InnerServerError.MemberwiseClone() as ServerErrorWrapper;
        }
        /// <summary>
        /// Convirte esta excepcion a una tabla Html
        /// </summary>
        /// <returns>cadena formateada en Html</returns>
        public string ToHtml()
        {
            StringBuilder stringBuilder1 = new StringBuilder();
            ServerErrorWrapper serverErrorWrapper = this;
            stringBuilder1.Append("<TABLE cellSpacing='0' cellPadding='0' border='0'>");
            while (serverErrorWrapper != null)
            {
                object[] objArr = new object[] {
                                                 serverErrorWrapper.Message, 
                                                 serverErrorWrapper.Tipo, 
                                                 serverErrorWrapper.Usuario,
                                                 serverErrorWrapper.IPMaquina,
                                                 serverErrorWrapper.Aplicativo, 
                                                 serverErrorWrapper.Source, 
                                                 serverErrorWrapper.Target, 
                                                 serverErrorWrapper.Data, 
                                                 serverErrorWrapper.Solucion, 
                                                 serverErrorWrapper.StackTrace };
                stringBuilder1.AppendFormat(CultureInfo.InvariantCulture,
                    "<TR><TD colspan='3'><b>{0}</b></TD></TR><TR><TD>&nbsp;&nbsp;</TD><TD>Clase:</TD><TD>{1}</TD></TR><TR><TD>&nbsp;&nbsp;</TD><TD>Usuario:</TD><TD>{2}</TD></TR><TR><TD>&nbsp;&nbsp;</TD><TD>IP Origen:</TD><TD>{3}</TD></TR><TR><TD>&nbsp;&nbsp;</TD><TD>Aplicacion:</TD><TD>{4}</TD></TR><TR><TD>&nbsp;&nbsp;</TD><TD>Origen:</TD><TD>{5}</TD></TR><TR><TD>&nbsp;&nbsp;</TD><TD>Método:</TD><TD>{6}</TD></TR>zzzz<TR><TD>&nbsp;&nbsp;</TD><TD>Complementos:</TD><TD>{7}</TD></TR><TR><TD>&nbsp;&nbsp;</TD><TD>Solucion:</TD><TD>{8}</TD></TR><TR><TD>&nbsp;&nbsp;</TD><TD colspan='2'>Cola:</TD></TR><TR><TD>&nbsp;&nbsp;</TD><TD colspan='2'>{9}</TD></TR>",
                    objArr);
                if (serverErrorWrapper.MensajesSql.Count > 0)
                {
                    StringBuilder stringBuilder2 = new StringBuilder();
                    bool flag = true;
                    using (IEnumerator<string> ienumerator = serverErrorWrapper.MensajesSql.GetEnumerator())
                    {
                        while (ienumerator.MoveNext())
                        {
                            string s = ienumerator.Current;
                            if (flag)
                            {
                                flag = false;
                                stringBuilder2.AppendFormat("<TR><TD>&nbsp;&nbsp;</TD><TD>Mensajes SQL:</TD><TD>{0}</TD></TR>", s);
                            }
                            else
                            {
                                stringBuilder2.AppendFormat("<TR><TD>&nbsp;&nbsp;</TD><TD></TD>&nbsp;<TD>{0}</TD></TR>", s);
                            }
                        }
                    }
                    stringBuilder1.Replace("zzzz", stringBuilder2.ToString());
                }
                else
                {
                    stringBuilder1.Replace("zzzz", String.Empty);
                }
                serverErrorWrapper = serverErrorWrapper.InnerServerError;
            }
            stringBuilder1.Append("</TABLE>");
            stringBuilder1.Replace(Environment.NewLine, String.Empty);
            return stringBuilder1.ToString();
        }
        /// <summary>
        /// convierte esta excepcion en una cadena formateada.
        /// </summary>
        /// <returns>Cadena formateada</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder1 = new StringBuilder();
            for (ServerErrorWrapper serverErrorWrapper = this; serverErrorWrapper != null; serverErrorWrapper = serverErrorWrapper.InnerServerError)
            {
                object[] objArr = new object[] {
                                                 serverErrorWrapper.Message, 
                                                 serverErrorWrapper.Tipo, 
                                                 serverErrorWrapper.Usuario,
                                                 serverErrorWrapper.IPMaquina,
                                                 serverErrorWrapper.Aplicativo, 
                                                 serverErrorWrapper.Source, 
                                                 serverErrorWrapper.Target, 
                                                 serverErrorWrapper.Data, 
                                                 serverErrorWrapper.Solucion, 
                                                 serverErrorWrapper.StackTrace };
                stringBuilder1.AppendFormat(CultureInfo.InvariantCulture,
                    "{0}\r\n  Clase:        {1}\r\n  Usuario:      {2}\r\n  IP Origen:    {3}\r\n  Aplicacion:   {4}\r\n  Origen:       {5}\r\n  M\u00E9todo:       {6}zzzz\r\n  Complementos: {7}\r\n  Solucion:     {8}\r\n  Cola:\r\n{9}\r\n\t------------------ // ------------------\r\n", objArr);
                if (serverErrorWrapper.MensajesSql.Count > 0)
                {
                    StringBuilder stringBuilder2 = new StringBuilder();
                    bool flag = true;
                    using (IEnumerator<string> ienumerator = serverErrorWrapper.MensajesSql.GetEnumerator())
                    {
                        while (ienumerator.MoveNext())
                        {
                            string s = ienumerator.Current;
                            if (flag)
                            {
                                flag = false;
                                stringBuilder2.AppendFormat("\r\n  Mensajes SQL: {0}", s);
                            }
                            else
                            {
                                stringBuilder2.AppendFormat("\r\n                {0}", s);
                            }
                        }
                    }
                    stringBuilder1.Replace("zzzz", stringBuilder2.ToString());
                }
                else
                {
                    stringBuilder1.Replace("zzzz", String.Empty);
                }
            }
            return stringBuilder1.ToString();
        }
        #endregion

        #region Funciones
        private void GenerarPropiedades(Exception error)
        {
            this.Message = error.Message;
            this.Tipo = error.GetType().ToString();
            this.Target = Comun.EvaluarTarget(error);
            this.StackTrace = error.StackTrace;
            this.Data = Comun.EvaluarData(error);
            this.Source = error.Source;
            if (error.InnerException != null)
                this.InnerServerError = new ServerErrorWrapper(error.InnerException);

            FaultException<ServerErrorWrapper> faultException = error as FaultException<ServerErrorWrapper>;
            if (faultException != null)
            {
                Copy(faultException.Detail);
                return;
            }
            ErrorException errorException = error as ErrorException;
            if (errorException != null)
            {
                this.Usuario = errorException.Usuario;
                this.Aplicativo = errorException.Aplicativo;
                this.Solucion = errorException.Solucion;
                this.IPMaquina = errorException.IPMaquina;
                return;
            }
            SqlException sqlException = error as SqlException;
            if ((sqlException != null) && (sqlException.Errors.Count > 0))
            {
                foreach (SqlError sqlError in sqlException.Errors)
                {
                    object[] objArr = new object[] {
                                                                 sqlError.Procedure, 
                                                                 sqlError.Number, 
                                                                 sqlError.Class, 
                                                                 sqlError.State, 
                                                                 sqlError.LineNumber, 
                                                                 sqlError.Message };
                    this.MensajesSql.Add(String.Format(CultureInfo.InvariantCulture,
                        "{0}: Msg {1}, Level {2}, State {3}, Line {4} {5}", objArr));
                }
            }
        }
        #endregion
    }
}