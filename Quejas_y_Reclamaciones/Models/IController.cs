using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Quejas_y_Reclamaciones.Models
{
    interface IController<T>
    {
        /// <summary>
        /// Metodo encargado de manejar todos los request hechos con el metodo Post a este Controlador
        /// </summary>
        /// <param name="obj">Objeto enviado en el cuerpo del request</param>
        /// <returns>Un estado Http</returns>
        public abstract Task<IActionResult> Post(T obj);

        /// <summary>
        /// Metodo encargado de manejar todos los request hechos con el metodo Put a este Controlador
        /// </summary>
        /// <param name="obj">Objeto enviado en el cuerpo del request</param>
        /// <returns>Un estado Http</returns>
        public abstract Task<IActionResult> Put(T obj);

        /// <summary>
        /// Metodo encargado de manejar todos los request hechos con el metodo Delete a este Controlador
        /// </summary>
        /// <param name="id">El identificador con el que el metodo trabajara</param>
        /// <returns>Un estado Http</returns>
        public abstract Task<IActionResult> Delete(int id);

        /// <summary>
        /// Metodo encargado de manejar parte de los request hechos con el metodo Get a este Controlador
        /// </summary>
        /// <param name="searchString">Cadena con la cual se podran realizar consultas personalizadas</param>
        /// <returns>Un estado Http</returns>
        public abstract Task<IActionResult> Get(string searchString);
        /// <summary>
        /// Metodo encargado de manejar parte de los request hechos con el metodo Get a este Controlador
        /// </summary>
        /// <param name="id">El identificador con el que el metodo trabajara</param>
        /// <returns>Un estado Http</returns>
        public abstract Task<IActionResult> Get(int id);

    }
}
