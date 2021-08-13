using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Associacao.App.Extensions
{
    public static class RazorExtensions
    {
        public static string FormataDocumento(this RazorPage page, string documento)
        {
            return Convert.ToUInt64(documento).ToString(@"00\.000\.000\-0");
        }

        //public static string FormataData(this RazorPage page, DateTime? data)
        //{
        //    return data.ToString(@"dd'/'MM'/'yyyy");
        //}

        public static string FormataTelefone(this RazorPage page, string numero)
        {
            return numero.Length == 11 ? Convert.ToUInt64(numero).ToString(@"(00) 00000-0000") : Convert.ToUInt64(numero).ToString(@"(00) 0000-0000");
        }

    }
}
