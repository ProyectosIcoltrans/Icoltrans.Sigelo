namespace Icoltrans.Sigelo.Data.Vehiculos
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Configuration;

    public class Datos
    {
        /// <summary>
        ///     Cadena de conexion a la base de datos.
        /// </summary>
        private SqlConnection conexion;

        /// <summary>
        ///     Constructor sin parametros.
        /// </summary>
        public Datos()
        {
            conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CadenaDefault"].ConnectionString);
        }

        /// <summary>
        ///     Constructor que especifica la conexion a usar.
        /// </summary>
        /// <param name="strBaseDatos"></param>
        public Datos(string strBaseDatos)
        {
            // Si la base de datos a usar es Mayes.
            if (strBaseDatos == "mayes")
            {
                conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CadenaMayes"].ConnectionString);
            }

            // Si la base de datos a usar es MayesDataMart.
            else if (strBaseDatos == "CadenaVehiculos")
            {
                conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CadenaVehiculos"].ConnectionString);
            }

            // Si la base de datos a usar es Produccion.
            else if (strBaseDatos == "produccion")
            {
                conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CadenaProduccion"].ConnectionString);
            }

            // Si la base de datos a usar es Maestros.
            else if (strBaseDatos == "maestros")
            {
                conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CadenaMaestros"].ConnectionString);
            }

            // Si la base de datos a usar es Mayes en el servidor de produccion.
            else if (strBaseDatos == "mayesProduccion")
            {
                conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CadenaMayesProduccion"].ConnectionString);
            }
        }

        #region "Setters y Getters"

        /// <summary>
        ///     Metodo que devuelve la conexion a la base de datos.
        /// </summary>
        public SqlConnection getConexion
        {
            get
            {
                return conexion;
            }
        }

        #endregion


        #region "Metodos Privados"

        /// <summary>
        ///     Metodo que se conecta con la base de datos usando el objeto "conexion".
        /// </summary>
        /// <returns> Devuelve True o False dependiendo el exito o no de la conexion.</returns>
        private bool Conectar()
        {
            try
            {
                // Se abre la conexion.
                conexion.Open();

                // Si la conexion resulta exitosa se devuelve True.
                return true;
            }
            catch (System.Exception)
            {
                // Si ocurre algun error devuelve False.
                return false;
            }
        }

        /// <summary>
        ///     Se desconecta de la base de datos.
        /// </summary>
        private void Desconectar()
        {
            // Se cierra la conexion.
            conexion.Close();
        }

        #endregion


        #region "Metodos Publicos"

        /// <summary>
        ///     Ejecuta un comando en la base de datos utilizando el objeto "conexion".
        /// </summary>
        /// <param name="strSql"> Comando Sql que se desea ejecutar.</param>
        /// <param name="sqlprParametros"> Parametros que necesita el comando SQL </param>
        public bool EjecutarComandoSql(string strSql, params SqlParameter[] sqlprParametros)
        {
            string s = string.Empty;
            try
            {
                // Se abre la conexion.
                if (Conectar())
                {
                    // Se declara el nuevo comando SQL.
                    SqlCommand comando = new SqlCommand();

                    // Se asocia la conexion con el comando SQL.
                    comando.Connection = this.conexion;

                    // Se asocia la sentencia SQL que esta como parametro al comando SQL.
                    comando.CommandText = strSql;

                    // Para cada parametro en el arreglo de parametros.
                    foreach (SqlParameter item in sqlprParametros)
                    {
                        // Se agrega cada parametros a la consulta SQL.
                        comando.Parameters.Add(item);
                    }

                    // Se ejecuta el comando SQL.
                    comando.ExecuteNonQuery();

                    // Se cierra la conexion.
                    Desconectar();

                    // Si no ocurrio ningun problema con la ejecucion del comando se devuelve True.
                    return true;
                }
                // Si no se puede abrir la conexion se devuelve false.
                else
                    return false;
            }
            catch (Exception ex)
            {
                s = ex.ToString();
                // Si ocurre algun problema con la ejecucion del comando SQL se devuelve False.
                return false;
            }
        }

        /// <summary>
        ///     Ejecuta un comando INSERT en la base de datos y devuelve el id recien insertado
        /// </summary>
        /// <param name="strSql"> Comando INSERT que se desea ejecutar.</param>
        /// <param name="sqlprParametros"> Parametros que necesita el comando INSERT </param>
        public Int32 EjecutarInsert(string strSql, params SqlParameter[] sqlprParametros)
        {
            // Variable que almacena el id recien insertado.
            Int32 resultado = 0;

            try
            {
                // Se abre la conexion.
                if (this.Conectar())
                {
                    // Se declara el nuevo comando SQL.
                    SqlCommand comando = new SqlCommand();

                    // Se asocia la conexion con el comando SQL.
                    comando.Connection = this.conexion;

                    // Se asocia la sentencia SQL que esta como parametro al comando SQL.
                    comando.CommandText = strSql + "; SELECT SCOPE_IDENTITY();";

                    // Para cada parametro en el arreglo de parametros.
                    foreach (SqlParameter item in sqlprParametros)
                    {
                        // Se agrega cada parametros a la consulta SQL.
                        comando.Parameters.Add(item);
                    }

                    // Se ejecuta el comando SQL.
                    resultado = Convert.ToInt32(comando.ExecuteScalar());

                    // Se cierra la conexion.
                    this.Desconectar();

                    // Si no ocurrio ningun problema con la ejecucion del comando se devuelve True.
                    return resultado;
                }
                // Si no se puede abrir la conexion se devuelve false.
                else
                    return 0;
            }
            catch (Exception)
            {
                // Si ocurre algun problema con la ejecucion del comando SQL se devuelve False.
                return 0;
            }
        }

        /// <summary>
        ///     Ejecuta un comando INSERT en la base de datos y devuelve el id recien insertado
        /// </summary>
        /// <param name="strSql"> Comando INSERT que se desea ejecutar.</param>
        /// <param name="sqlprParametros"> Parametros que necesita el comando INSERT </param>
        public T EjecutarEscalar<T>(string strSql, Dictionary<string, object> dicParameters)
        {
            // Variable que almacena el id recien insertado.
            T resultado = default(T);

            try
            {
                // Se abre la conexion.
                if (this.Conectar())
                {
                    // Se declara el nuevo comando SQL.
                    SqlCommand comando = new SqlCommand();

                    comando.CommandType = CommandType.StoredProcedure;
                    // Se asocia la conexion con el comando SQL.
                    comando.Connection = this.conexion;

                    // Se asocia la sentencia SQL que esta como parametro al comando SQL.
                    comando.CommandText = strSql;

                    // Para cada parametro en el arreglo de parametros.
                    if (dicParameters != null && dicParameters.Count > 0)
                    {
                        foreach (KeyValuePair<string, object> keyParameter in dicParameters)
                        {
                            // Se agrega cada parametros a la consulta SQL.
                            comando.Parameters.AddWithValue(keyParameter.Key, keyParameter.Value);
                        }
                    }

                    // Se ejecuta el comando SQL.
                    resultado = (T)comando.ExecuteScalar();

                    // Se cierra la conexion.
                    this.Desconectar();

                    // Si no ocurrio ningun problema con la ejecucion del comando se devuelve True.
                    return resultado;
                }
                // Si no se puede abrir la conexion se devuelve false.
                else
                    return default(T);
            }
            catch (Exception)
            {
                // Si ocurre algun problema con la ejecucion del comando SQL se devuelve False.
                return default(T);
            }
        }


        /// <summary>
        ///     Ejecuta una consulta en la base de datos.
        /// </summary>
        /// <param name="strSql"> Comando Sql que se desea ejecutar.</param>
        /// <param name="sqlprParametros"> Parametros que necesita el comando SQL </param>
        /// <returns> Devuelve un datatable con el resultado de la consulta ejecutada </returns>
        public DataTable EjecutarConsulta(string strSql, params SqlParameter[] sqlprParametros)
        {
            // Se crea el dataAdapter.
            SqlDataAdapter adaptador = new SqlDataAdapter();

            // Se asocia el crea el comando select del adaptador.
            adaptador.SelectCommand = new SqlCommand();

            // Se asocia la conexion al adaptador.
            adaptador.SelectCommand.Connection = conexion;

            // Se asocia el texto de la consulta SQL al comandoSQL.
            adaptador.SelectCommand.CommandText = strSql;

            // Se agrega cada parametro de entrada al comando SQL.
            foreach (SqlParameter item in sqlprParametros)
            {
                // Se agrega cada parametro al comando.
                adaptador.SelectCommand.Parameters.Add(item);
            }

            // Se crea el dataset que guardara el resultado de la consulta.
            DataSet resultado = new DataSet();

            // Se llena el dataset con la ejecucion de la consulta.
            adaptador.Fill(resultado);

            // Se muestra la tabla 0 del dataset obtenido.
            return resultado.Tables[0];
        }

        public DataSet ExecuteQuery(string strProcedure, Dictionary<string, object> dicParameters, ref bool bolSpError)
        {
            DataSet dtsData = null;
            SqlDataAdapter sqlDataAdapter = null;

            try
            {
                sqlDataAdapter = new SqlDataAdapter(strProcedure, (SqlConnection)conexion);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandTimeout = 500;

                if (dicParameters != null && dicParameters.Count > 0)
                {
                    foreach (KeyValuePair<string, object> keyParameter in dicParameters)
                    {
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue(keyParameter.Key, keyParameter.Value);
                    }
                }

                dtsData = new DataSet();
                sqlDataAdapter.Fill(dtsData, "TablaResultadoProcedimiento");
                sqlDataAdapter.Dispose();

            }
            catch (Exception e)
            {
                if (sqlDataAdapter != null) sqlDataAdapter.Dispose();

                dtsData = null;
                bolSpError = true;

                throw new Exception(string.Format("[Base de Datos]: Error al ejecutar el SP '{0}'. '{1}'.", strProcedure, e.Message));
            }

            if ((dtsData == null) || (dtsData.Tables.Count == 0)) { return null; }
            return dtsData;
        }

        public bool ExecuteAlterStoredProcedure(string strProcedure, Dictionary<string, object> dicParameters, ref bool bolSpError)
        {
            int intRowsAffected = 0;
            bool booRowsAffected = false;

            SqlCommand sqlCommand = null;

            try
            {

                sqlCommand = new SqlCommand(strProcedure, (SqlConnection)conexion);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 500;

                foreach (KeyValuePair<string, object> keyParameter in dicParameters)
                {
                    sqlCommand.Parameters.AddWithValue(keyParameter.Key, keyParameter.Value);
                }

                conexion.Open();
                intRowsAffected = sqlCommand.ExecuteNonQuery();

                booRowsAffected = true;
            }
            catch (Exception e)
            {
                if (sqlCommand != null) sqlCommand.Dispose();

                bolSpError = true;

                throw new Exception(string.Format("[Base de Datos]: Error al ejecutar el SP '{0}'. '{1}'.", strProcedure, e.Message));
            }
            finally
            {
                conexion.Close();
            }

            return booRowsAffected;
        }

        /// <summary>
        ///     Ejecuta la ETL.
        /// </summary>
        /// <param name="inCliente"></param>
        /// <param name="inServicio"></param>
        /// <param name="dtFechaInicio"></param>
        /// <param name="dtFechaFinal"></param>
        /// <param name="inCentroOrigen"></param>
        /// <param name="inCentroDestino"></param>
        /// <returns></returns>
        public int actualizarInformacion(Int32 inCliente, Int16 inServicio, string dtFechaInicio, string dtFechaFinal, Int16 inCentroOrigen, Int16 inCentroDestino)
        {
            // Se crean los parametros SQL que permiten crear la entidad.
            SqlParameter sqlPrtFechaInicial = new SqlParameter("@dtFechaInicial", dtFechaInicio);
            SqlParameter sqlPrtFechaFinal = new SqlParameter("@dtFechaFinal", dtFechaFinal);
            SqlParameter sqlPrtCliente = new SqlParameter("@inCliente", inCliente);
            SqlParameter sqlPrtCentroOrigen = new SqlParameter("@smCentroOrigen", inCentroOrigen);
            SqlParameter sqlPrtCentroDestino = new SqlParameter("@smCentroDestino", inCentroDestino);

            // Se obtiene ejecuta la insercion o actualizacion.
            DataTable dttActualizar = new Datos("mayesDataMart").EjecutarConsulta(@"stpTransformarMayes @dtFechaInicial,@dtFechaFinal
                ,@inCliente,@smCentroOrigen,@smCentroDestino",
                sqlPrtFechaInicial, sqlPrtFechaFinal, sqlPrtCliente, sqlPrtCentroOrigen, sqlPrtCentroDestino);

            return Int32.Parse(dttActualizar.Rows[0]["RESULTADO"].ToString());
        }

        public bool EjecutarSpDml(string strProcedimiento, Dictionary<string, object> dicParametros, ref bool bolError)
        {
            int inFilasAfectadas = 0;
            bool bolFilasAfectadas = false;

            SqlCommand sqlComando = null;

            try
            {
                if (this.Conectar())
                {
                    sqlComando = new SqlCommand(strProcedimiento, (SqlConnection)this.getConexion);
                    sqlComando.CommandType = CommandType.StoredProcedure;
                    sqlComando.CommandTimeout = 1500;

                    foreach (KeyValuePair<string, object> kvParametro in dicParametros)
                    {
                        sqlComando.Parameters.AddWithValue(kvParametro.Key, kvParametro.Value);
                    }

                    inFilasAfectadas = sqlComando.ExecuteNonQuery();

                    if (inFilasAfectadas > 0)
                        bolFilasAfectadas = true;
                    else
                        bolFilasAfectadas = false;
                }
            }
            catch (Exception e)
            {
                if (sqlComando != null) sqlComando.Dispose();

                bolError = true;

                throw new Exception(string.Format("[Base de Datos]: Error al ejecutar el SP '{0}'. '{1}'.", strProcedimiento, e.Message));
            }

            return bolFilasAfectadas;
        }

        #endregion
    }
}
