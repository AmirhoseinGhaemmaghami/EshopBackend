using EshopBackend.Shared.Entities.Access;
using EshopBackend.Shared.Entities.Account;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopBackend.Data.Context
{
    public class EshopContext : DbContext
    {
        public EshopContext(DbContextOptions<EshopContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var foreignKeys = modelBuilder.Model.GetEntityTypes()
                .SelectMany(c => c.GetForeignKeys())
                .Where(f => f.IsOwnership && f.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var item in foreignKeys)
            {
                item.DeleteBehavior = DeleteBehavior.Restrict;
            }
                
        }


        #region DBSets

        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }

        #endregion
    }
}
