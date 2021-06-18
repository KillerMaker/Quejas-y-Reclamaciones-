using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Quejas_y_Reclamaciones.Models
{
    public class CComplainType : CEntity<int>
    {
        public int? id { get; set; }
        public string tittle { get; set;}
        public string description { get; set;}
        public int stateId { get; set; }

        public CComplainType(int? id, string tittle, string description, int stateId)
        {
            this.id = id;
            this.tittle = tittle;
            this.description = description;
            this.stateId = stateId;

            setConnection();
        }

        public async override Task<int> Insert()
        {
            var task = new Task<int>(()=> 
            {
                try
                {

                    int rowCount = 0;

                    if (_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();

                    _command = new SqlCommand($@"EXEC INSERTA_TIPO_QUEJA
                                                      '{description}',
                                                      '{tittle}',
                                                       {stateId};

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
                catch ( Exception ex)
                {

                    throw new NotSupportedException(ex.Message);
                }
            });

            return await task;
        }

        public async override Task<int> Update()
        {
            var task = new Task<int>(() =>
            {
                try
                {
                    int rowCount = 0;

                    if (_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();

                    _command = new SqlCommand($@"UPDATE TIPO_QUEJA SET 
                                                    DESCRIPCION_QUEJA={description},
                                                    TITULO_QUEJA ={tittle},
                                                    ID_ESTADO = {stateId};

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

            return await task;
        }
        
        public async static Task<List<CComplainType>> Select(string searchString)
        {
            var task = new Task<List<CComplainType>>(() =>
            {
                try
                {
                    setConnection();
                    List<CComplainType> ComplainTypes = new List<CComplainType>();
                    CComplainType ComplainType;

                    if (_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();

                    if (searchString == null)
                        _command = new SqlCommand($"SELECT * FROM TIPO_QUEJA", _connection);
                    else
                        _command = new SqlCommand($"SELECT * FROM TIPO_QUEJA {searchString}", _connection);

                    //_command.ExecuteNonQuery();
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
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
            });

            task.Start();
            return await task;
        }
    }
}
