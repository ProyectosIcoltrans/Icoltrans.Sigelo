namespace Icoltrans.Sigelo.Data.DA
{
    using Dapper;
    using DapperExtensions;
    using Icoltrans.Sigelo.Data.DA.Interface;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    /// <summary>
    /// Clase que maneja el contexto de la base de datos.
    /// </summary>
    /// <typeparam name="T">Clase del contexto</typeparam>
    /// <remarks>
    /// Autor:                  Alex Mauricio Paredes Celorio.
    /// Fecha:                  23/09/2016
    /// Ultima Modificacion:    23/09/2016
    /// </remarks>
    public class DbContextScope<T> : IDbContextScope<T> where T : class
    {
        #region Atributos
        /// <summary>
        /// 
        /// </summary>
        private static readonly Lazy<DbContextScope<T>> instancia = new Lazy<DbContextScope<T>>(() => new DbContextScope<T>());

        private string connectionString = string.Empty;
        public string DataBase
        {
            set
            {
                connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[value].ToString();
            }
        }
        #endregion

        #region Constructor
        public DbContextScope()
        {
            connectionString = string.Empty;
        }
        #endregion

        #region Singleton
        public static DbContextScope<T> ObtenerInstancia
        {
            get
            {
                return instancia.Value;
            }
        }
        #endregion

        public bool Insert(T parameter)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                sqlConnection.Insert(parameter);
                sqlConnection.Close();
                return true;
            }
        }

        public int InsertWithReturnId(T parameter)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var recordId = sqlConnection.Insert(parameter);
                sqlConnection.Close();
                return recordId;
            }
        }

        public bool Update(T parameter)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                sqlConnection.Update(parameter);
                sqlConnection.Close();
                return true;
            }
        }

        public IList<T> GetAll()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var result = sqlConnection.GetList<T>();
                sqlConnection.Close();
                return result.ToList();
            }
        }

        public T Find(PredicateGroup predicate)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var result = sqlConnection.GetList<T>(predicate).FirstOrDefault();
                sqlConnection.Close();
                return result;
            }
        }

        public bool Delete(PredicateGroup predicate)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                sqlConnection.Delete<T>(predicate);
                sqlConnection.Close();
                return true;
            }
        }

        public IEnumerable<T> QuerySP(string storedProcedure, dynamic param = null, dynamic outParam = null,
                                            SqlTransaction transaction = null, bool buffered = true,
                                            int? commandTimeout = null)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            var output = connection.Query<T>(storedProcedure, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: commandTimeout, commandType: CommandType.StoredProcedure);
            return output;
        }
    }
}
