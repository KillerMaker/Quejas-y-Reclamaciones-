using System;
using System.Collections.Generic;
using System.Threading;
using System.Data.SqlClient;
using Quejas_y_Reclamaciones.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;

namespace Quejas_y_Reclamaciones.Models
{
    public class CProduct:IEntityInterface<CProduct>
    {
        
        private static SqlConnection _connection;
        private static SqlCommand _command;
        private static SqlDataReader _reader;

        public int? id { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [Range(1.0, 99999999.99)]
        public decimal price { get; set; }

        public int state { get; set; }
        public int productType { get; set; }

        public CProduct(int? id, string name, decimal price, int state, int productType)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.state = state;
            this.productType = productType;

            _connection = new SqlConnection("Data Source=DESKTOP-7V51383\\SQLEXPRESS;Initial Catalog=Quejas&Reclamaciones;Integrated Security=True");
        }

        public string Delete()
        {
            string message="";

            if (_connection.State.Equals(ConnectionState.Closed))
                _connection.Open();

            _command = new SqlCommand($@"DELETE FROM PRODUCTO WHERE ID_PRODUCTO={id}; 
                                             EXEC ERROR_MESSAGES;", _connection);
            _command.ExecuteNonQuery();
            _reader = _command.ExecuteReader();

            while (_reader.Read())
                message = _reader["Text"].ToString();

            return message;
        }

        public string Insert()
        {
            string message = "";

            if (_connection.State.Equals(ConnectionState.Closed))
                _connection.Open();

            _command = new SqlCommand($@"EXEC ={id}; 
                                             EXEC ERROR_MESSAGES;", _connection);
            _command.ExecuteNonQuery();
            _reader = _command.ExecuteReader();

            while (_reader.Read())
                message = _reader["Text"].ToString();

            return message;
        }

        /// <summary>
        /// Hara un SELECT * de la vista de el objeto en la base de datos
        /// </summary>
        /// <param name="searchString">Sirve para ingresar los parametros de busqueda
        /// tales como WHERE y HAVING</param>
        /// <returns>La lista de objetos provenientes de la Base de datos</returns>
        public static List<CProduct> Select(string searchString = null)
        {
            _connection = new SqlConnection("Data Source=DESKTOP-7V51383\\SQLEXPRESS;Initial Catalog=Quejas&Reclamaciones;Integrated Security=True");
            
            int idProduct=0;
            int? nullableIdProduct=null;
            List<CProduct> products = new List<CProduct>();

            if (_connection.State.Equals(ConnectionState.Closed))
                _connection.Open();

            if(searchString== null)
                _command = new SqlCommand($"SELECT * FROM PRODUCTO", _connection);
            else
                _command = new SqlCommand($"SELECT * FROM PRODUCTO+{searchString}", _connection);
           
            _command.ExecuteNonQuery();
            _reader = _command.ExecuteReader();

            while (_reader.Read())
            {  
                if (int.TryParse(_reader["ID_PRODUCTO"].ToString(), out idProduct))
                {
                    idProduct = int.Parse(_reader["ID_PRODUCTO"].ToString());
                    nullableIdProduct = idProduct;
                }
                
                products.Add(new CProduct(nullableIdProduct, _reader["NOMBRE_PRODUCTO"].ToString(), decimal.Parse(_reader["PRECIO_PRODUCTO"].ToString()), int.Parse(_reader["ID_ESTADO"].ToString()), int.Parse(_reader["ID_TIPO_PRODUCTO"].ToString())));
            }
            return products;
        }
        public string Update()
        {
            throw new NotImplementedException();
        }

    }
}
