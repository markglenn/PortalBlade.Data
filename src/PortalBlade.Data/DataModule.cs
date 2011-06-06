using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Ninject.Modules;

namespace PortalBlade.Data
{
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

        public DataModule( FluentConfiguration configuration )
        {
            this.sessionFactory = configuration
                .BuildSessionFactory( );
        }

        #region [ Overrides of NinjectModule ]

        public override void Load( )
        {
            this.Kernel.Bind<ISessionFactory>( ).ToConstant( this.sessionFactory );
        }

        #endregion [ Overrides of NinjectModule ]
    }
}