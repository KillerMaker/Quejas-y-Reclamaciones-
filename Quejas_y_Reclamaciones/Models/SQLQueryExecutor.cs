using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Quejas_y_Reclamaciones.Models
{
    interface SQLQueryExecutor

    {
        public async static Task<int> CommandExecutor(SqlCommand command)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-7V51383\\SQLEXPRESS;Initial Catalog=Quejas&Reclamaciones;Integrated Security=True"))
                {
                    await connection.OpenAsync();
                    command.Connection = connection;

                    return await command.ExecuteNonQueryAsync();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async static Task<SqlDataReader> ReaderExecutor(SqlCommand command)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-7V51383\\SQLEXPRESS;Initial Catalog=Quejas&Reclamaciones;Integrated Security=True"))
                {
                    await connection.OpenAsync();
                    command.Connection = connection;
                    
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    return reader;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
