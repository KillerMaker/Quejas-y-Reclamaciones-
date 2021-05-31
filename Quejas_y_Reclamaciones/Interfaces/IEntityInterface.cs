using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Quejas_y_Reclamaciones.Interfaces
{
 interface IEntityInterface<T>
    {
        /// <summary>
        /// Hara un SELECT * de la vista de el objeto en la base de datos
        /// </summary>
        /// <param name="searchString">Sirve para ingresar los parametros de busqueda
        /// tales como WHERE y HAVING</param>
        /// <returns>La lista de objetos provenientes de la Base de datos</returns>
        //public List<T> Select(string searchString = null) { throw new NotImplementedException(); }

        /// <summary>
        /// Insertara a Object a su correspondiente tabla en la base de datos
        /// </summary>
        /// <returns>El mensaje proveniente de la base de datos respecto a la accion ejecutada por el metodo</returns>
        public string Insert();

        /// <summary>
        /// Actualiza el elemento en la base de datos que tenga un id que coincida con el de Object.id.
        /// </summary>
        /// <returns>El mensaje proveniente de la base de datos respecto a la accion ejecutada por el metodo</returns>
        public string Update();
        /// <summary>
        /// Eliminara de la base de datos el elemento que coincida con el id en la respectiva tabla en la base de datos.
        /// </summary>
        /// <returns>El mensaje proveniente de la base de datos respecto a la accion ejecutada por el metodo</returns>
        public string Delete();
        
    }
}
