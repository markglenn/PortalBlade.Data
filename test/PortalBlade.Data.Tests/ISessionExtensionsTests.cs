using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FakeItEasy;
using NHibernate;
using NUnit.Framework;
using PortalBlade.Data.Tests.Models;

namespace PortalBlade.Data.Tests
{
    [TestFixture]
    public class ISessionExtensionsTests
    {
        [Test]
        public void Repository_CreatesRepository( )
        {
            var session = A.Fake<ISession>( );

            Assert.IsNotNull( session.Repository<Person>( ) );
        }
    }
}
