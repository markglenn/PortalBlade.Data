using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortalBlade.Data.Database
{
    public class DatabaseStore : IDataStore
    {
        #region Implementation of IDisposable

        public void Dispose( )
        {
            throw new NotImplementedException( );
        }

        #endregion

        #region Implementation of IDataStore

        public IRepository<T> Repository<T>( ) where T : class
        {
            throw new NotImplementedException( );
        }

        public IDataSession CreateSession( )
        {
            throw new NotImplementedException( );
        }

        public ITransaction CreateTransaction( )
        {
            throw new NotImplementedException( );
        }

        #endregion
    }
}
