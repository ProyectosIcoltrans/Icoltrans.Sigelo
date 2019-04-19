namespace Icoltrans.Sigelo.Data.DA.Interface
{
    using DapperExtensions;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    /// <summary>
    /// Interface de la clase que maneja el contexto de la base de datos.
    /// </summary>
    /// <typeparam name="T">Clase del contexto</typeparam>
    /// <remarks>
    /// Autor:                  Alex Mauricio Paredes Celorio.
    /// Fecha:                  24/09/2016
    /// Ultima Modificacion:    24/09/2016
    /// </remarks>
    public interface IDbContextScope<T> where T : class
    {
        bool Insert(T parameter);
        int InsertWithReturnId(T parameter);
        bool Update(T parameter);
        IList<T> GetAll();
        T Find(PredicateGroup predicate);
        bool Delete(PredicateGroup predicate);
        IEnumerable<T> QuerySP(string storedProcedure, dynamic param = null,
            dynamic outParam = null, SqlTransaction transaction = null,
            bool buffered = true, int? commandTimeout = null);
    }
}
