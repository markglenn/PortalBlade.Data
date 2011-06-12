using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Linq;

namespace PortalBlade.Data
{
    public class DatabaseRepository<T> : IRepository<T> where T : class
    {
        #region [ Private Members ]
        
        private readonly ISession session;
        private readonly IQueryable<T> queryable;

        #endregion [ Private Members ]

        public DatabaseRepository( ISession session )
        {
            if( session == null ) 
                throw new ArgumentNullException( "session" );

            this.session = session;
            this.queryable = this.session.Query<T>( );
        }

        #region [ Implementation of IEnumerable ]

        public IEnumerator<T> GetEnumerator( )
        {
            return this.queryable.GetEnumerator( );
        }

        IEnumerator IEnumerable.GetEnumerator( )
        {
            return GetEnumerator( );
        }

        #endregion [ Implementation of IEnumerable ]

        #region [ Implementation of IQueryable ]

        public Expression Expression
        {
            get { return this.queryable.Expression; }
        }

        public Type ElementType
        {
            get { return this.queryable.ElementType; }
        }

        public IQueryProvider Provider
        {
            get { return this.queryable.Provider; }
        }

        #endregion [ Implementation of IQueryable ]

        #region [ Implementation of IRepository ]

        public void Insert( object entity )
        {
            this.Insert( ( T ) entity );
        }

        public void Update( object entity )
        {
            this.Update( ( T ) entity );
        }

        public void Delete( object entity )
        {
            this.Delete( ( T ) entity );
        }

        public void Evict( object entity )
        {
            this.Evict( ( T ) entity );
        }

        #endregion [ Implementation of IRepository ]

        #region [ Implementation of IRepository<T> ]

        public void Insert( T entity )
        {
            this.session.Save( entity );
        }

        public void Update( T entity )
        {
            this.session.Update( entity );
        }

        public void Delete( T entity )
        {
            this.session.Delete( entity );
        }

        public void Evict( T entity )
        {
            this.session.Evict( entity );
        }

        public void Refresh( T entity )
        {
            this.session.Refresh( entity );
        }

        #endregion [ Implementation of IRepository<T> ]
    }
}
