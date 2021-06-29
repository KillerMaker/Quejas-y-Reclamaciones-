using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;


namespace Quejas_y_Reclamaciones.Models
{
    public class CProduct:CEntity<int>
    {
        protected static SqlConnection _connection;
        protected static SqlCommand _command;
        protected static SqlDataReader _reader;

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

            setConnection();
        }

        public static async Task<int> Delete(int id)
        {
            try
            {
                setConnection();
                _connection = connection;


                if (_connection.State.Equals(ConnectionState.Open))
                    await _connection.CloseAsync();

                await _connection.OpenAsync();

                _command = new SqlCommand($@"DELETE FROM PRODUCTO WHERE ID_PRODUCTO={id};", _connection);
               
                return (await _command.ExecuteNonQueryAsync() != 0) ? id : 0;


            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }

        }

        public override async Task<int> Insert()
        {
            try
            {

                if (_connection.State.Equals(ConnectionState.Closed))
                    _connection.Open();

                _command = new SqlCommand($@"EXEC INSERTA_PRODUCTO
                                            '{name.SQLInyectionClearString()}',
                                            '{price}',
                                            '{state}',
                                            '{productType}';", _connection);

                return (await _command.ExecuteNonQueryAsync() != 0) ? id.Value : 0;

            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }

            
        }


        /// <summary>
        /// Hara un SELECT * de la vista de el objeto en la base de datos
        /// </summary>
        /// <param name="searchString">Sirve para ingresar los parametros de busqueda
        /// tales como WHERE y HAVING</param>
        /// <returns>La lista de objetos provenientes de la Base de datos</returns>
        public async static Task<List<CProduct>> Select(string searchString = null)
        {
            try
            {
                setConnection();
                _connection = connection;

                if (_connection.State.Equals(ConnectionState.Open))
                    await _connection.CloseAsync();

                await _connection.OpenAsync();

                List<CProduct> products = new List<CProduct>();

                _command = new SqlCommand($"SELECT * FROM PRODUCTO+ {searchString}", _connection);


                _reader = _command.ExecuteReader();

                while (_reader.Read())
                {
                    products.Add(new CProduct(int.Parse(_reader["ID_PRODUCTO"].ToString()),
                                    _reader["NOMBRE_PRODUCTO"].ToString(),
                                    decimal.Parse(_reader["PRECIO_PRODUCTO"].ToString()),
                                    int.Parse(_reader["ID_ESTADO"].ToString()),
                                    int.Parse(_reader["ID_TIPO_PRODUCTO"].ToString())));
                }

                return products;
            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }


        }
        public override async Task<int> Update()
        {
            try
            {
                if (_connection.State.Equals(ConnectionState.Closed))
                    _connection.Open();

                _command = new SqlCommand($@"UPDATE PRODUCTO SET 
                                            NOMBRE_PRODUCTO = '{name.SQLInyectionClearString()}',
                                            PRECIO_PRODUCTO = {price},
                                            ID_ESTADO = {state},
                                            ID_TIPO_PRODUCTO = {productType}
                                            WHERE ID_PRODUCTO = {id};", _connection);

                return (await _command.ExecuteNonQueryAsync() != 0) ? id.Value : 0;

            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }
            
        }

    }
}
