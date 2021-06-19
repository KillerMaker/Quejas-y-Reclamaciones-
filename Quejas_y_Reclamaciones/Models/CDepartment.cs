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
            var task = new Task<int>(() => 
            {
                try 
                {
                    if (_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();
                    int insertedId = 0;

                    _command = new SqlCommand($@"EXEC INSERTA_DEPARTAMENTO 
                                                        '{departmentName}'
                                                        ,{managerId};

                                                SELECT MAX(ID_DEPARTAMENTO) AS [COLUMN] FROM DEPARTAMENTO",_connection);
                    _reader = _command.ExecuteReader();

                    while(_reader.Read())
                        insertedId =int.Parse(_reader["COLUMN"].ToString());

                    return insertedId;

                }
                catch(Exception ex)
                {
                    throw new NotSupportedException(ex.Message);
                }
            });

            return await task;
        }

        public override Task<int> Update()
        {
            throw new NotImplementedException();
        }

        public async static Task<List<CDepartment>> Select(string searchString)
        {
            var task = new Task<List<CDepartment>>(()=> 
            {
                try 
                {
                    setConnection();
                    List<CDepartment> departments = new List<CDepartment>();
                    CDepartment department = null;

                    if (_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();

                    _command = new SqlCommand($"SELECT * FROM DEPARTAMENTO",_connection);

                    _reader = _command.ExecuteReader();

                    while(_reader.Read())
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
            });
            task.Start();
            var task2 = await task;
            return task2;
        }
    }
}
