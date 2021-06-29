using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Web.Helpers;

namespace Quejas_y_Reclamaciones.Models
{
    public class CLogin
    {
        public string userName { get; set; }
        public string password { get; set; }

        private SqlConnection _connection;
        private SqlCommand _command;
        private SqlDataReader _reader;

        public CLogin(string userName, string password)
        {
            this.userName = userName;
            this.password = password;

            _connection = new SqlConnection("Data Source = DESKTOP-7V51383\\SQLEXPRESS; Initial Catalog = Quejas&Reclamaciones; Integrated Security = True");

        }
        public async Task<CPerson> loginIntoApplication()
        {
            var task = new Task<CPerson>(()=> 
            {
                try
                {
                    _connection.Close();
                    _connection.Open();

                    _command = new SqlCommand($@"SELECT * FROM PERSONA P INNER JOIN USUARIO U ON U.ID_PERSONA=P.ID_PERSONA
                                                WHERE U.NOMBRE_USUARIO='{userName.SQLInyectionClearString()}' AND U.CLAVE_USUARIO='{password.SQLInyectionClearString()}'", _connection);
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
                                            new CUser(
                                                        int.Parse(_reader["ID_USUARIO"].ToString()),
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

        public async Task<bool>CheckUserName()
        {
           
               var task= new Task<bool>(()=> 
               {
                   
                   try
                   {
                       int state = 0;
                       _connection.Close();
                       _connection.Open();

                        _command = new SqlCommand($@"IF EXISTS (SELECT TOP 1 * FROM USUARIO WHERE NOMBRE_USUARIO ='{userName.SQLInyectionClearString()}')
                                                        SELECT 1 AS EXISTENCIA_USUARIO
                                                    ELSE
                                                        SELECT 0 AS EXISTENCIA_USUARIO", _connection);

                        _reader = _command.ExecuteReader();
                        while (_reader.Read())
                            state = int.Parse(_reader["EXISTENCIA_USUARIO"].ToString());

                        if (state == 0)
                            return false;
                        else
                            return true;
                    }

                    catch (Exception ex)
                    {
                        throw new NotSupportedException(ex.Message);
                    }
               });

            task.Start();
            return await task; 
           
        }
        public async Task<bool> CheckPassword()
        {
            var task = new Task<bool>(()=> 
            {
                try
                {
                    int state = 0;
                    _connection.Close();
                    _connection.Open();

                    _command = new SqlCommand($@"IF EXISTS (SELECT TOP 1 * FROM USUARIO WHERE NOMBRE_USUARIO='{userName.SQLInyectionClearString()}' AND CLAVE_USUARIO ='{password.SQLInyectionClearString()}')
                                                        SELECT 1 AS USUARIO_VALIDO
                                                        ELSE
                                                        SELECT 0 AS USUARIO_VALIDO", _connection);
                    _reader = _command.ExecuteReader();
                    while (_reader.Read())
                        state = int.Parse(_reader["USUARIO_VALIDO"].ToString());

                    if (state == 0)
                        return false;

                    else
                        return true;
                }
                catch(Exception ex)
                {
                    throw new NotSupportedException(ex.Message);
                }
            });

            task.Start();
            return await task;
        }
            
            
    }
}

