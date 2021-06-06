using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quejas_y_Reclamaciones.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Quejas_y_Reclamaciones.Models
{
    public class CEmployee :CPerson,IEntityInterface<CEmployee>
    {
        public int idDepartment { get; set; }
        public CEmployee(int? id, string name, string birthDay, string idCard, string email, string phone, string genre, int idDepartment, CUser user = null)
            : base(id, name, birthDay, idCard, email, phone, genre, user) =>this.idDepartment=idDepartment;


        public override string Update()
        {
            string message = "";
            try 
            { 
                base.Update();

                if (_connection.State.Equals(ConnectionState.Closed))
                    _connection.Open();

                _command = new SqlCommand($@"UPDATE PERSONA_DEPARTAMENTO SET 
                                                ID_DEPARTAMENTO={idDepartment}
                                             WHERE ID_PERSONA ={id}
                                             EXEC ERROR_MESSAGES", _connection);

                _reader = _command.ExecuteReader();

                while (_reader.Read())
                    message = _reader["Text"].ToString();

                return message;
            }
            catch(Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }
        }
        public override object Insert()
        {
            if (_connection.State.Equals(ConnectionState.Closed))
                _connection.Open();

            _command = new SqlCommand($@"EXEC INSERTA_EMPLEADO
                                              '{name}',
                                              '{birthDay}',
                                              '{idCard}',
                                              '{email}',
                                              '{phone}',
                                              '{genre}',
                                              '{user.userName}',
                                              '{user.password}',
                                               {idDepartment};

                                        SELECT * FROM VISTA_EMPLEADO 
                                              WHERE ID_PERSONA=(SELECT MAX (ID_PERSONA) FROM PERSONA);", _connection);

            _reader = _command.ExecuteReader();

            CEmployee employee = null;

            while (_reader.Read())
                employee= new CEmployee(
                                     int.Parse(_reader["ID_PERSONA"].ToString()),
                                     _reader["NOMBRE_PERSONA"].ToString(),
                                     _reader["FECHA_NAC_PERSONA"].ToString(),
                                     _reader["CEDULA_PERSONA"].ToString(),
                                     _reader["CORREO_PERSONA"].ToString(),
                                     _reader["TELEFONO_PERSONA"].ToString(),
                                     _reader["GENERO_PERSONA"].ToString(),
                                     int.Parse(_reader["ID_DEPARTAMENTO"].ToString()),
                                     new CUser(
                                               int.Parse(_reader["ID_USUARIO"].ToString()),
                                               _reader["NOMBRE_USUARIO"].ToString(),
                                               _reader["CLAVE_USUARIO"].ToString(),
                                               int.Parse(_reader["ID_TIPO_USUARIO"].ToString())));

            return employee;
        }
        public override string Delete()
        {
            return "";
        }

        public new static CEmployee Select(string searchString)
        {
            throw new NotFiniteNumberException();
        }
    }
}
