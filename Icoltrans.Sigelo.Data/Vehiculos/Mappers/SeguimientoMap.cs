using DapperExtensions.Mapper;
using Icoltrans.Sigelo.Entity.Vehiculos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icoltrans.Sigelo.Data.Vehiculos.Mappers
{
    public class SeguimientoMap :  ClassMapper<Seguimiento>
    {
        public SeguimientoMap()
            {
                Table("Seguimiento");
                Map(x => x.fidCaravana).Column("fidCaravana");
                Map(x => x.fvcDescripcion).Column("fvcDescripcion");
                Map(x => x.finIdRuta).Column("finIdRuta");
                Map(x => x.origen).Column("origen");
                Map(x => x.destino).Column("destino");
                Map(x => x.fdtSalida).Column("fdtSalida");
                Map(x => x.ftyMinutosReporte).Column("ftyMinutosReporte");
                Map(x => x.ftyMinutosAlerta).Column("ftyMinutosAlerta");
                Map(x => x.fbtUrbana).Column("fbtUrbana");
                Map(x => x.finUltimoPunto).Column("finUltimoPunto");
                Map(x => x.fvcUltimoPunto).Column("fvcUltimoPunto");
                Map(x => x.fvcEstado).Column("fvcEstado");
                Map(x => x.fdtUltimoPunto).Column("fdtUltimoPunto");
                Map(x => x.finSiguientePunto).Column("finSiguientePunto");
                Map(x => x.fvcSiguientePunto).Column("fvcSiguientePunto");
                Map(x => x.fdtSiguientePunto).Column("fdtSiguientePunto");
                Map(x => x.frlDiferencia).Column("frlDiferencia");
                Map(x => x.ruta).Column("ruta");
                Map(x => x.ftyEstReporte).Column("ftyEstReporte");
                Map(x => x.ftyEstAlerta).Column("ftyEstAlerta");
                Map(x => x.Vehiculos).Column("Vehiculos");
                Map(x => x.ftxObservacion).Column("ftxObservacion");
                Map(x => x.Satelital).Column("Satelital");
                Map(x => x.URLSatelital).Column("URLSatelital");
                Map(x => x.UsuarioSatelital).Column("UsuarioSatelital");
                Map(x => x.ClaveSatelital).Column("ClaveSatelital");
                AutoMap();
            }
    }
}
