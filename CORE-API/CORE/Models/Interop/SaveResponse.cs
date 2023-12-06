using System;
using CORE_API.CORE.Models.Entities.Abstract;

namespace CORE_API.CORE.Models.Interop
{
    public class SaveResponse<TEntity> where TEntity : CoreEntity
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }
        public TEntity Entity { get; private set; }
        public Exception Inner { get; private set; }

        private SaveResponse(bool success, string message, TEntity entity, Exception ex)
        {
            Entity = entity;
            Inner = ex;

        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="entity">Saved entity</param>
        /// <returns>Response.</returns>
        public SaveResponse(TEntity entity) : this(true, string.Empty, entity, null)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public SaveResponse(string message, Exception ex) : this(false, message, null, ex)
        { }
    }
}