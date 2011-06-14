using System;
using FluentNHibernate.Cfg;
using NHibernate;
using Ninject.Modules;

namespace PortalBlade.Data
{
    /// <summary>
    /// Ninject module used to setup Fluent NHibernate
    /// </summary>
    public class DataModule : NinjectModule
    {
        #region [ Private Members ]

        private readonly ISessionFactory sessionFactory;

        #endregion [ Private Members ]

        #region [ Public Properties ]

        public ISessionFactory SessionFactory
        {
            get { return this.sessionFactory; }
        }

        #endregion [ Public Properties ]

        /// <summary>
        /// Initializes the data module
        /// </summary>
        /// <param name="configuration">Fluent NHibernate configuration</param>
        public DataModule( FluentConfiguration configuration )
        {
            this.sessionFactory = configuration
                .BuildSessionFactory( );
        }

        #region [ Overrides of NinjectModule ]

        public override void Load( )
        {
            this.Bind<ISessionFactory>( )
                .ToConstant( this.sessionFactory );
            
            this.Bind<ISession>( )
                .ToMethod( m => this.sessionFactory.OpenSession( ) )
                .InRequestScope( );

            this.Bind( typeof( IRepository<> ) )
                .To( typeof( DatabaseRepository<> ) );
        }

        #endregion [ Overrides of NinjectModule ]
    }
}