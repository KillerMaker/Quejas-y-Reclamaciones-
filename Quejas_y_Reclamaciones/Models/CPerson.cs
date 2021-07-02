using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Quejas_y_Reclamaciones.Models
{
    public class CPerson:CEntity<CPerson>
    {
        protected static SqlConnection _connection;
        protected static SqlCommand _command;
        protected static SqlDataReader _reader;
        public CUser user { get; set; }
        public int? id { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(8)]
        public string birthDay { get; set; }
        [StringLength(11)]
        public string idCard { get; set; }

        [StringLength(70)]
        public string email { get; set; }

        [StringLength(10)]
        public string phone { get; set; }

        [StringLength(1)]
        public string genre { get; set; }
        
        [JsonConstructor]
        public CPerson(int? id,string name, string birthDay, string idCard, string email, string phone, string genre, CUser user=null)
        {
            this.id = id;
            this.name = name;
            this.birthDay = birthDay;
            this.idCard = idCard;
            this.email = email;
            this.phone = phone;
            this.genre = genre;
            this.user = user;

            setConnection();
            _connection = connection;
        }
        public static async Task<int> Delete(int id)
        {
            try
            {
                setConnection();
                _connection = connection;

                if (_connection.State.Equals(ConnectionState.Open))
                   await _connection.CloseAsync();

                await _connection.OpenAsync();

                _command = new SqlCommand($@"EXEC ELIMINA_PERSONA_USUARIO {id};", _connection);

                return (await _command.ExecuteNonQueryAsync() != 0) ? id : 0;

            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }
            
        }

        public async override Task<CPerson> Insert()
        {
            try
            {
                if (_connection.State.Equals(ConnectionState.Closed))
                   await _connection.OpenAsync();

                _command = new SqlCommand($@"EXEC INSERTA_PERSONA
                                                '{name.SQLInyectionClearString()}',
                                                '{birthDay.SQLInyectionClearString()}',
                                                '{idCard.SQLInyectionClearString()}',
                                                '{email.SQLInyectionClearString()}',
                                                '{phone.SQLInyectionClearString()}',
                                                '{genre.SQLInyectionClearString()}',
                                                '{user.userName.SQLInyectionClearString()}',
                                                '{user.password.SQLInyectionClearString()}',
                                                    {user.userType};", _connection);

                return (await _command.ExecuteNonQueryAsync() != 0) ? this : null;

            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }  
        }   
        

        public override async Task<int> Update()
        {

            try
            {
                if (_connection.State.Equals(ConnectionState.Closed))
                   await _connection.OpenAsync();

                _command = new SqlCommand($@"UPDATE PERSONA SET
                                                NOMBRE_PERSONA = '{name.SQLInyectionClearString()}',
                                                FECHA_NAC_PERSONA = '{birthDay.SQLInyectionClearString()}',
                                                CEDULA_PERSONA = '{idCard.SQLInyectionClearString()}',
                                                CORREO_PERSONA = '{email.SQLInyectionClearString()}',
                                                TELEFONO_PERSONA = '{phone.SQLInyectionClearString()}',
                                                GENERO_PERSONA = '{genre.SQLInyectionClearString()}'
                                                WHERE ID_PERSONA = {id}", _connection);

                return (await _command.ExecuteNonQueryAsync() != 0) ? id.Value : 0;
            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            } 
        }

        public async static Task<List<CPerson>> Select(string searchString)
        {
            try
            {
                setConnection();
                _connection = connection;

                if (_connection.State.Equals(ConnectionState.Open))
                    await _connection.CloseAsync();

                await _connection.OpenAsync();

                List<CPerson> people = new List<CPerson>();
                searchString = (searchString != null) ? searchString +="AND ID_PERSONA NOT IN(SELECT ID_PERSONA FROM EMPLEADO)" : "WHERE ID_PERSONA NOT IN(SELECT ID_PERSONA FROM EMPLEADO)";

                _command = new SqlCommand($"SELECT * FROM PERSONA {searchString}", _connection);
                _reader = await _command.ExecuteReaderAsync();

                while (await _reader.ReadAsync())
                {
                    people.Add(new CPerson(int.Parse(_reader["ID_PERSONA"].ToString()),
                                            _reader["NOMBRE_PERSONA"].ToString(),
                                            _reader["FECHA_NAC_PERSONA"].ToString(),
                                            _reader["CEDULA_PERSONA"].ToString(),
                                            _reader["CORREO_PERSONA"].ToString(),
                                            _reader["TELEFONO_PERSONA"].ToString(),
                                            _reader["GENERO_PERSONA"].ToString())
                                );
                }
                return people;
            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }
            
        }
    }
}
