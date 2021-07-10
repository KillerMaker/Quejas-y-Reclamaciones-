using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Quejas_y_Reclamaciones.Models
{
    public class CComplainType : CEntity<int>
    {
        protected static SqlConnection _connection;
        protected static SqlCommand _command;
        protected static SqlDataReader _reader;
        public int? id { get; set; }

        [StringLength(50)]
        public string tittle { get; set;}

        [StringLength(200)]
        public string description { get; set;}
        public int stateId { get; set; }

        public CComplainType(int? id, string tittle, string description, int stateId)
        {
            this.id = id;
            this.tittle = tittle;
            this.description = description;
            this.stateId = stateId;

            setConnection();
            _connection = connection;
        }

        public async override Task<int> Insert()
        {
            try
            {
                if (_connection.State.Equals(ConnectionState.Closed))
                   await _connection.OpenAsync();

                _command = new SqlCommand($@"EXEC INSERTA_TIPO_QUEJA
                                                    '{description.SQLInyectionClearString()}',
                                                    '{tittle.SQLInyectionClearString()}',
                                                    {stateId};", _connection);

                return (await _command.ExecuteNonQueryAsync() != 0) ? 1 : 0;
            }
            catch ( Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }
        }

        public async override Task<int> Update()
        {

            try
            {
                if (_connection.State.Equals(ConnectionState.Closed))
                   await _connection.OpenAsync();

                _command = new SqlCommand($@"UPDATE TIPO_QUEJA SET 
                                                DESCRIPCION_QUEJA={description.SQLInyectionClearString()},
                                                TITULO_QUEJA ={tittle.SQLInyectionClearString()},
                                                ID_ESTADO = {stateId}
                                                WHERE ID_QUEJA={id};", _connection);

                return (await _command.ExecuteNonQueryAsync() != 0) ? id.Value : 0;
            }
            catch (Exception ex)
            {

                throw new NotSupportedException(ex.Message);
            }
        }
        
        public async static Task<List<CComplainType>> Select(string searchString)
        {

            try
            {
                setConnection();
                _connection = connection;

                if (_connection.State.Equals(ConnectionState.Open))
                    await _connection.CloseAsync();

                await _connection.OpenAsync();

                List<CComplainType> ComplainTypes = new List<CComplainType>();
                CComplainType ComplainType;

                _command = new SqlCommand($"SELECT * FROM TIPO_QUEJA {searchString}", _connection);

                _reader = await _command.ExecuteReaderAsync();

                while (await _reader.ReadAsync())
                {
                    ComplainType = new CComplainType(
                                        int.Parse(_reader["ID_TIPO_QUEJA"].ToString()),
                                        _reader["TITULO_QUEJA"].ToString(),
                                        _reader["DESCRIPCION_QUEJA"].ToString(),
                                        int.Parse(_reader["ID_ESTADO"].ToString())
                        );
                    ComplainTypes.Add(ComplainType);
                }

                return ComplainTypes;
            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }

        }
        public async static Task<int>Delete(int id)
        {
            try
            {
                if (_connection.State.Equals(ConnectionState.Closed))
                    await _connection.OpenAsync();

                _command = new SqlCommand($@"DELETE FROM TIPO_QUEJA WHERE ID ={id}", _connection);

                return (await _command.ExecuteNonQueryAsync() != 0) ? id: 0;
            }
            catch (Exception ex)
            {

                throw new NotSupportedException(ex.Message);
            }
        }
    }
}

