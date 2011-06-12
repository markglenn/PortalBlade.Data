using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Ninject;
using NUnit.Framework;

namespace PortalBlade.Data.Tests
{
    [TestFixture]
    public class DataModuleTests
    {
        private readonly FluentConfiguration configuration =
            Fluently.Configure( ).Database( SQLiteConfiguration.Standard.InMemory( ) );

        private DataModule dataModule;
        private IKernel kernel;

        [SetUp]
        public void Setup( )
        {
            this.dataModule = new DataModule( configuration );
            this.kernel = new StandardKernel( dataModule );
        }

        [TearDown]
        public void TearDown( )
        {
            this.kernel.Dispose( );
        }

        [Test]
        public void Ctor_SetsConfiguration( )
        {
            var module = new DataModule( configuration );

            Assert.IsNotNull( module.SessionFactory );
        }

        [Test]
        public void InitializesSessionFactory( )
        {
            Assert.AreEqual( dataModule.SessionFactory, kernel.Get<ISessionFactory>( ) );
        }

        [Test]
        public void InitializesSession( )
        {
            using( var session = kernel.Get<ISession>( ) )
                Assert.IsNotNull( session );
        }

        [Test]
        public void InitializesRepositoryCreation( )
        {
            Assert.IsNotNull( kernel.Get<IRepository<Object>>( ) );
        }
    }
}
