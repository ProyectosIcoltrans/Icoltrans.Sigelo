using System;
using System.Globalization;

namespace Icoltrans.Sigelo.Rules.ComProxy
{
    internal static class Utilidades
    {
        internal static string AjustarGuid(Guid entrada)
        {
            return string.Format(CultureInfo.InvariantCulture, "{{{0}}}", entrada.ToString().ToUpperInvariant());
        }
    }
}
