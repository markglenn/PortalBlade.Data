using System;
using NHibernate;

namespace PortalBlade.Data
{
    public static class ISessionExtensions
    {
        /// <summary>
        /// Creates a repository attached to the session
        /// </summary>
        /// <typeparam name="T">Type of the model</typeparam>
        /// <param name="session">Session that this repository is attached</param>
        /// <returns>Data repository</returns>
        public static IRepository<T> Repository<T>( this ISession session ) where T : class
        {
            return new DatabaseRepository<T>( session );
        }
    }
}
