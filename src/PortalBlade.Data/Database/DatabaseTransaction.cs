using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortalBlade.Data.Database
{
    public class DatabaseTransaction : ITransaction
    {
        private readonly NHibernate.ITransaction transaction;
        private bool committed;

        public DatabaseTransaction( NHibernate.ITransaction transaction )
        {
            this.transaction = transaction;
        }

        public void Rollback( )
        {
            this.transaction.Rollback( );
            this.committed = true;
        }

        public void Commit( )
        {
            this.transaction.Commit( );
            this.committed = true;
        }

        ~DatabaseTransaction( )
        {
            Dispose( false );
        }

        public void Dispose( )
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            if( !disposing ) 
                return;

            if ( !this.committed )
                this.transaction.Rollback( );
    
            this.transaction.Dispose( );
        }
    }
}
