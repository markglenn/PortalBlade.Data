using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NUnit.Framework;
using PortalBlade.Data.Database;
using FakeItEasy;

namespace PortalBlade.Data.Tests.Database
{
    public class DatabaseSessionTests
    {
        private ISession session;

        [SetUp]
        public void Setup( )
        {
            this.session = A.Fake<ISession>( );
        }

        [Test]
        public void SessionType_DefaultsToAutoFlush( )
        {
            var dbSession = new DatabaseSession( this.session );
            Assert.AreEqual( SessionType.AutoFlush, dbSession.SessionType );
        }

        [Test]
        public void Ctor_SetsSession( )
        {
            var dbSession = new DatabaseSession( session );

            Assert.AreEqual( this.session, dbSession.Session );
        }

        [Test]
        public void Ctor_SetsSessionAndFlushMode( )
        {
            var dbSession = new DatabaseSession( session, SessionType.ReadOnly );

            Assert.AreEqual( this.session, dbSession.Session );
            Assert.AreEqual( SessionType.ReadOnly, dbSession.SessionType );
        }

        [Test]
        public void Ctor_WithAuto_SetsNHibernateFlushMode( )
        {
            new DatabaseSession( session, SessionType.AutoFlush );

            Assert.AreEqual( FlushMode.Auto, this.session.FlushMode );
        }

        [Test]
        public void Ctor_WithReadOnly_SetsNHibernateFlushMode( )
        {
            new DatabaseSession( session, SessionType.ReadOnly );

            Assert.AreEqual( FlushMode.Never, this.session.FlushMode );
        }
        [Test]
        public void Dispose_DisposesSession( )
        {
            var dbSession = new DatabaseSession( session );

            dbSession.Dispose( );
            A.CallTo( ( ) => this.session.Dispose( ) ).MustHaveHappened( );
        }

        [Test]
        public void Flush_FlushesSession( )
        {
            var dbSession = new DatabaseSession( session );

            dbSession.Flush( );
            A.CallTo( ( ) => this.session.Flush( ) ).MustHaveHappened( );
        }
    }
}
