using System;
using System.Linq;
using FakeItEasy;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using PortalBlade.Data.Tests.Models;

namespace PortalBlade.Data.Tests
{
    [TestFixture]
    public class DatabaseRepositoryTests
    {
        #region [ Setup / Teardown Code ]

        private readonly FluentConfiguration configuration =
            Fluently.Configure( )
                .Database( SQLiteConfiguration.Standard.UsingFile( "test.db" ) )
                .Mappings( m => m.FluentMappings.Add<PersonMap>( ) )
                .ExposeConfiguration( BuildSchema );

        private ISessionFactory sessionFactory;
        private ISession session;
        private ITransaction transaction;

        [TestFixtureSetUp]
        public void SetupFixture( )
        {
            this.sessionFactory = configuration.BuildSessionFactory( );
        }

        private static void BuildSchema( Configuration configuration )
        {
            new SchemaExport( configuration ).Create( false, true );
        }

        [TestFixtureTearDown]
        public void TearDownFixture( )
        {
            this.sessionFactory.Dispose( );
        }

        [SetUp]
        public void Setup( )
        {
            this.session = this.sessionFactory.OpenSession( );
            this.transaction = this.session.BeginTransaction( );
        }

        [TearDown]
        public void TearDown( )
        {
            this.transaction.Dispose( );
            this.session.Dispose( );
        }

        #endregion [ Setup / Teardown Code ]

        #region [ Constructor Tests ]

        [Test]
        public void Ctor_WithNullSession_ThrowsArgumentNullException( )
        {
            Assert.Throws<ArgumentNullException>( ( ) => new DatabaseRepository<Person>( null ) );
        }

        #endregion [ Constructor Tests ]

        [Test]
        public void Insert_InsertsModel( )
        {
            var repository = new DatabaseRepository<Person>( session );
            var person = new Person { Name = "Test User" };
            repository.Insert( person );

            Assert.That( person.Id != 0 );
        }

        [Test]
        public void Update_UpdatesGivenModel( )
        {
            var repository = new DatabaseRepository<Person>( session );

            repository.Insert( new Person { Name = "Test User" } );

            var person = repository.First( );
            person.Name = "New Name";
            repository.Update( person );

            session.Refresh( person );
            Assert.AreEqual( person.Name, repository.First( p => p.Id == person.Id ).Name );
        }

        [Test]
        public void Delete_DeletesGivenModel( )
        {
            var repository = new DatabaseRepository<Person>( session );

            var person = new Person { Name = "Test User" };
            repository.Insert( person );

            repository.Delete( person );

            Assert.IsNull( repository.FirstOrDefault( p => p.Id == person.Id ) );
        }

        [Test]
        public void Evict_EvictsModel( )
        {
            var repository = new DatabaseRepository<Person>( session );

            var person = new Person {Name = "Test User"};
            repository.Insert( person );

            repository.Evict( person );

            person.Name = "New Name";
            repository.Update( person );

            session.Refresh( person );
            Assert.AreNotEqual( "New Name", repository.First( ).Name );
        }

        [Test]
        public void Refresh_RefreshesEntity( )
        {
            var fakeSession = A.Fake<ISession>( );

            var repository = new DatabaseRepository<Person>( fakeSession );

            var person = new Person( );

            repository.Refresh( person );

            A.CallTo( ( ) => fakeSession.Refresh( person ) ).MustHaveHappened( );
        }
    }
}