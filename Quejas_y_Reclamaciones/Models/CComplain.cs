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
        }

        public override async Task<int> Insert()
        {
            var task = new Task<int>(()=> 
            {
                int insertedId = 0;
                try
                {
                    if (_connection.State == ConnectionState.Closed)
                        _connection.Open();

                    _command = new SqlCommand($@"EXEC INSERTA_QUEJA
                                                {idPerson},
                                                {idDepartment},
                                               '{date.SQLInyectionClearString()}',
                                               '{description.SQLInyectionClearString()}',
                                                {idComplainType},
                                                {idState};
                                            SELECT MAX(ID_QUEJA) FROM QUEJA;", _connection);

                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                        insertedId = int.Parse(_reader["text"].ToString());

                    return insertedId;
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
                int rowCount=0;
                try
                {
                    if (_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();

                    _command = new SqlCommand($@"UPDATE QUEJA SET
                                                DESCRIPCION_QUEJA = '{description.SQLInyectionClearString()}',
                                                ID_TIPO_QUEJA = {idComplainType},
                                                ID_ESTADO = {idState}
                                                WHERE ID_QUEJA = {id};

                                                SELECT @@ROWCOUNT AS [COLUMN];", _connection);

                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                        rowCount =int.Parse(_reader["COLUMN"].ToString());

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

        public static async Task<int> Delete(int id)
        {
            var task= new Task<int>(() =>
            {
                int rowCount = 0;
                try
                {
                    setConnection();
                    if(_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();

                    _command = new SqlCommand($@"EXEC ELIMINA_QUEJA {id}
                                           SELECT @@ROWCOUNT AS [COLUMN];", _connection);

                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                        rowCount =int.Parse( _reader["text"].ToString());

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

        public async static Task<List<CComplain>> Select(string searchString)
        {
            
            var task = new Task<List<CComplain>>(() => 
            {
                try
                {
                    List<CComplain> complains = new List<CComplain>();
                    setConnection();

                    if (_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();

                    if (searchString == null)
                        _command = new SqlCommand("SELECT * FROM VISTA_QUEJA", _connection);
                    else
                        _command = new SqlCommand($"SELECT * FROM VISTA_QUEJA {searchString}", _connection);

                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
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
            });

            task.Start();
            return await task;
            
        }
    }
}
