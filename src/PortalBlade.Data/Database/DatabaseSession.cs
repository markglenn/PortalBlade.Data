using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace PortalBlade.Data.Database
{
    public class DatabaseSession : IDataSession
    {
        #region [ Private Members ]

        private readonly SessionType sessionType;
        private readonly ISession session;

        #endregion [ Private Members ]

        public DatabaseSession( ISession session )
        {
            this.session = session;
            this.sessionType = SessionType.AutoFlush;
        }

        public DatabaseSession( ISession session, SessionType sessionType )
        {
            this.session = session;
            this.sessionType = sessionType;

            switch( sessionType )
            {
                case SessionType.AutoFlush:
                    this.session.FlushMode = FlushMode.Auto;
                    break;

                case SessionType.ReadOnly:
                    this.session.FlushMode = FlushMode.Never;
                    break;
            }
        }

        public ISession Session
        {
            get { return this.session; }
        }

        #region [ Implementation of IDataSession ]

        public SessionType SessionType
        {
            get { return this.sessionType; }
        }

        public void Flush( )
        {
            this.session.Flush( );
        }

        #endregion [ Implementation of IDataSession ]

        #region [ Implementation of IDisposable ]

        ~DatabaseSession( )
        {
            Dispose( false );
        }

        public void Dispose( )
        {
            this.Dispose( true );
            GC.SuppressFinalize( this );

        }

        protected virtual void Dispose( bool disposing )
        {
            if ( disposing )
                this.session.Dispose( );
        }

        #endregion [ Implementation of IDisposable ]

    }
}
