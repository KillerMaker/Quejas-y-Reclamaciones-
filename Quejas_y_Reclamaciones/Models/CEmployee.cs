using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Quejas_y_Reclamaciones.Models
{
    public class CEmployee :CPerson
    {
        //Atributos de Constructor

        public int idDepartment { get; set; }

        [StringLength(50)]
        public string departmentName { get; set; }
        public int idState { get; set; }

        public int managerId { get; set; }

        //Construtor
        public CEmployee(int? id, string name, string birthDay, string idCard, string email, string phone, string genre, int idDepartment, CUser user = null)
            //Llamada al constructor padre (CPersona(...))
            : base(id, name, birthDay, idCard, email, phone, genre, user) 
                =>this.idDepartment=idDepartment;


        public async override Task<int> Update()
        {
            try
            {
                await base.Update();

                if (_connection.State.Equals(ConnectionState.Closed))
                    _connection.Open();

                _command = new SqlCommand($@"UPDATE EMPLEADO SET 
                                        ID_DEPARTAMENTO={idDepartment}
                                        WHERE ID_PERSONA ={id};", _connection);

                return (await _command.ExecuteNonQueryAsync() != 0) ? id.Value : 0;

            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }
            
        }

        public async override Task<CPerson> Insert()
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
                                            {idDepartment};", _connection);

            return (await _command.ExecuteNonQueryAsync() != 0) ? this : null;
        }
        public new static async Task<int> Delete(int id)
        {
            try
            {
                setConnection();
                _connection = connection;

                if (_connection.State.Equals(ConnectionState.Open))
                    await _connection.CloseAsync();

                await _connection.OpenAsync();

                _command = new SqlCommand($@"EXEC ELIMINA_EMPLEADO {id}", _connection);

                return (await _command.ExecuteNonQueryAsync() != 0) ?await CPerson.Delete(id) : 0;
            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }

        }

        public async new static Task<List<CEmployee>> Select(string searchString)
        {

            try
            {
                setConnection();
                _connection = connection;

                if (_connection.State.Equals(ConnectionState.Open))
                    await _connection.CloseAsync();

                await _connection.OpenAsync();

                List<CEmployee> employees = new List<CEmployee>();
                CEmployee employee;


                _command = new SqlCommand($"SELECT * FROM VISTA_EMPLEADO {searchString}", _connection);

                _reader =await _command.ExecuteReaderAsync();

                while(await _reader.ReadAsync())
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
                                                    idState=int.Parse(_reader["ID_ESTADO"].ToString()),
                                                    managerId=(int)_reader["ID_EMPLEADO"]
                                                };
                    employees.Add(employee);

                }
                return employees;

            }
            catch (Exception ex)
            {

                throw new NotSupportedException(ex.Message);
            }

        }
    }
}
