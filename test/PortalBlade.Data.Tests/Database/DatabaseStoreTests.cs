using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FakeItEasy;
using NHibernate;
using NUnit.Framework;
using PortalBlade.Data.Database;

namespace PortalBlade.Data.Tests.Database
{
    [TestFixture]
    public class DatabaseStoreTests
    {
        private ISessionFactory sessionFactory;

        [SetUp]
        public void Setup( )
        {
            this.sessionFactory = A.Fake<ISessionFactory>( );
        }

        [Test]
        public void CreateSession_CreatesDatabaseSession( )
        {
            A.CallTo( ( ) => this.sessionFactory.OpenSession( ) )
                .Returns( A.Fake<ISession>( ) ).Once( );
            var store = new DatabaseStore( this.sessionFactory );

            var session = store.CreateSession( );
            Assert.IsNotNull( session );
            Assert.IsInstanceOf<DatabaseSession>( session );
        }

    }
}
