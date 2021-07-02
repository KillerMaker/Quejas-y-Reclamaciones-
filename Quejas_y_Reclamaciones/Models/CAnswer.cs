using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Quejas_y_Reclamaciones.Models
{
    public class CAnswer : CEntity<int>
    {
        public int? id { get; set; }
        public int employee { get; set; }
        public int? complain { get; set; }
        public int? claim { get; set; }
        [StringLength(200)]
        public string message { get; set; }
        [StringLength(8)]
        public string date { get; set; }

        private static SqlConnection _connection;
        private static SqlCommand _command;
        private static SqlDataReader _reader;

        public CAnswer(int? id,int employee,int? complain,int? claim,string message,string date)
        {
            this.id = id;
            this.employee = employee;
            this.complain = complain;
            this.claim = claim;
            this.message = message;
            this.date = date;

            setConnection();
            _connection = connection;
        }

        public async override Task<int> Insert()
        {
            try
            {
                setConnection();

                if (_connection.State.Equals(ConnectionState.Open))
                    await _connection.CloseAsync();

                await _connection.OpenAsync();

                if (claim.HasValue)
                    _command = new SqlCommand($@"EXEC INSERTA_RESPUESTA_QUEJA {employee},{claim},'{message}','{date}'", _connection);
                else if (complain.HasValue)
                    _command = new SqlCommand($@"EXEC INSERTA_RESPUESTA_RECLAMACION {employee},{complain},'{message}','{date}'", _connection);
                else if (!complain.HasValue && !claim.HasValue)
                    return 0;

                return (await _command.ExecuteNonQueryAsync() != 0) ? 1 : 0;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

        public async override Task<int> Update()
        {
            try
            {
                setConnection();

                if (_connection.State.Equals(ConnectionState.Open))
                    await _connection.CloseAsync();

                await _connection.OpenAsync();

                _command = new SqlCommand($@"UPDATE RESPUESTA SET
                                            SET ID_EMPLADO={employee},
                                                ID_QUEJA={complain},
                                                ID_RECLAMACION={claim},
                                                MENSAJE_RESPUESTA='{message}'
                                            WHERE ID_RESPUESTA={id.Value}", _connection);

                return (await _command.ExecuteNonQueryAsync() != 0) ? id.Value : 0;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public async static Task<int>Delete(int id)
        {
            try
            {
                setConnection();
                _connection = connection;
                if (_connection.State.Equals(ConnectionState.Open))
                    await _connection.CloseAsync();

                await _connection.OpenAsync();

                _command = new SqlCommand($@"", _connection);

                return (await _command.ExecuteNonQueryAsync() != 0) ? id : 0;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async static Task<List<CAnswer>>Select(string searchString)
        {
            try
            {
                setConnection();
                _connection = connection;

                if (_connection.State.Equals(ConnectionState.Open))
                    await _connection.CloseAsync();

                await _connection.OpenAsync();
                int claim;
                int complain;

                List<CAnswer> answers = new List<CAnswer>();
                CAnswer answer = null;

               
                _command = new SqlCommand($"SELECT * FROM VISTA_RESPUESTA {searchString}",_connection);
                _reader = await _command.ExecuteReaderAsync();

                while(await _reader.ReadAsync())
                {
                    int.TryParse(_reader["ID_QUEJA"].ToString(), out complain);
                    int.TryParse(_reader["ID_RECLAMACION"].ToString(),out claim);

                    answer = new CAnswer(
                        int.Parse(_reader["ID_RESPUESTA"].ToString()),
                        (int)_reader["ID_EMPLEADO"],
                        complain,
                        claim,
                        (string)_reader["MENSAJE_RESPUESTA"],
                        _reader["FECHA_RESPUESTA"].ToString());

                    answers.Add(answer);
                }
                return answers;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
