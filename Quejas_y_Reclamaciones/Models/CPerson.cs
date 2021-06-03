using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Quejas_y_Reclamaciones.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.Json.Serialization;

namespace Quejas_y_Reclamaciones.Models
{
    public class CPerson : IEntityInterface<CPerson>
    {
        private static SqlConnection _connection;
        private static SqlCommand _command;
        private static SqlDataReader _reader;

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

            _connection = new SqlConnection("Data Source=DESKTOP-7V51383\\SQLEXPRESS;Initial Catalog=Quejas&Reclamaciones;Integrated Security=True");
        }
        public string Delete()
        {
            if (id == null)
                return "Datos de la persona insuficientes";
            try
            {
                string message = "";

                if (_connection.State.Equals(ConnectionState.Closed))
                    _connection.Open();

                _command = new SqlCommand($@"EXEC ELIMINA_PERSONA_USUARIO {id}; 
                                             EXEC ERROR_MESSAGES;", _connection);
                //_command.ExecuteNonQuery();
                _reader = _command.ExecuteReader();

                while (_reader.Read())
                    message = _reader["Text"].ToString();

                return message;
            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }
        }

        public string Insert()
        {
            if (user==null)
                return "Datos de usuario Insuficientes";
            else
            {
                try
                {
                    string message = "";

                    if (_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();

                    _command = new SqlCommand($@"EXEC INSERTA_PERSONA
                                                        '{name}',
                                                        '{birthDay}',
                                                        '{idCard}',
                                                        '{email}',
                                                        '{phone}',
                                                        '{genre}',
                                                        '{user.userName}',
                                                        '{user.password}',
                                                         {user.userType};
                                             EXEC ERROR_MESSAGES;", _connection);
                    //_command.ExecuteNonQuery();
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                        message = _reader["Text"].ToString();

                    return message;
                }
                catch (Exception ex)
                {
                    throw new NotSupportedException(ex.Message);
                }
            }   
        }

        public string Update()
        {
            if (id == null)
                return "Datos de la persona insuficientes";
            try
            {
                string message = "";

                if (_connection.State.Equals(ConnectionState.Closed))
                    _connection.Open();

                _command = new SqlCommand($@"UPDATE PERSONA SET
                                                    NOMBRE_PERSONA = '{name}',
                                                    FECHA_NAC_PERSONA = '{birthDay}',
                                                    CEDULA_PERSONA = '{idCard}',
                                                    CORREO_PERSONA = '{email}',
                                                    TELEFONO_PERSONA = '{phone}',
                                                    GENERO_PERSONA = '{genre}'
                                                    WHERE ID_PERSONA = {id}
                                                    
                                             EXEC ERROR_MESSAGES;", _connection);
                //_command.ExecuteNonQuery();
                _reader = _command.ExecuteReader();

                while (_reader.Read())
                    message = _reader["Text"].ToString();

                return message;
            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }
        }

        public static List<CPerson> Select(string searchString)
        {
            try
            {
                _connection = new SqlConnection("Data Source=DESKTOP-7V51383\\SQLEXPRESS;Initial Catalog=Quejas&Reclamaciones;Integrated Security=True");

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
        }
    }
}
