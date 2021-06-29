using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Quejas_y_Reclamaciones.Models
{
    public class CDepartment:CEntity<int>
    {
        protected static SqlConnection _connection;
        protected static SqlCommand _command;
        protected static SqlDataReader _reader;

        public int? id { get; set; }
        public string departmentName { get; set; }
        public int managerId { get; set; }

        public CDepartment(int? id,string departmentName,int managerId )
        {
            this.id = id;
            this.departmentName = departmentName;
            this.managerId = managerId;

            setConnection();
        }

        public async override Task<int> Insert()
        {

            try 
            {
                if (_connection.State.Equals(ConnectionState.Closed))
                   await _connection.OpenAsync();


                _command = new SqlCommand($@"EXEC INSERTA_DEPARTAMENTO 
                                                    '{departmentName}'
                                                    ,{managerId};",_connection);

                return (await _command.ExecuteNonQueryAsync() != 0) ? id.Value : 0;

            }
            catch(Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }
        }

        public override Task<int> Update()
        {
            throw new NotImplementedException();
        }

        public async static Task<List<CDepartment>> Select(string searchString)
        {

            try 
            {
                setConnection();
                _connection = connection;

                if (_connection.State.Equals(ConnectionState.Open))
                    await _connection.CloseAsync();

                await _connection.OpenAsync();

                List<CDepartment> departments = new List<CDepartment>();
                CDepartment department = null;

                _command = new SqlCommand($"SELECT * FROM DEPARTAMENTO {searchString}",_connection);

                _reader = await _command.ExecuteReaderAsync();

                while(await _reader.ReadAsync())
                {
                    department = new CDepartment(
                        int.Parse(_reader["ID_DEPARTAMENTO"].ToString()),
                        _reader["NOMBRE_DEPARTAMENTO"].ToString(),
                        int.Parse(_reader["ID_ENCARGADO"].ToString()));

                    departments.Add(department);
                }

                return departments;

            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }

        }
    }
}
