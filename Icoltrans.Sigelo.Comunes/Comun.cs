using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using Microsoft.CSharp;

namespace Icoltrans.Sigelo.Comunes
{
    /// <summary>
    /// *----------------------------------------------------------------------------------*
    /// * Copyright (C) 2006 Michael Guerrero Ltda, Todos los Derechos Reservados
    /// * http://www.michaelguerrero.com - mailto:gerencia@michaelguerrero.com
    /// *
    /// * Archivo:      Comun.cs
    /// * Tipo:         Componente estatico de soporte
    /// * Autor:        jguerrero
    /// * Fecha:        2006 Junio 11
    /// * Propósito:    Funciones compartidas genéricas de múltiple proposito
    /// * Cambios:
    /// *----------------------------------------------------------------------------------*
    /// </summary>
    public static class Comun
    {
        #region Variables
        private static readonly Dictionary<string, Type> TiposCopia = new Dictionary<string, Type>();
        #endregion

        #region Propiedades estáticos
        /// <summary>
        /// Nombre del Assembly dominio de la aplicación
        /// </summary>
        public static string Aplicativo
        {
            get
            {
                string stUrl = Assembly.GetExecutingAssembly().FullName;
                AppDomain obApp = Thread.GetDomain();
                if (obApp != null)
                    return string.Format(CultureInfo.InvariantCulture, "{0} = {1}", obApp.FriendlyName, stUrl);
                else
                    return stUrl;
            }
        }
        /// <summary>
        /// Usuario actual que ejecuta el aplicativo
        /// </summary>
        public static string Usuario
        {
            get
            {
                string usuario = string.Empty;
#if SILVERLIGHT
#else
                if (Thread.CurrentPrincipal != null && Thread.CurrentPrincipal.Identity != null)
                    usuario = Thread.CurrentPrincipal.Identity.Name;

                if (usuario.Length == 0)
                    usuario = WindowsIdentity.GetCurrent().Name;

                if (usuario.Length == 0)
                    usuario = Environment.UserDomainName + "\\" + Environment.UserName;
#endif
                return usuario;
            }
        }
        /// <summary>
        /// path del directorio seguro para salvar información referente a un usuario
        /// </summary>
        /// <returns>Nombre valido del directorio</returns>
        public static string DirectoryName
        {
            get
            {
                string stDirPath = string.Format(CultureInfo.InvariantCulture, "{0}\\MichaelGuerrero", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                if (!System.IO.Directory.Exists(stDirPath))
                    System.IO.Directory.CreateDirectory(stDirPath);

                return stDirPath;
            }
        }
        /// <summary>
        /// Obtiene el primer ip de la maquina actual
        /// </summary>
        public static string IPMaquina
        {
            get
            {
                string ipActual = "127.0.0.1";
#if SILVERLIGHT
#else
                var ipEntry = Dns.GetHostEntry(string.Empty);
                var addr = ipEntry.AddressList;
                switch (addr.Length)
                {
                    case 0:
                        ipActual = string.Empty;
                        break;
                    case 1:
                        ipActual = addr[0].ToString();
                        break;
                    default:
                        {
                            var res = from dir in addr
                                      where dir.AddressFamily == AddressFamily.InterNetwork
                                      select dir;
                            if (res.Count() > 0)
                            {
                                ipActual = res.AsQueryable().First().ToString();
                                break;
                            }
                            else
                                goto case 1;
                        }
                }
#endif
                return ipActual;
            }
        }
        /// <summary>
        /// Traer el nombre del directorio del Primer Assembly en la cadena de ejecución.
        /// </summary>
        /// <returns>Nombre del directorio</returns>
        public static string AssemblyDir
        {
            get { return (new FileInfo(Assembly.GetEntryAssembly().Location)).DirectoryName; }
        }
        /// <summary>
        /// Retorna el nombre del ejecutable que inicia la ejecución del programa
        /// </summary>
        public static string AssemblyFile
        {
            get { return Assembly.GetEntryAssembly().ManifestModule.Name; }
        }
        /// <summary>
        /// Obtiene el nombre de red de la máquina
        /// </summary>
        /// <value>Nombre de la maquina.</value>
        public static string NombreMaquina
        {
            get { return Dns.GetHostEntry(string.Empty).HostName; }
        }
        #endregion

        #region Metodos Estaticos
        /// <summary>
        /// Toma la instancia del entidad, la serializa en fomra Xml y retorna la cadena resultante.
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="entidad">La entidad.</param>
        /// <returns>string en formato xml con los datos de la entidad.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public static string SerializarXml<T>(T entidad) where T : new()
        {
            DataContractSerializer serializer = new DataContractSerializer(entidad.GetType());
            using (StringWriter stream = new StringWriter(CultureInfo.InvariantCulture))
            using (XmlWriter xmlWriter = XmlWriter.Create(stream))
            {
                serializer.WriteObject(xmlWriter, entidad);
                return stream.ToString();
            }
        }
        /// <summary>
        /// Fabricar the entidad especificada a partir de la repesentación binaria.
        /// </summary>
        /// <typeparam name="T">Tipo de entidad a construir</typeparam>
        /// <param name="entidadBinaria">La representación binaria de la entidad.</param>
        /// <returns>Una instancia de la entidad <c>T</c> con todos sus datos.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public static T Fabricar<T>(byte[] entidadBinaria) where T : new()
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            using (MemoryStream memoryStream = new MemoryStream(entidadBinaria))
            using (XmlDictionaryReader binaryReader = XmlDictionaryReader.CreateBinaryReader(memoryStream, XmlDictionaryReaderQuotas.Max))
                return (T)serializer.ReadObject(binaryReader);
        }
        /// <summary>
        /// Fabricar the entidad especificada a partir de la repesentación binaria.
        /// </summary>
        /// <typeparam name="T">Tipo de entidad a construir</typeparam>
        /// <param name="entidadXml">La representación XML de la entidad.</param>
        /// <returns>
        /// Una instancia de la entidad <c>T</c> con todos sus datos.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public static T Fabricar<T>(string entidadXml) where T : new()
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            using (StringReader streamRead = new StringReader(entidadXml))
            using (XmlReader reader = XmlReader.Create(streamRead))
                return (T)serializer.ReadObject(reader);
        }
        /// <summary>
        /// Determina si es valida la dirección de correo electrónico enviada.
        /// </summary>
        /// <param name="email"><see cref="string"/>La dirección de correo.</param>
        /// <returns>
        /// 	<c>true</c> sí es valido el correo especificado; de lo contrario, <c>false</c>.
        /// </returns>
        public static bool IsEmailValido(string email)
        {
            string patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"
                                  + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                                  + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                                  + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                                  + @"[a-zA-Z]{2,}))$";
            Regex reStrict = new Regex(patternStrict);
            return reStrict.IsMatch(email);
        }
        /// <summary>
        /// Calcula el modulo once para un número.  Este algoritmo se usa para hallar el digito de verificación  en un documento de identidad colombiano.
        /// </summary>
        /// <param name="numero"><see cref="Int64"/> con el número base del cálculo.</param>
        /// <returns>
        /// 	<see cref="Byte"/> con el dígito calculado como módulo 11.
        /// </returns>
        public static byte ModuloOnce(long numero)
        {
            var digitos = numero.ToString(CultureInfo.InvariantCulture);
            var primos = new byte[] { 3, 7, 13, 17, 19, 23, 29, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };
            if (primos.Length < digitos.Length)
                throw new ErrorException(
                    string.Format(CultureInfo.CurrentUICulture, "Algoritomo de módulo 11 sólo es soportado para números con {0} digitos",
                    primos.Length));

            int acumulado = 0;
            byte inPrimo = 0;
            for (short i = Convert.ToInt16(digitos.Length - 1); i > -1; i--)
            {
                acumulado += primos[inPrimo] * (Convert.ToByte(digitos.Substring(i, 1), CultureInfo.InvariantCulture));
                inPrimo++;
            }
            byte residuo = Convert.ToByte(acumulado % 11);
            if (residuo < 2)
                return residuo;
            else
                return Convert.ToByte(11 - residuo);
        }
        /// <summary>
        /// Adiciona un espacio en blanco a la oración por cada letra mayuscula que encuntra.
        /// </summary>
        /// <param name="text">El texto a editar.</param>
        /// <returns></returns>
        public static string AddSpacesToSentence(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";

            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                {
                    newText.Append(' ');
                    newText.Append(char.ToLower(text[i], CultureInfo.InvariantCulture));
                }
                else
                    newText.Append(text[i]);
            }
            return newText.ToString();
        }
        /// <summary>
        /// <para>
        /// Copiar las propiedades entre dos instancias de clases que implementen la misma interfaz.  Las clases no deben ser del mismo tipo, solo se valida que implementen la interfaz.
        /// </para>
        /// <para>Este método no es recursivo y solo copia las porpiedades que se pueden asignar (implementan el set).</para>
        /// </summary>
        /// <typeparam name="TOrigen">El <see cref="Type"/>  del origen.</typeparam>
        /// <typeparam name="TDestino">El <see cref="Type"/>  del destino.</typeparam>
        /// <param name="interfaz">La interfaz comun entre las dos instancias.</param>
        /// <param name="origen">La instancia de origen.</param>
        /// <param name="destino">La instancia de destino.</param>
        /// 28/07/2010 by Jaimir Guerrero
        public static void CopiarPropiedades<TOrigen, TDestino>(Type interfaz, TOrigen origen, TDestino destino)
        {
            if (interfaz == null) throw new ArgumentNullException("interfaz");
            if (origen == null) throw new ArgumentNullException("origen");
            if (destino == null) throw new ArgumentNullException("destino");

            Type sourceType = typeof(TOrigen);
            Type targetType = typeof(TDestino);

            var interfaces = sourceType.GetInterface(interfaz.FullName, false);
            if (interfaces == null)
                throw new ErrorException(string.Format(CultureInfo.InvariantCulture, "El origen [{0}] no implementa la interfaz [{1}]", sourceType, interfaz));

            interfaces = targetType.GetInterface(interfaz.FullName, false);
            if (interfaces == null)
                throw new ErrorException(string.Format(CultureInfo.InvariantCulture, "El destino [{0}] no implementa la interfaz [{1}]", targetType, interfaz));

            foreach (var prop in interfaz.GetProperties().Where(t => t.CanRead && t.CanWrite))
            {
                var sourceValue = sourceType.GetProperty(prop.Name).GetValue(origen, null);
                targetType.GetProperty(prop.Name).SetValue(destino, sourceValue, null);
            }
        }
        /// <summary>
        /// <para>
        /// Copiar las propiedades entre dos instancias de clases del mismo tipo.
        /// </para>
        /// <para>Este método no es recursivo y solo copia las porpiedades que se pueden asignar (implementan el set).</para>
        /// </summary>
        /// <typeparam name="TOrigen">El <see cref="Type"/>  del origen.</typeparam>
        /// <param name="origen">La instancia de origen.</param>
        /// <param name="destino">La instancia de destino.</param>
        /// 25/11/2010 by Jaimir Guerrero
        public static void CopiarPropiedades<TOrigen>(TOrigen origen, TOrigen destino)
        {
            if (origen == null) throw new ArgumentNullException("origen");
            if (destino == null) throw new ArgumentNullException("destino");

            foreach (var prop in typeof(TOrigen).GetProperties().Where(t => t.CanRead && t.CanWrite))
            {
                var sourceValue = prop.GetValue(origen, null);
                prop.SetValue(destino, sourceValue, null);
            }
        }
        /// <summary>
        /// Obtener la lista de culturas e Idiomas instalada en el computador ordenadas alfabéticamente
        /// </summary>
        /// <returns></returns>
        public static CultureInfo[] CulturasOrdenadas()
        {
            CultureInfo[] da = CultureInfo.GetCultures(CultureTypes.AllCultures);
            Array.Sort<CultureInfo>(da, (x, y) =>
                string.Compare(x.DisplayName, y.DisplayName, StringComparison.CurrentCulture));
            return da;
        }
        /// <summary>
        /// Retorna el valor asignado en la sección AppSetting
        /// </summary>
        /// <param name="key">Parametro a buscar</param>
        /// <returns></returns>
        public static string GetAppSetting(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }
        /// <summary>
        /// Copiar las propiedades entre dos instancias de clases de diferente tipo.  Solo se valida que las porpiedades se llamen igual y sean del mismo tipo.
        /// </summary>
        /// <typeparam name="TOrigen">El <see cref="Type"/>  del origen.</typeparam>
        /// <typeparam name="TDestino">El <see cref="Type"/>  del destino.</typeparam>
        /// <param name="origen">La instancia de origen.</param>
        /// <param name="destino">La instancia de destino.</param>
        /// 28/01/2011 by Jaimir Guerrero
        public static void CopiarPropiedades<TOrigen, TDestino>(TOrigen origen, TDestino destino)
        {
            var sourceType = typeof(TOrigen);
            var targetType = typeof(TDestino);
            var className = GetClassName(sourceType, targetType);
            var flags = BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod;
            var args = new object[] { origen, destino };
            GenerateCopyClass(className, sourceType, targetType);
            TiposCopia[className].InvokeMember("CopyProps", flags, null, null, args, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// TiposCopiarimir un texto en un buffer de <see cref="byte"/> para ahorrar espacio.
        /// </summary>
        /// <param name="texto">El texto a TiposCopiarimir.</param>
        /// <returns>Arreglo de <see cref="byte"/> con la información TiposCopiarimida.</returns>
        /// 02/09/2010 by Jaimir Guerrero
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public static byte[] ComprimirTexto(string texto)
        {
            if (string.IsNullOrEmpty( texto)) throw new ArgumentNullException("texto");
            
            byte[] enc = new byte[texto.Length];
            int indexBA = 0;
            foreach (char item in texto.ToCharArray())
                enc[indexBA++] = (byte)item;

            using (MemoryStream store = new MemoryStream())
            {
                using (GZipStream strm = new GZipStream(store, CompressionMode.Compress, true))
                    strm.Write(enc, 0, enc.Length);

                store.Position = 0;
                return store.ToArray();
            }
        }
        /// <summary>
        /// DesTiposCopiarimir un texto a partir del buffer.
        /// </summary>
        /// <param name="zip">Un arreglo de <see cref="byte"/> con el buffer TiposCopiarimido.</param>
        /// <returns>El Texto original</returns>
        /// 02/09/2010 by Jaimir Guerrero
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public static string DescomprimirTexto(byte[] zip)
        {
            int length = BitConverter.ToInt32(zip, 0);

            using (MemoryStream store = new MemoryStream(zip))
            {
                byte[] enc = new byte[length];
                int read = 0;
                using (GZipStream strm = new GZipStream(store, CompressionMode.Decompress, true))
                    read = strm.Read(enc, 0, length);

                store.Position = 0;
                StringBuilder sB = new StringBuilder();
                for (int i = 0; i < read; i++)
                    sB.Append((char)enc[i]);

                return sB.ToString();
            }
        }
        /// <summary>
        /// Obtiene el tipo MIME dependiendo de la extención del archivo.
        /// </summary>
        /// <param name="fileName">Nombre del archivo.</param>
        /// <returns></returns>
        /// 24/08/2010 by Jaimir_guerrero
        public static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToUpperInvariant();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }
        #endregion

        #region Internas
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "error")]
        internal static string EvaluarTarget(Exception error)
        {
            try
            {
#if SILVERLIGHT
                return string.Empty;
#else
                if (error.TargetSite == null)
                    return string.Empty;
                else
                    return error.TargetSite.Name;
#endif
            }
            catch
            {
                return string.Empty;
            }
        }
        internal static string EvaluarData(Exception error)
        {
            try
            {

                if (error.Data != null && error.Data.Count > 0)
                {
                    StringBuilder obMensaje = new StringBuilder();
                    foreach (DictionaryEntry de in error.Data)
                        obMensaje.AppendFormat(CultureInfo.InvariantCulture, "{0}=[{1}] | ", de.Key, de.Value);

                    return obMensaje.ToString();
                }
                else
                    return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion

        #region funciones
        private static IList<PropertyMap> GetMatchingProperties(Type sourceType, Type targetType)
        {
            PropertyInfo[] sourceProperties = sourceType.GetProperties();
            PropertyInfo[] targetProperties = targetType.GetProperties();

            return (from s in sourceProperties
                    from t in targetProperties
                    where s.Name == t.Name &&
                    s.PropertyType == t.PropertyType &&
                    s.CanRead && t.CanWrite &&
                    s.PropertyType.IsPublic &&
                    t.PropertyType.IsPublic &&
                    t.GetSetMethod(true).IsPublic
                    orderby s.Name
                    select new PropertyMap { SourceProperty = s, TargetProperty = t }).ToList();
        }
        private static string GetClassName(Type sourceType, Type targetType)
        {
            var className = "Copy_";
            className += sourceType.FullName.Replace(".", "_");
            className += "_";
            className += targetType.FullName.Replace(".", "_");

            return className;
        }
        private static void GenerateCopyClass(string className, Type sourceType, Type targetType)
        {
            if (TiposCopia.ContainsKey(className))
                return;

            var builder = new StringBuilder();
            builder.Append("namespace Copy {\r\n");
            builder.Append(" public class ");
            builder.Append(className);
            builder.Append(" {\r\n");
            builder.Append(" public static void CopyProps(");
            builder.Append(sourceType.FullName);
            builder.Append(" source, ");
            builder.Append(targetType.FullName);
            builder.Append(" target) {\r\n");

            List<string> ensamblados = new List<string>();
            ensamblados.Add(typeof(Comun).Assembly.Location);
            ensamblados.Add(sourceType.Assembly.Location);
            if (sourceType.Assembly.Location != targetType.Assembly.Location)
                ensamblados.Add(targetType.Assembly.Location);

            var map = GetMatchingProperties(sourceType, targetType);
            foreach (var item in map)
            {
                builder.Append(" target.");
                builder.Append(item.TargetProperty.Name);
                builder.Append(" = ");
                builder.Append("source.");
                builder.Append(item.SourceProperty.Name);
                builder.Append(";\r\n");

                // Por definición TargetProperty y SourceProperty son del mismo tipo por ende no es necesario
                // definir los dos paths.
                string ensambladoPath = item.SourceProperty.PropertyType.Assembly.Location;
                if (!ensamblados.Contains(ensambladoPath))
                    ensamblados.Add(ensambladoPath);
            }
            builder.Append(" }\r\n }\r\n}");

            // Write out method body
            Trace.WriteLine(builder.ToString());

            using (CodeDomProvider codeDomProvider = new CSharpCodeProvider())
            {
                var compilerParameters = new CompilerParameters();
                compilerParameters.ReferencedAssemblies.AddRange(ensamblados.ToArray());
                compilerParameters.GenerateInMemory = true;
                var results = codeDomProvider.CompileAssemblyFromSource(compilerParameters, builder.ToString());

                // Compiler output
                foreach (var line in results.Output)
                    Trace.WriteLine(line);

                var copierType = results.CompiledAssembly.GetType("Copy." + className);
                TiposCopia.Add(className, copierType);
            }
        }
        #endregion
    }
    internal class PropertyMap
    {
        internal PropertyInfo SourceProperty { get; set; }
        internal PropertyInfo TargetProperty { get; set; }
    }

}
