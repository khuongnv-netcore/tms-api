using System;
using System.Collections.Generic;
using CORE_API.CORE.Models.Entities.Abstract;

namespace CORE_API.CORE.Models.Interop
{
    public class SaveManyResponse<TEntity> where TEntity : CoreEntity
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }
        public List<TEntity> Entities { get; private set; }
        public Exception Inner { get; private set; }

        private SaveManyResponse(bool success, string message, List<TEntity> entities, Exception ex)
        {
            Entities = entities;
            Inner = ex;

        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="entity">Saved entity</param>
        /// <returns>Response.</returns>
        public SaveManyResponse(List<TEntity> entities) : this(true, string.Empty, entities, null)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public SaveManyResponse(string message, Exception ex) : this(false, message, null, ex)
        { }
    }
}