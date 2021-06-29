using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Quejas_y_Reclamaciones.Models
{
    public class CClaim:CEntity<int>
    {
        protected static SqlConnection _connection;
        protected static SqlCommand _command;
        protected static SqlDataReader _reader;

        //Atributos de Constructor
        public int? id { get; set; }
        public int idPerson { get; set; }
        public int idDepartment { get; set; }
        public string date { get; set; }
        public string description { get; set; }
        public int claimType { get; set; }
        public int idState { get; set; }

        //Atributos Opcionales
        public string stateTittle { get; private set; }
        public string complainTypeName { get; private set; }
        public string departmentName { get; private set; }
        public string PersonName { get; set; }

        public CClaim(int? id,int idPerson,int idDepartment,string date,string description,int claimType,int idState)
        {
            this.id = id;
            this.idPerson = idPerson;
            this.idDepartment = idDepartment;
            this.date = date;
            this.description = description;
            this.claimType = claimType;
            this.idState = idState;

            setConnection();
            _connection = connection;
        }

        public override async Task<int> Insert()
        {
            try
            {
                if (_connection.State.Equals(ConnectionState.Closed))
                    await _connection.OpenAsync();

                _command = new SqlCommand($@"EXEC INSERTA_RECLAMACION 
                                                {idPerson},
                                                {idDepartment},
                                                '{date}',
                                                '{description}',
                                                {claimType},
                                                {idState};", _connection);

                return (await _command.ExecuteNonQueryAsync() != 0) ? id.Value : 0;

            }
            catch(Exception ex)
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

                _command = new SqlCommand($@"UPDATE RECLAMACION SET
                                                ID_DEPARTAMENTO = {idDepartment},
                                                DESCRIPCION_RECLAMACION = '{description}',
                                                ID_TIPO_RECLAMACION = {claimType},
                                                ID_ESTADO = {idState}
                                            WHERE ID_RECLAMACION ={id};", _connection);

                return (await _command.ExecuteNonQueryAsync() != 0) ? id.Value : 0;

            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }
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

                _command = new SqlCommand($@"EXEC ELIMINA_RECLAMACION {id};", _connection);

                return (await _command.ExecuteNonQueryAsync() != 0) ? id : 0;

            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }
        }

        public static async Task<List<CClaim>> Select(string searchString)
        {
            try
            {
                setConnection();
                _connection = connection;

                if (_connection.State.Equals(ConnectionState.Open))
                    await _connection.CloseAsync();

                await _connection.OpenAsync();

                List<CClaim> claims = new List<CClaim>();

                _command = new SqlCommand($"SELECT * FROM VISTA_RECLAMACION {searchString}", _connection);

                _reader = await _command.ExecuteReaderAsync();

                while (await _reader.ReadAsync())
                {
                    CClaim claim = new CClaim(
                                    int.Parse(_reader["ID_RECLAMACION"].ToString()),
                                    int.Parse(_reader["ID_PERSONA"].ToString()),
                                    int.Parse(_reader["ID_DEPARTAMENTO"].ToString()),
                                    _reader["FECHA_RECLAMACION"].ToString(),
                                    _reader["DESCRIPCION_RECLAMACION"].ToString(),
                                    int.Parse(_reader["ID_TIPO_RECLAMACION"].ToString()),
                                    int.Parse(_reader["ID_ESTADO"].ToString()))
                    {
                        stateTittle = _reader["TITULO_ESTADO"].ToString(),
                        departmentName = _reader["NOMBRE_DEPARTAMENTO"].ToString(),
                        complainTypeName = _reader["TITULO_RECLAMACION"].ToString(),
                        PersonName=_reader["NOMBRE_PERSONA"].ToString()
                    };

                    claims.Add(claim);
                }

                return claims;

            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }
        }
    }
}


