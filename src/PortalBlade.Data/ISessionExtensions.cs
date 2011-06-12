using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace PortalBlade.Data
{
    public static class ISessionExtensions
    {
        public static IRepository<T> Repository<T>( this ISession session ) where T : class
        {
            return new DatabaseRepository<T>( session );
        }
    }
}
