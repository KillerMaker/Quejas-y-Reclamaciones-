using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCaja.Utilidades
{
    public static class ExtensionsMethods
    {
        public static string SQLInyectionClearString(this string cadena)
        {
            string s = cadena.ToUpper().Replace("--", "").Replace("'","").Replace("<","").Replace(">","").Replace("=","").Replace("!","").Replace("*","").Replace("SELECT ","").Replace("UPDATE ","").Replace("DELETE ","").Replace("DROP","").Replace(";","").MayusCadaEspacio();
            return s;
        }
        public static string MayusCadaEspacio(this string cadena)
        {
            string s="";
            for (int i = 0; i < cadena.Length; i++)
            {
                if (i==0||cadena[i - 1] == ' ')
                    s += cadena[i].ToString().ToUpper();
                else
                    s += cadena[i].ToString().ToLower();
            }
            return s;
        }
    }
}
