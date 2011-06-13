using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        }
    }
}
