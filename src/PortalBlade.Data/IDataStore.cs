using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortalBlade.Data
{
    /// <summary>
    /// Access to the data storage unit and factory for access classes
    /// </summary>
    public interface IDataStore : IDisposable
    {
        /// <summary>
        /// Creates a repository object that points to a data store
        /// </summary>
        /// <typeparam name="T">Type of the repository</typeparam>
        /// <returns>Repository that points to the data store</returns>
        IRepository<T> Repository<T>( ) where T : class;

        /// <summary>
        /// Creates a database session using the current provider
        /// </summary>
        /// <returns>Database session</returns>
        IDataSession CreateSession( );

        /// <summary>
        /// Creates a transaction object that is automatically rolled back if not committed
        /// </summary>
        /// <returns>Transaction object</returns>
        ITransaction CreateTransaction( );
    }
}