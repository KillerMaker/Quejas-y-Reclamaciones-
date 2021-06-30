using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Quejas_y_Reclamaciones.Models
{
    public class CComplain:CEntity<int>
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
        public int idComplainType { get; set; }
        public int idState { get; set; }


        //Atributos opcionales
        public string stateTittle { get; set; }
        public string departmentName { get; set; }
        public string complainTypeName { get; set; }
        public string PersonName { get; set; }


        public CComplain(int? id, int idPerson, int idDepartment, string date, string description, int idComplainType,int idState ):base()
        {
            this.id = id;
            this.idPerson = idPerson;
            this.idDepartment = idDepartment;
            this.date = date;
            this.description = description;
            this.idComplainType = idComplainType;
            this.idState = idState;

            setConnection();
            _connection = connection;
        }

        public override async Task<int> Insert()
        {
            try
            {
                if (_connection.State == ConnectionState.Closed)
                    await _connection.OpenAsync();

                _command = new SqlCommand($@"EXEC INSERTA_QUEJA
                                            {idPerson},
                                            {idDepartment},
                                            '{date.SQLInyectionClearString()}',
                                            '{description.SQLInyectionClearString()}',
                                            {idComplainType},
                                            {idState};", _connection);

                return (await _command.ExecuteNonQueryAsync() != 0 ? 1 : 0);
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
                    _connection.Open();

                _command = new SqlCommand($@"UPDATE QUEJA SET
                                            DESCRIPCION_QUEJA = '{description.SQLInyectionClearString()}',
                                            ID_TIPO_QUEJA = {idComplainType},
                                            ID_ESTADO = {idState}
                                            WHERE ID_QUEJA = {id};", _connection);

                return (await _command.ExecuteNonQueryAsync() != 0 ? id.Value : 0);
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

                _command = new SqlCommand($@"EXEC ELIMINA_QUEJA {id}", _connection);

                return (await _command.ExecuteNonQueryAsync() != 0 ? id : 0);
            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }
            
        }

        public async static Task<List<CComplain>> Select(string searchString)
        {
            try
            {
                setConnection();
                _connection = connection;
                if (_connection.State.Equals(ConnectionState.Open))
                    await _connection.CloseAsync();

                await _connection.OpenAsync();

                List<CComplain> complains = new List<CComplain>();

                _command = new SqlCommand($"SELECT * FROM VISTA_QUEJA {searchString}", _connection);
                _reader = await _command.ExecuteReaderAsync();

                while (await _reader.ReadAsync())
                {
                    CComplain complain= new CComplain(
                                    int.Parse(_reader["ID_QUEJA"].ToString()),
                                    int.Parse(_reader["ID_PERSONA"].ToString()),
                                    int.Parse(_reader["ID_DEPARTAMENTO"].ToString()),
                                    _reader["FECHA_QUEJA"].ToString(),
                                    _reader["DESCRIPCION_QUEJA"].ToString(),
                                    int.Parse(_reader["ID_TIPO_QUEJA"].ToString()),
                                    int.Parse(_reader["ID_ESTADO"].ToString()))
                                    { 
                                        stateTittle=_reader["TITULO_ESTADO"].ToString(),
                                        departmentName=_reader["NOMBRE_DEPARTAMENTO"].ToString(),
                                        complainTypeName=_reader["TITULO_QUEJA"].ToString(),
                                        PersonName = _reader["NOMBRE_PERSONA"].ToString()
                    };

                    complains.Add(complain);
                }
                
                return complains;
            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }
            
        }
    }
}
