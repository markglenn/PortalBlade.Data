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

        [Test]
        public void Ctor_SetsConfiguration( )
        {
            var module = new DataModule( configuration );

            Assert.IsNotNull( module.SessionFactory );
        }

        [Test]
        public void InitializesSessionFactory( )
        {
            var module = new DataModule( configuration );

            using( var kernel = new StandardKernel( module ) )
            {
                Assert.AreEqual( module.SessionFactory, kernel.Get<ISessionFactory>( ) );
            }
        }
    }
}
