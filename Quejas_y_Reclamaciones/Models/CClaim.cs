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
        }

        public override async Task<int> Insert()
        {
            var task = new Task<int>(() => 
            {
                try
                {
                    if (_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();

                    int insertedID=0;

                    _command = new SqlCommand($@"EXEC INSERTA_RECLAMACION 
                                                    {idPerson},
                                                    {idDepartment},
                                                    '{date}',
                                                    '{description}',
                                                    {claimType},
                                                    {idState};
                                                
                                                SELECT MAX(ID_RECLAMACION) FROM RECLAMACION;", _connection);

                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                        insertedID = int.Parse(_reader["ID_RECLAMACION"].ToString());

                    return insertedID;
                }
                catch(Exception ex)
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
                try
                {
                    if (_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();

                    int rowCount = 0;

                    _command = new SqlCommand($@"UPDATE RECLAMACION SET
                                                   ID_DEPARTAMENTO = {idDepartment},
                                                   DESCRIPCION_RECLAMACION = '{description}',
                                                   ID_TIPO_RECLAMACION = {claimType},
                                                   ID_ESTADO = {idState}
                                                WHERE ID_RECLAMACION ={id};

                                               SELECT @@ROWCOUNT AS [COLUMN];", _connection);

                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                        rowCount =int.Parse( _reader["COLUMN"].ToString());

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
            var task = new Task<int>(() =>
            {
                try
                {
                    setConnection();
                    _connection.Close();
                    if (_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();

                    int rowCount = 0;

                    _command = new SqlCommand($@"EXEC ELIMINA_RECLAMACION {id};
                                                
                                                SELECT @@ROWCOUNT AS [COLUMN];", _connection);

                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                        rowCount =int.Parse(_reader["COLUMN"].ToString());

                    if(rowCount!=0)
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
        public static async Task<List<CClaim>> Select(string searchString)
        {
            var task = new Task<List<CClaim>>(() =>
            {
                try
                {
                    setConnection();
                    _connection.Close();
                    List<CClaim> claims = new List<CClaim>();
                    _connection = new SqlConnection("Data Source = DESKTOP-7V51383\\SQLEXPRESS; Initial Catalog = Quejas&Reclamaciones; Integrated Security = True");

                    if (_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();

                    if (searchString == null)
                        _command = new SqlCommand("SELECT * FROM VISTA_RECLAMACION", _connection);
                    else
                        _command = new SqlCommand($"SELECT * FROM VISTA_RECLAMACION {searchString}", _connection);

                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
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
            });

            task.Start();
            return await task;
        }
    }

}
