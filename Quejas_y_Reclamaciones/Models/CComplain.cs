using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Quejas_y_Reclamaciones.Interfaces;

namespace Quejas_y_Reclamaciones.Models
{
    public class CComplain:IEntityInterface<CComplain>
    {
        public int? id { get; set; }
        public int idPerson { get; set; }
        public int idDepartment { get; set; }
        public string date { get; set; }
        public string description { get; set; }
        public int idComplainType { get; set; }
        public int idState { get; set; }

        private SqlConnection _connection;
        private SqlCommand _command;
        private SqlDataReader _reader;

        public CComplain(int? id, int idPerson, int idDepartment, string date, string description, int idComplainType,int idState )
        {
            this.id = id;
            this.idPerson = idPerson;
            this.idDepartment = idDepartment;
            this.date = date;
            this.description = description;
            this.idComplainType = idComplainType;
            this.idState = idState;

            _command = new SqlCommand("Data Source = DESKTOP-7V51383\\SQLEXPRESS; Initial Catalog = Quejas&Reclamaciones; Integrated Security = True");
        }

        public string Insert()
        {
            string message = "";
            try
            {
                if (_connection.State.Equals(ConnectionState.Closed))
                    _connection.Open();

                _command = new SqlCommand($@"EXEC INSERTA_QUEJA
                                            {idPerson},
                                            {idDepartment},
                                           '{date}',
                                           '{description}',
                                            {idComplainType},
                                            {idState};
                                        EXEC ERROR_MESSAGES;", _connection);

                _reader = _command.ExecuteReader();

                while (_reader.Read())
                    message = _reader["text"].ToString();

                return message;
            }
            catch(Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }
        }

        public string Update()
        {
            string message = "";
            try
            {
                if (_connection.State.Equals(ConnectionState.Closed))
                    _connection.Open();

                _command = new SqlCommand($@"UPDATE QUEJA SET
                                                DESCRIPCION_QUEJA = '{description}',
                                                ID_TIPO_QUEJA = {idComplainType},
                                                ID_ESTADO = {idState}
                                                WHERE ID_QUEJA = {id};
                                        EXEC ERROR_MESSAGES;", _connection);

                _reader = _command.ExecuteReader();

                while (_reader.Read())
                    message = _reader["text"].ToString();

                return message;
            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }
        }

        public string Delete()
        {
            throw new NotImplementedException();
        }
    }
}
