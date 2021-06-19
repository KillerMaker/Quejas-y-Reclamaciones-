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
            var task = new Task<int>(()=> 
            {
                try
                {
                    int rowCount = 0;

                    setConnection();
                    if (_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();

                    _command = new SqlCommand($@"DELETE FROM PRODUCTO WHERE ID_PRODUCTO={id}; 
                                             SELECT @@ROWCOUNT AS [COLUMN]", _connection);

                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                        rowCount =int.Parse(_reader["COLUMN"].ToString());

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

        public override async Task<int> Insert()
        {
            var task = new Task<int>(()=> 
            {
                try
                {
                    int rowCount = 0;

                    if (_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();

                    _command = new SqlCommand($@"EXEC INSERTA_PRODUCTO
                                              '{name.SQLInyectionClearString()}',
                                              '{price}',
                                              '{state}',
                                              '{productType}';
                                         SELECT @@ROWCOUNT AS [COLUMN];", _connection);
                    //_command.ExecuteNonQuery();
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


        /// <summary>
        /// Hara un SELECT * de la vista de el objeto en la base de datos
        /// </summary>
        /// <param name="searchString">Sirve para ingresar los parametros de busqueda
        /// tales como WHERE y HAVING</param>
        /// <returns>La lista de objetos provenientes de la Base de datos</returns>
        public async static Task<List<CProduct>> Select(string searchString = null)
        {
            var task = new Task<List<CProduct>>(()=> 
            {
                try
                {
                    setConnection();
                    
                    int idProduct = 0;
                    int? nullableIdProduct = null;

                    List<CProduct> products = new List<CProduct>();

                    if (_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();

                    if (searchString == null)
                        _command = new SqlCommand($"SELECT * FROM PRODUCTO", _connection);
                    else
                        _command = new SqlCommand($"SELECT * FROM PRODUCTO+ {searchString}", _connection);

                    //_command.ExecuteNonQuery();
                    _reader = _command.ExecuteReader();

                    while (_reader.Read())
                    {
                        if (int.TryParse(_reader["ID_PRODUCTO"].ToString(), out idProduct))
                        {
                            idProduct = int.Parse(_reader["ID_PRODUCTO"].ToString());
                            nullableIdProduct = idProduct;
                        }

                        products.Add(new CProduct(nullableIdProduct,
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
            });
            
            task.Start();
            return await task;

        }
        public override async Task<int> Update()
        {
            var task = new Task<int>(()=>
            {
                try
                {
                    int rowCount = 0;

                    if (_connection.State.Equals(ConnectionState.Closed))
                        _connection.Open();

                    _command = new SqlCommand($@"UPDATE PRODUCTO SET 
                                                NOMBRE_PRODUCTO = '{name.SQLInyectionClearString()}',
                                                PRECIO_PRODUCTO = {price},
                                                ID_ESTADO = {state},
                                                ID_TIPO_PRODUCTO = {productType}
                                                WHERE ID_PRODUCTO = {id};

                                       SELECT @@ROWCOUNT AS [COLUMN];", _connection);
                    //_command.ExecuteNonQuery();
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

    }
}
