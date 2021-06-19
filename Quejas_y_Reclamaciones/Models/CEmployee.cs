using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Quejas_y_Reclamaciones.Models
{
    public class CEmployee :CPerson
    {
        //Atributos de Constructor
        public int idDepartment { get; set; }

        //Atributos opcionales
        public string departmentName { get; set; }
        public int idState { get; set; }

        //Construtor
        public CEmployee(int? id, string name, string birthDay, string idCard, string email, string phone, string genre, int idDepartment, CUser user = null)
            //Llamada al constructor padre (CPersona(...))
            : base(id, name, birthDay, idCard, email, phone, genre, user) 
                =>this.idDepartment=idDepartment;


        public async override Task<int> Update()
        {

            await base.Update();

            var task = new Task<int>(()=>
            {
                int rowCount = 0;
                try
                {
                    if (_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();

                    _command = new SqlCommand($@"UPDATE EMPLEADO SET 
                                            ID_DEPARTAMENTO={idDepartment}
                                            WHERE ID_PERSONA ={id};

                                            SELECT @@ROWCOUNT AS [COLUMN]", _connection);

                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                        rowCount =int.Parse(_reader["COLUMN"].ToString());

                    if (rowCount != 0)
                        return id.Value;
                    else
                        return rowCount;
                }
                catch (Exception ex)
                {
                    throw new NotSupportedException(ex.Message);
                }
            });

            task.Start();

            return await task;
            
        }

        public async override Task<CPerson> Insert()
        {
            var task = new Task<CPerson>(()=> 
            {
                if (_connection.State.Equals(ConnectionState.Closed))
                    _connection.Open();

                _command = new SqlCommand($@"EXEC INSERTA_EMPLEADO
                                              '{name}',
                                              '{birthDay}',
                                              '{idCard}',
                                              '{email}',
                                              '{phone}',
                                              '{genre}',
                                              '{user.userName}',
                                              '{user.password}',
                                               {idDepartment};

                                        SELECT * FROM VISTA_EMPLEADO 
                                              WHERE ID_PERSONA=(SELECT MAX (ID_PERSONA) FROM PERSONA_DEPARTAMENTO);", _connection);

                _reader = _command.ExecuteReader();

                CEmployee employee = null;

                while (_reader.Read())
                    employee = new CEmployee(
                                         int.Parse(_reader["ID_PERSONA"].ToString()),
                                         _reader["NOMBRE_PERSONA"].ToString(),
                                         _reader["FECHA_NAC_PERSONA"].ToString(),
                                         _reader["CEDULA_PERSONA"].ToString(),
                                         _reader["CORREO_PERSONA"].ToString(),
                                         _reader["TELEFONO_PERSONA"].ToString(),
                                         _reader["GENERO_PERSONA"].ToString(),
                                         int.Parse(_reader["ID_DEPARTAMENTO"].ToString()),
                                         new CUser(
                                                   int.Parse(_reader["ID_USUARIO"].ToString()),
                                                   _reader["NOMBRE_USUARIO"].ToString(),
                                                   _reader["CLAVE_USUARIO"].ToString(),
                                                   int.Parse(_reader["ID_TIPO_USUARIO"].ToString())));
                return employee;
            });

            task.Start();
            return await task;
            
        }
        public new static async Task<int> Delete(int id)
        {
            var task = new Task<Task<int>>(async()=> 
            {
                try
                {
                    setConnection();
                    _connection.Close();
                    if (_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();

                    _command = new SqlCommand($@"EXEC ELIMINA_EMPLEADO {id}", _connection);

                    await _command.ExecuteReaderAsync();
                    await CPerson.Delete(id);
                    _connection.Close();

                    return await CPerson.Delete(id);
                }
                catch (Exception ex)
                {
                    throw new NotSupportedException(ex.Message);
                }
            });

            task.Start();
            return await task.Result;
        }

        public async new static Task<List<CEmployee>> Select(string searchString)
        {
            var task = new Task<List<CEmployee>>(()=> 
            {
                try
                {
                    _connection = new SqlConnection("Data Source=DESKTOP-7V51383\\SQLEXPRESS;Initial Catalog=Quejas&Reclamaciones;Integrated Security=True");

                    if (_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();

                    List<CEmployee> employees = new List<CEmployee>();
                    CEmployee employee;

                    if(searchString== null)
                        _command = new SqlCommand($"SELECT * FROM VISTA_EMPLEADO ", _connection);
                    else
                        _command = new SqlCommand($"SELECT * FROM VISTA_EMPLEADO {searchString}", _connection);

                    _reader = _command.ExecuteReader();

                    while(_reader.Read())
                    {
                        employee = new CEmployee(
                                        int.Parse(_reader["ID_PERSONA"].ToString()),
                                        _reader["NOMBRE_PERSONA"].ToString(),
                                        _reader["FECHA_NAC_PERSONA"].ToString(),
                                        _reader["CEDULA_PERSONA"].ToString(),
                                        _reader["CORREO_PERSONA"].ToString(),
                                        _reader["TELEFONO_PERSONA"].ToString(),
                                        _reader["GENERO_PERSONA"].ToString(),
                                        int.Parse(_reader["ID_DEPARTAMENTO"].ToString()),
                                        new CUser(
                                                  int.Parse(_reader["ID_USUARIO"].ToString()),
                                                  _reader["NOMBRE_USUARIO"].ToString(),
                                                  _reader["CLAVE_USUARIO"].ToString(),
                                                  int.Parse(_reader["ID_TIPO_USUARIO"].ToString())))
                                                  {
                                                        departmentName=_reader["NOMBRE_DEPARTAMENTO"].ToString(),
                                                        idState=int.Parse(_reader["ID_ESTADO"].ToString())
                                                  };
                        employees.Add(employee);

                    }
                    return employees;

                }
                catch (Exception ex)
                {

                    throw new NotSupportedException(ex.Message);
                }
            });

            task.Start();
            return await task;
        }
    }
}
