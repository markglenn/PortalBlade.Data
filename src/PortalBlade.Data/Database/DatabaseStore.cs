using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace PortalBlade.Data.Database
{
    public class DatabaseStore : IDataStore
    {
        private readonly ISessionFactory sessionFactory;

        public DatabaseStore( ISessionFactory sessionFactory )
        {
            this.sessionFactory = sessionFactory;
        }

        #region [ Implementation of IDataStore ]

        public IRepository<T> Repository<T>( ) where T : class
        {
            throw new NotImplementedException( );
        }

        public IDataSession CreateSession( )
        {
            return new DatabaseSession( this.sessionFactory.OpenSession( ));
        }

        public ITransaction CreateTransaction( )
        {
            throw new NotImplementedException( );
        }

        #endregion [ Implementation of IDataStore ]

        #region [ Implementation of IDisposable ]

        public void Dispose( )
        {
            throw new NotImplementedException( );
        }

        #endregion [ Implementation of IDisposable ]

    }
}
