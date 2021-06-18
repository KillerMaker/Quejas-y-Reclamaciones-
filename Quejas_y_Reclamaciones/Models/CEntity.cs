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
        protected static SqlConnection _connection;
        protected static SqlCommand _command;
        protected static SqlDataReader _reader;

        public abstract Task<T> Insert();
        public abstract Task<int> Update();

        protected static void setConnection()
        {
            //_connection = new SqlConnection("Data Source=DESKTOP-T76LFOU;Initial Catalog=Quejas&Reclamaciones;Integrated Security=True");
            _connection = new SqlConnection("Data Source=DESKTOP-7V51383\\SQLEXPRESS;Initial Catalog=Quejas&Reclamaciones;Integrated Security=True");

        }
    }
}
