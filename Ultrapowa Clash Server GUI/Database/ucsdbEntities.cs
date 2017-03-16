#region Usings

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

#endregion

namespace UCS.Database
{
    internal class ucsdbEntities : DbContext
    {
        public ucsdbEntities(string connectionString)
            : base("name=" + connectionString) {}

        public virtual DbSet<clan> Clans { get; set; }

        public virtual DbSet<player> Players { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    }
}