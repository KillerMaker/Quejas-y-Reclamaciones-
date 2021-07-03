using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Quejas_y_Reclamaciones.Models
{
    public abstract class CEntity<T>
    {
        protected static SqlConnection connection;

        /// <summary>
        /// Inserta en la base de datos los datos del objeto CEntity
        /// </summary>
        /// <returns>Devuelve T</returns>
        public abstract Task<T> Insert();

        /// <summary>
        /// Actualiza los datos del objeto CEntity donde el id de este coincida en la base de datos
        /// </summary>
        /// <returns>El id del registro actualizado en la base de datos</returns>
        public abstract Task<int> Update();

        protected static void setConnection()
        {
            connection = new SqlConnection("Data Source=DESKTOP-T76LFOU;Initial Catalog=Quejas&Reclamaciones;Integrated Security=True");
            //connection = new SqlConnection("Data Source=DESKTOP-7V51383\\SQLEXPRESS;Initial Catalog=Quejas&Reclamaciones;Integrated Security=True");

        }
    }
}
