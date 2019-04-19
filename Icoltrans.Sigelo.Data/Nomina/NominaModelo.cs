using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using Icoltrans.Sigelo.Comunes;
using Icoltrans.Sigelo.Entity.Nomina;
using Microsoft.Data.Extensions;

namespace Icoltrans.Sigelo.Data
{
    public partial class NominaModelo 
    {
        #region Métodos
        /// <summary>
        /// Obteners the informacion usuario.
        /// </summary>
        /// 
        /// <returns></returns>
        public Usuario ObtenerInformacionUsuario()
        {
            Usuario retornoUsuario = null;

            SqlParameter domainUser = new SqlParameter("@vcDomainUser", Comun.Usuario);
            SqlParameter retorno = new SqlParameter("RETURN_VALUE", SqlDbType.Int);
            retorno.Direction = ParameterDirection.ReturnValue;

            using (var q = this.CreateStoreCommand("stpRolDomainUser", CommandType.StoredProcedure, retorno, domainUser))
            {
                q.CommandTimeout = 60;
                retornoUsuario = q.Materialize<Usuario>((r) => new Usuario
                {
                    CiudadDpto = r.Field<string>("CiudadDpto"),
                    DesArea = r.Field<string>("DesArea"),
                    DesCargo = r.Field<string>("DesCargo"),
                    DesCentro = r.Field<string>("DesCentro"),
                    DesCompania = r.Field<string>("DesCompania"),
                    DesSucursal = r.Field<string>("DesSucursal"),
                    NomEmpleado = r.Field<string>("NomEmpleado"),
                    fidRol = r.Field<Guid>("fidRol"),
                    fnuIdentidad = r.Field<decimal>("fnuIdentidad"),
                    fsmCentroCosto = r.Field<Int16>("fsmCentroCosto"),
                    fsmIdArea = r.Field<Int16>("fsmIdArea"),
                    fsmIdCargo = r.Field<Int16>("fsmIdCargo"),
                    fsmIdSucursal = r.Field<Int16>("fsmIdSucursal"),
                    ftyIdCompania = (Int16)r.Field<byte>("ftyIdCompania"),
                    fvcIDUbicacion = r.Field<string>("fvcIDUbicacion"),
                    IdCOperativo = r.Field<Int16>("IdCOperativo"),
                    IdPunto = r.Field<Guid>("IdPunto")
                }).FirstOrDefault();
                if ((int)retorno.Value == 1)
                    throw new ErrorException(string.Format(CultureInfo.InvariantCulture,
                        "El usuario [{0}] no esta registrado como un empleado activo", Comun.Usuario));
            }
            return retornoUsuario;
        }
        #endregion
    }
}
