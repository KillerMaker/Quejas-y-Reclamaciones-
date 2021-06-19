using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Quejas_y_Reclamaciones.Models
{
    public class CClaimType:CEntity<int>
    {
        protected static SqlConnection _connection;
        protected static SqlCommand _command;
        protected static SqlDataReader _reader;
        public int? id { get; set; }
        public string tittle { get; set; }
        public string description { get; set; }
        public int stateId { get; set; }

        public CClaimType(int? id, string tittle, string description, int stateId)
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
            var task = new Task<int>(() =>
            {
                try
                {

                    int rowCount = 0;

                    if (_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();

                    _command = new SqlCommand($@"EXEC INSERTA_TIPO_RECLAMACION
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
                catch (Exception ex)
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

                    _command = new SqlCommand($@"UPDATE TIPO_RECLAMACION SET 
                                                    DESCRIPCION_RECLAMACION={description},
                                                    TITULO_RECLAMACION ={tittle},
                                                    ID_ESTADO = {stateId}
                                                    WHERE ID_TIPO_RECLAMACION={id};
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

        public async static Task<List<CClaimType>> Select(string searchString)
        {
            var task = new Task<List<CClaimType>>(() =>
            {
                try
                {
                    setConnection();
                    _connection = connection;

                    List<CClaimType> ClaimTypes = new List<CClaimType>();
                    CClaimType ClaimType;

                    if (_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();

                    if (searchString == null)
                        _command = new SqlCommand($"SELECT * FROM TIPO_RECLAMACION", _connection);
                    else
                        _command = new SqlCommand($"SELECT * FROM TIPO_RECLAMACION {searchString}", _connection);

                    //_command.ExecuteNonQuery();
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        ClaimType = new CClaimType(
                                           int.Parse(_reader["ID_TIPO_RECLAMACION"].ToString()),
                                           _reader["TITULO_RECLAMACION"].ToString(),
                                           _reader["DESCRIPCION_RECLAMACION"].ToString(),
                                           int.Parse(_reader["ID_ESTADO"].ToString())
                            );
                        ClaimTypes.Add(ClaimType);
                    }
                    _connection.Close();
                    return ClaimTypes;
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

