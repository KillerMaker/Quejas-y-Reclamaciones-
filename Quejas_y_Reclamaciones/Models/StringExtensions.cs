using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quejas_y_Reclamaciones
{
    public static class ExtensionsMethods
    {
        /// <summary>
        /// Transforma la cadena de entrada para que sea seguro utilizarla en una consulta a la base de datos
        /// </summary>
        /// <param name="cadena">Cadena desde la que se ejcuta la funcion</param>
        /// <returns>La cadena limpia de posible codigo infiltrado</returns>
        public static string SQLInyectionClearString(this string cadena)
        {
            string s = cadena.ToUpper()
                .Replace("--", "")
                .Replace("'","")
                .Replace("<","")
                .Replace(">","")
                .Replace("=","")
                .Replace("!","")
                .Replace("*","")
                .Replace("SELECT ","")
                .Replace("UPDATE ","")
                .Replace("DELETE ","")
                .Replace("DROP","")
                .Replace("CREATE","")
                .Replace("EXEC","")
                .Replace(";","").MayusCadaEspacio();
            return s;
        }

        /// <summary>
        /// Transforma la cadena de entrada para que contenga masusculas luego de cada espacio
        /// </summary>
        /// <param name="cadena">Cadena desde la que se ejecuta la funcion</param>
        /// <returns>La cadena con mauysculas tras cada espacio</returns>
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
