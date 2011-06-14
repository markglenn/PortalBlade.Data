using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Ninject;
using NUnit.Framework;
using PortalBlade.Data.Tests.Models;

namespace PortalBlade.Data.Tests
{
    [TestFixture]
    public class MockDataModuleTests
    {
        [Test]
        public void InitializesInMemorySessions( )
        {
            var dataModule = new MockDataModule( m => m.FluentMappings.Add<PersonMap>( ) );

            using ( var kernel = new StandardKernel( dataModule ) )
            {
                using( var session = kernel.Get<ISession>( ) )
                {
                    Assert.That( session.Connection.ConnectionString.Contains( ":memory:" ) );
                }
            }
        }

        [Test]
        public void Session_StaysAliveMultipleConnections( )
        {
            var dataModule = new MockDataModule( m => m.FluentMappings.Add<PersonMap>( ) );

            using ( var kernel = new StandardKernel( dataModule ) )
            {
                using( var session = kernel.Get<ISession>( ) )
                {
                    var repository = session.Repository<Person>( );
                    repository.Insert( new Person {Name = "Test User"} );
                }

                using( var session = kernel.Get<ISession>( ) )
                {
                    var repository = session.Repository<Person>( );
                    Assert.AreEqual( "Test User", repository.First( ).Name );
                }
            }
        }

        [Test]
        public void Repository_CreatesLiveConnection( )
        {
            var dataModule = new MockDataModule( m => m.FluentMappings.Add<PersonMap>( ) );

            using ( var kernel = new StandardKernel( dataModule ) )
            {
                var repository = kernel.Get<IRepository<Person>>( );

                repository.Insert( new Person {Name = "Testing User"} );

                Assert.AreEqual( 1, repository.Count( ) );
                Assert.AreEqual( "Testing User", repository.First( ).Name );

            }
        }
    }
}
