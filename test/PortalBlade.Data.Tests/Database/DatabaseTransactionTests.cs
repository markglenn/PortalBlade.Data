using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FakeItEasy;
using NUnit.Framework;
using PortalBlade.Data.Database;
using NHTransaction = NHibernate.ITransaction;

namespace PortalBlade.Data.Tests.Database
{
    [TestFixture]
    public class DatabaseTransactionTests
    {
        private NHTransaction transaction;

        [SetUp]
        public void Setup( )
        {
            transaction = A.Fake<NHTransaction>( );
        }

        [Test]
        public void Commit_CommitsTransaction( )
        {
            new DatabaseTransaction( this.transaction ).Commit( );
            A.CallTo( ( ) => transaction.Commit( ) ).MustHaveHappened( );
        }

        [Test]
        public void RollBack_RollsbackTransaction( )
        {
            new DatabaseTransaction( this.transaction ).Rollback( );
            A.CallTo( ( ) => transaction.Rollback( ) ).MustHaveHappened( );
        }

        [Test]
        public void Dispose_RollsbackTransaction( )
        {
            new DatabaseTransaction( this.transaction ).Dispose( );
            A.CallTo( ( ) => transaction.Rollback( ) ).MustHaveHappened( );
        }

        [Test]
        public void Dispose_ShouldNotRollbackCommittedTransaction( )
        {
            using( var tx = new DatabaseTransaction( this.transaction ) )
            {
                tx.Commit( );
            }

            A.CallTo( ( ) => transaction.Rollback( ) ).MustNotHaveHappened( );
        }

        [Test]
        public void Dispose_ShouldNotRollbackRolledBackTransaction( )
        {
            using ( var tx = new DatabaseTransaction( this.transaction ) )
            {
                tx.Rollback( );
            }

            A.CallTo( ( ) => transaction.Rollback( ) ).MustHaveHappened( Repeated.Exactly.Once );
        }

        [Test]
        public void Dispose_DisposesTransaction( )
        {
            using ( new DatabaseTransaction( this.transaction ) )
            {
            }

            A.CallTo( ( ) => transaction.Dispose( ) ).MustHaveHappened( );
        }


    }
}
