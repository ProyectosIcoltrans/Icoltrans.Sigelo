using System;
using System.Linq;
using Icoltrans.Sigelo.Entity.Maestros;


namespace Icoltrans.Sigelo.Data
{
    public partial class MaestrosModelo 
    {
        public ParametrosPantalla ObtenerParametros()
        {
            short[] parametrosId = new short[] { 30, 31, 32, 33, 34 };

            var query = (from c in this.Parametros
                         where parametrosId.Contains(c.IdParametro)
                         select new { Id = c.IdParametro, Valor = c.Valor }).ToDictionary(p => p.Id);

            return new ParametrosPantalla
            {
                InformeOperaciones = new Uri(query[30].Valor),
                InformeNovedadesManifiesto = new Uri(query[31].Valor),
                CapturaNovedades = new Uri(query[32].Valor),
                InformeSatelital = new Uri(query[33].Valor),
                ParametrosSatelital = query[34].Valor
            };           
        }
    }
}
