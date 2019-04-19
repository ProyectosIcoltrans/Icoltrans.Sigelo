namespace Icoltrans.Sigelo.Entity.Vehiculos
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class VehiculoCaravanaTemp
    {
        #region VehiculoCaravana
        [DataMember]
        public string CiudadesRecorrido { get; set; }
        [DataMember]
        public Nullable<int> NumeroCarnet { get; set; }
        [DataMember]
        public System.Guid Id { get; set; }
        [DataMember]
        public string NombreCaravana { get; set; }
        [DataMember]
        public string Origen { get; set; }
        [DataMember]
        public string Destino { get; set; }
        [DataMember]
        public bool NoCambiar { get; set; }
        [DataMember]
        public string Estado { get; set; }
        #endregion
        #region Vehiculos
        [DataMember]
        public string Placa { get; set; }
        [DataMember]
        public string Carroceria { get; set; }
        [DataMember]
        public string Chasis { get; set; }
        [DataMember]
        public string Color { get; set; }
        [DataMember]
        public string Complemento { get; set; }
        [DataMember]
        public string De { get; set; }
        [DataMember]
        public byte Ejes { get; set; }
        [DataMember]
        public Nullable<byte> GPS { get; set; }
        [DataMember]
        public Nullable<decimal> Manifiesto { get; set; }
        [DataMember]
        public string Marca { get; set; }
        [DataMember]
        public short Modelo { get; set; }
        [DataMember]
        public string Motor { get; set; }
        [DataMember]
        public float Peso { get; set; }
        [DataMember]
        public bool Prioridad { get; set; }
        [DataMember]
        public string Tipo { get; set; }
        [DataMember]
        public string Vinculacion { get; set; }
        [DataMember]
        public float Volumen { get; set; }
        [DataMember]
        public byte VinculacionId { get; set; }
        #endregion
        #region Conductor
        [DataMember]
        public string Documento { get; set; }
        [DataMember]
        public decimal Numero { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string PrimerApellido { get; set; }
        [DataMember]
        public string SegundoApellido { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        [DataMember]
        public string Ciudad { get; set; }
        [DataMember]
        public string Telefonos { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Licencia { get; set; }
        [DataMember]
        public string Categoria { get; set; }
        [DataMember]
        public Nullable<System.DateTime> Vence { get; set; }
        [DataMember]
        public string CuentaAhorros { get; set; }
        [DataMember]
        public string Banco { get; set; }
        #endregion
    }
}