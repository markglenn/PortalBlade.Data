﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace PortalBlade.Data
{
    public class MockDataModule : DataModule
    {
        private const string ConnectionString = 
            "Data Source=:memory:;Version=3;New=True;Pooling=True;Max Pool Size=1";

        public MockDataModule( Action<MappingConfiguration> mappings )
            : base( BuildConfiguration( mappings ) )
        {
            
        }

        private static FluentConfiguration BuildConfiguration( Action<MappingConfiguration> mappings )
        {
            return Fluently.Configure( )
                .Database( SQLiteConfiguration.Standard.ConnectionString( ConnectionString ) )
                .Mappings( mappings )
                .ExposeConfiguration( BuildSchema );
        }

        private static void BuildSchema( Configuration configuration )
        {
            new SchemaExport( configuration ).Create( false, true );
        }
    }
}
