using CSharp_Tutorial_Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Tutorial_Repositories.DbContext
{
    public partial class BookManagementDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BookManagementDbContext(DbContextOptions<BookManagementDbContext> options) : base(options)
        {
        }

        // DbSet properties for your entities go here, e.g.:
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookManagementDbContext).Assembly);
        }
    }
}
