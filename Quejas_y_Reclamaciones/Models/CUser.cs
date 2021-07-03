using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace Quejas_y_Reclamaciones.Models
{
    public class CUser
    {
        public int? id { get; set; }

        [StringLength(50)]
        public string userName { get; set; }
        [StringLength(50)]
        public string password { get; set; }
        public int? userType { get; set; }

        private SqlConnection _connection;
        private SqlCommand _command;
        private SqlDataReader _reader;

        public CUser(int? id,string userName, string password, int? userType)
        {
            this.id = id;
            this.userName = userName;
            this.password = password;
            this.userType = userType;

            _connection = new SqlConnection("Data Source=DESKTOP-T76LFOU;Initial Catalog=Quejas&Reclamaciones;Integrated Security=True");
            //_connection = new SqlConnection("Data Source = DESKTOP-7V51383\\SQLEXPRESS; Initial Catalog = Quejas&Reclamaciones; Integrated Security = True");
        }

        /// <summary>
        /// Ejecuta la accion de loguear al usuario.
        /// Es preferible que ya se haya verificado la existencia del usuario antes de ejecutar esta funcion
        /// </summary>
        /// <returns>Los datos de la persona con nombre de usuario y clave similares</returns>
        public async Task<CPerson> loginIntoApplication()
        {
            var task = new Task<CPerson>(() =>
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

        /// <summary>
        /// Verifica que el nombre de usuario exista en la base de datos
        /// </summary>
        /// <returns>Retorna True si el nombre de usuario existe y false de lo contrario</returns>
        public async Task<bool> CheckUserName()
        {

            var task = new Task<bool>(() =>
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

        /// <summary>
        /// Verifica que la clave y el nombre de usuario existan en la base de datos.
        /// Es preferible que se utilice el metodo CheckUsername antes que este para verificar el nombre de usuario.
        /// </summary>
        /// <returns>Retorna True si existe un registro con el nombre de usuario y clave similares y false de lo contrario</returns>
        public async Task<bool> CheckPassword()
        {
            var task = new Task<bool>(() =>
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

