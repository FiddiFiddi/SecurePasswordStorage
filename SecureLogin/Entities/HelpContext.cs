using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    /// <summary>
    /// Using entity Framework - Simple Context
    /// </summary>
    public class HelpContext : DbContext
    {
        public HelpContext(DbContextOptions<HelpContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
