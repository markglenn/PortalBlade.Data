using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace PortalBlade.Data.Tests.Models
{
    public class Person
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }

    public sealed class PersonMap : ClassMap<Person>
    {
        public PersonMap( )
        {
            Id( x => x.Id );
            Map( x => x.Name );
        }
    }
}
