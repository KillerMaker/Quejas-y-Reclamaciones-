﻿using System;
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

        private static SqlConnection _connection;
        private static SqlCommand _command;
        private static SqlDataReader _reader;

        public CComplain(int? id, int idPerson, int idDepartment, string date, string description, int idComplainType,int idState )
        {
            this.id = id;
            this.idPerson = idPerson;
            this.idDepartment = idDepartment;
            this.date = date;
            this.description = description;
            this.idComplainType = idComplainType;
            this.idState = idState;

            _connection = new SqlConnection("Data Source = DESKTOP-7V51383\\SQLEXPRESS; Initial Catalog = Quejas&Reclamaciones; Integrated Security = True");
        }

        public object Insert()
        {
            string message = "";
            try
            {
                if (_connection.State==ConnectionState.Closed)
                    _connection.Open();

                _command = new SqlCommand($@"EXEC INSERTA_QUEJA
                                                {idPerson},
                                                {idDepartment},
                                               '{date.SQLInyectionClearString()}',
                                               '{description.SQLInyectionClearString()}',
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
                                                DESCRIPCION_QUEJA = '{description.SQLInyectionClearString()}',
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
            string message = "";
            try
            {
                if (_connection.State.Equals(ConnectionState.Closed))
                    _connection.Open();

                _command = new SqlCommand($@"DELETE FROM QUEJA WHERE ID_QUEJA ={id}
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

        public static List<CComplain> Select(string searchString)
        {
            try
            {
                List<CComplain> complains = new List<CComplain>();
                _connection = new SqlConnection("Data Source = DESKTOP-7V51383\\SQLEXPRESS; Initial Catalog = Quejas&Reclamaciones; Integrated Security = True");
                
                if (_connection.State.Equals(ConnectionState.Closed))
                    _connection.Open();

                if (searchString == null)
                    _command = new SqlCommand("SELECT * FROM VISTA_QUEJA", _connection);
                else
                    _command = new SqlCommand($"SELECT * FROM VISTA_QUEJA {searchString}", _connection);

                _reader = _command.ExecuteReader();

                while (_reader.Read())
                    complains.Add(new CComplain(
                                  int.Parse(_reader["ID_QUEJA"].ToString()),
                                  int.Parse(_reader["ID_PERSONA"].ToString()),
                                  int.Parse(_reader["ID_DEPARTAMENTO"].ToString()),
                                  _reader["FECHA_QUEJA"].ToString(),
                                  _reader["DESCRIPCION_QUEJA"].ToString(),
                                  int.Parse(_reader["ID_TIPO_QUEJA"].ToString()),
                                  int.Parse(_reader["ID_ESTADO"].ToString())));
               return complains;

            }
            catch(Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }
            
        }
    }
}
