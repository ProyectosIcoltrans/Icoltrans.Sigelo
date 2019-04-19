using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;

namespace Icoltrans.Sigelo.Comunes
{
    /// <summary>
    /// Extensiones para tipos de datos existentes
    /// </summary>
    /// <remarks><code>
    /// *-----------------------------------------------------------------------------------------*
    /// * Copyright (C) 2008 Michael Guerrero Ltda, Todos los Derechos Reservados
    /// * http://www.michaelguerrero.com - mailto:gerencia@michaelguerrero.com
    /// *
    /// * Archivo:      Extension.cs
    /// * Tipo:         Componente de Apoyo
    /// * Autor:        Jaimir Guerrero
    /// * Fecha:        2008 Ago 04
    /// * Propósito:    Extensiones para tipos de datos existentes
    /// *-----------------------------------------------------------------------------------------*
    /// </code></remarks>
    public static class Extension
    {
        #region Linq
        /// <summary>
        /// Toma una colección que contiene sub colecciones del mismo tipo y retorna todos miembros en diferentes niveles a un unico nivel.
        /// </summary>
        /// <typeparam name="T">Tipo de la entidad de negocio contenida en la colección.</typeparam>
        /// <param name="fuente">Colección inicial.  Extensión de la interfaz <see cref="IEnumerable{T}"/></param>
        /// <param name="subColeccion">Delegado para indicar la porpiedad que contiene la colección de subelementos.</param>
        /// <returns><see cref="IEnumerable{T}"/> con todos los elementos en un solo nivel</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static IEnumerable<T> Aplanar<T>(this IEnumerable<T> fuente, Func<T, IEnumerable<T>> subColeccion)
        {
            foreach (T value in fuente)
            {
                yield return value;
                foreach (T child in subColeccion(value).Aplanar<T>(subColeccion))
                    yield return child;
            }
        }
        #endregion

        #region Object
        /// <summary>
        /// 	<para>Extension a la clase Object</para>
        /// 	<para>Determina sí el contenido es un número</para>
        /// </summary>
        /// <param name="valor">valor a evaluar</param>
        /// <returns>
        /// Verdadero si el contenido se puede convertir a un número
        /// </returns>
        public static bool IsNumeric(this Object valor)
        {
            if (valor == null)
                return false;
            else
            {
                decimal outValue = 0;
                return decimal.TryParse(valor.ToString(), out outValue);
            }
        }
        #endregion

        #region Exception
        /// <summary>
        /// 	<para>Extension a la clase Exception.</para>
        /// 	<para>Convierte una Excepción a una tabla HTML.</para>
        /// </summary>
        /// <param name="valor">valor a evaluar</param>
        /// <returns></returns>
        public static string ToHtmlTable(this Exception valor)
        {
            if (valor == null)
                return null;
            else
            {
                ServerErrorWrapper obWrapper = new ServerErrorWrapper(valor);
                return obWrapper.ToHtml();
            }
        }
        /// <summary>
        /// 	<para>Extension a la clase Exception</para>
        /// 	<para>Formatea una excepcion para mostrarla como string</para>
        /// </summary>
        /// <param name="valor">valor a evaluar</param>
        /// <returns></returns>
        public static string ToFormatString(this Exception valor)
        {
            if (valor == null)
                return null;
            else
            {
                ServerErrorWrapper obWrapper = new ServerErrorWrapper(valor);
                return obWrapper.ToString();
            }
        }
        #endregion

        #region DataSet
        /// <summary>
        /// Convierte un Dataset en su Representación XML
        /// </summary>
        /// <param name="paso">DataSet a Convertir</param>
        /// <returns>Representación XML con los datos contenidos en el DataSet.</returns>
        public static string DataToString(this DataSet paso)
        {
            if (paso == null) throw new ArgumentNullException("paso");

            if (paso.Tables.Count > 0)
            {
                using (StringWriter obWriter = new StringWriter(CultureInfo.InvariantCulture))
                {
                    paso.WriteXml(obWriter, XmlWriteMode.DiffGram);
                    return obWriter.ToString();
                }
            }
            return null;
        }
        /// <summary>
        /// Convierte un Dataset en su Representación XML
        /// </summary>
        /// <param name="paso">DataSet a Convertir</param>
        /// <returns>Representación XLS con el esquema contenido en el DataSet.</returns>
        public static string SchemaToString(this DataSet paso)
        {
            if (paso == null) throw new ArgumentNullException("paso");

            if (paso.Tables.Count > 0)
            {
                using (StringWriter obWriter = new StringWriter(CultureInfo.InvariantCulture))
                {
                    paso.WriteXmlSchema(obWriter);
                    return obWriter.ToString();
                }
            }
            return null;
        }
        #endregion
    }
}
