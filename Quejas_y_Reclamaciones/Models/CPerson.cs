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

        //public CPerson(int id, string name, string birthDay, string idCard, string email, string phone, string genre)
        //{
        //    this.id = id;
        //    this.name = name;
        //    this.birthDay = birthDay;
        //    this.idCard = idCard;
        //    this.email = email;
        //    this.phone = phone;
        //    this.genre = genre;

        //    _connection = new SqlConnection("Data Source=DESKTOP-7V51383\\SQLEXPRESS;Initial Catalog=Quejas&Reclamaciones;Integrated Security=True");
        //}
        
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
        }
        public static async Task<int> Delete(int id)
        {
            var task = new Task<int>(()=> 
            {
                try
                {
                    int rowCount = 0;

                    setConnection();
                    if (_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();

                    _command = new SqlCommand($@"EXEC ELIMINA_PERSONA_USUARIO {id}; 
                                                SELECT @@ROWCOUNT AS [COLUMN];", _connection);
                    //_command.ExecuteNonQuery();
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                       rowCount =int.Parse(_reader["COLUMN"].ToString());

                    if (rowCount != 0)
                        return id;
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
                try
                {
                    if (_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();

                    _command = new SqlCommand($@"EXEC INSERTA_PERSONA
                                                    '{name.SQLInyectionClearString()}',
                                                    '{birthDay.SQLInyectionClearString()}',
                                                    '{idCard.SQLInyectionClearString()}',
                                                    '{email.SQLInyectionClearString()}',
                                                    '{phone.SQLInyectionClearString()}',
                                                    '{genre.SQLInyectionClearString()}',
                                                    '{user.userName.SQLInyectionClearString()}',
                                                    '{user.password.SQLInyectionClearString()}',
                                                        {user.userType};
                                                SELECT * FROM PERSONA P 
                                                        INNER JOIN USUARIO U ON U.ID_PERSONA = P.ID_PERSONA 
                                                        WHERE P.ID_PERSONA =(SELECT MAX(ID_PERSONA)FROM PERSONA);", _connection);
                    //_command.ExecuteNonQuery();
                    _reader = _command.ExecuteReader();

                    CPerson person = null;

                    while (_reader.Read())
                        person = new CPerson(int.Parse(_reader["ID_PERSONA"].ToString()),
                                        _reader["NOMBRE_PERSONA"].ToString(),
                                        _reader["FECHA_NAC_PERSONA"].ToString(),
                                        _reader["CEDULA_PERSONA"].ToString(),
                                        _reader["CORREO_PERSONA"].ToString(),
                                        _reader["TELEFONO_PERSONA"].ToString(),
                                        _reader["GENERO_PERSONA"].ToString(),
                                        new CUser(int.Parse(_reader["ID_USUARIO"].ToString()),
                                                    _reader["NOMBRE_USUARIO"].ToString(),
                                                    _reader["CLAVE_USUARIO"].ToString(),
                                                    int.Parse(_reader["ID_TIPO_USUARIO"].ToString())));
                    return person;
                }
                catch (Exception ex)
                {
                    throw new NotSupportedException(ex.Message);
                }
            });

            task.Start();

            return await task;
            
        }   
        

        public override async Task<int> Update()
        {
            var task = new Task<int>(() => 
            {
                try
                {
                    int rowCount = 0;

                    if (_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();

                    _command = new SqlCommand($@"UPDATE PERSONA SET
                                                    NOMBRE_PERSONA = '{name.SQLInyectionClearString()}',
                                                    FECHA_NAC_PERSONA = '{birthDay.SQLInyectionClearString()}',
                                                    CEDULA_PERSONA = '{idCard.SQLInyectionClearString()}',
                                                    CORREO_PERSONA = '{email.SQLInyectionClearString()}',
                                                    TELEFONO_PERSONA = '{phone.SQLInyectionClearString()}',
                                                    GENERO_PERSONA = '{genre.SQLInyectionClearString()}'
                                                    WHERE ID_PERSONA = {id}
                                                    
                                                SELECT @@ROWCOUNT AS [COLUMN];", _connection);
                    //_command.ExecuteNonQuery();
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                        rowCount = int.Parse(_reader["COLUMN"].ToString());

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

        public async static Task<List<CPerson>> Select(string searchString)
        {
            var task = new Task<List<CPerson>>(() =>
             {
                 try
                 {
                     setConnection();
                     List<CPerson> people = new List<CPerson>();

                     if (_connection.State.Equals(ConnectionState.Closed))
                         _connection.Open();

                     if (searchString == null)
                         _command = new SqlCommand($"SELECT * FROM PERSONA", _connection);
                     else
                         _command = new SqlCommand($"SELECT * FROM PERSONA {searchString}", _connection);

                     //_command.ExecuteNonQuery();
                     _reader = _command.ExecuteReader();

                     while (_reader.Read())
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
             });

            task.Start();
            return await task;
            
        }
    }
}
