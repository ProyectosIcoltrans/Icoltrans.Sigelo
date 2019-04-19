using System;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

 namespace Icoltrans.Sigelo.Comunes

{
    /// <summary>
    /// Clase estandar mara la generación (throw) de los errores
    /// </summary>
    /// <remarks><code>
    /// *-----------------------------------------------------------------------------------------*
    /// * Copyright (C) 2008 Michael Guerrero Ltda, Todos los Derechos Reservados
    /// * http://www.michaelguerrero.com - mailto:gerencia@michaelguerrero.com
    /// *
    /// * Archivo:      ErrorException.cs
    /// * Tipo:         Entidad de Apoyo
    /// * Autor:        Jaimir Guerrero
    /// * Fecha:        2008 Jun 23
    /// * Propósito:    Clase estandar mara la generación (throw) de los errores
    /// *-----------------------------------------------------------------------------------------*
    /// </code></remarks>
    [Serializable]
    public class ErrorException : Exception
    {
        #region Constructores
        /// <summary>
        /// La calse no se debe construir sin parametros iniciales
        /// </summary>
        public ErrorException() { }

        /// <summary>
        /// Constructor para el mensaje y la excepción original Asigna la propiedad base InnerExcption
        /// </summary>
        /// <param name="mensaje">Mensaje de error</param>
        /// <param name="innerException">Excepcion que se desea encapsular</param>
        public ErrorException(string mensaje, Exception innerException)
            : base(mensaje, innerException) { }

        /// <summary>
        /// Constructor para el mensaje
        /// </summary>
        /// <param name="mensaje">Mensaje de error</param>
        public ErrorException(string mensaje) : base(mensaje) { }

        /// <summary>
        /// Constructor para serialización
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ErrorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null) throw new ArgumentNullException("info");

            stSolucion = info.GetString("stSolucion");
            stUsuario = info.GetString("stUsuario");
            stAplicativo = info.GetString("stAplicativo");
            stIPMaquina = info.GetString("stIPMaquina");
        }

        /// <summary>
        /// Constructor para el mensaje, la solución y la excepción original Asigna la propiedad base InnerExcption
        /// </summary>
        /// <param name="mensaje">Mensaje de error</param>
        /// <param name="solucion">Descripción de la solución</param>
        /// <param name="innerException">Excepcion que se desea encapsular</param>
        public ErrorException(string mensaje, string solucion, Exception innerException)
            : base(mensaje, innerException)
        {
            stSolucion = solucion;
        }

        /// <summary>
        /// Constructor para el mensaje y la solución.
        /// </summary>
        /// <param name="mensaje">Mensaje de error</param>
        /// <param name="solucion">Descripción de la solución</param>
        public ErrorException(string mensaje, string solucion)
            : base(mensaje)
        {
            stSolucion = solucion;
        }
        #endregion

        #region Variables Privadas
        private string stSolucion = "Revise el detalle y comuniquese con su soporte técnico";
        private string stUsuario = Comun.Usuario;
        private string stAplicativo = Comun.Aplicativo;
        private string stIPMaquina = Comun.IPMaquina;
        #endregion

        #region Propiedades
        /// <summary>
        /// Entrega la solucion documentada para el error
        /// </summary>
        public string Solucion
        {
            get
            {
                return stSolucion;
            }
        }

        /// <summary>
        /// Usuario que generó la excepción
        /// </summary>
        public string Usuario
        {
            get { return stUsuario; }
        }

        /// <summary>
        /// Aplicativo que generó la excepción.
        /// </summary>
        public string Aplicativo
        {
            get { return stAplicativo; }
        }

        /// <summary>
        /// Ip del computador deonde se generó el error
        /// </summary>
        public string IPMaquina
        {
            get { return stIPMaquina; }
        }
        #endregion

        #region Metodos Publicos
        /// <summary>
        /// Recuperar la información de serialización
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityCritical]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");
            info.AddValue("stSolucion", stSolucion, typeof(string));
            info.AddValue("stUsuario", stUsuario, typeof(string));
            info.AddValue("stAplicativo", stAplicativo, typeof(string));
            info.AddValue("stIPMaquina", stIPMaquina, typeof(string));

            base.GetObjectData(info, context);
        }
        /// <summary>
        /// convierte esta excepcion en una cadena formateada.
        /// </summary>
        /// <returns>Cadena formateada</returns>
        public override string ToString()
        {
            return this.ToFormatString();
        }
        #endregion
    }
}
