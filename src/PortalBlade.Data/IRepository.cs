﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortalBlade.Data
{
    public interface IRepository : IQueryable
    {
        /// <summary>
        /// Inserts a new entity into the repository
        /// </summary>
        /// <param name="entity">Entity to insert into the repository</param>
        void Insert( object entity );

        /// <summary>
        /// Updates an entity in the repository
        /// </summary>
        /// <param name="entity">Entity to update in the repository</param>
        void Update( object entity );

        /// <summary>
        /// Deletes an entity from the repository
        /// </summary>
        /// <param name="entity">Entity to delete from the repository</param>
        void Delete( object entity );

        /// <summary>
        /// Evicts any changes made to the entity from the session
        /// </summary>
        /// <param name="entity">Entity to evict</param>
        void Evict( object entity );
    }

    public interface IRepository<T> : IRepository, IQueryable<T> where T : class
    {
        /// <summary>
        /// Inserts a new entity into the repository
        /// </summary>
        /// <param name="entity">Entity to insert into the repository</param>
        void Insert( T entity );

        /// <summary>
        /// Updates an entity in the repository
        /// </summary>
        /// <param name="entity">Entity to update in the repository</param>
        void Update( T entity );

        /// <summary>
        /// Deletes an entity from the repository
        /// </summary>
        /// <param name="entity">Entity to delete from the repository</param>
        void Delete( T entity );

        /// <summary>
        /// Evicts any changes made to the entity from the session
        /// </summary>
        /// <param name="entity">Entity to evict</param>
        void Evict( T entity );
    }
}