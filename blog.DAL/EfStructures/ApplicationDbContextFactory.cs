using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace blog.DAL
{
    class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
          var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
          var connectionString = @"Data Source=3.133.83.210,1433;Database=BlogAPI;User Id=hovhannes.sisoyan;Password=123456;Integrated Security=false;";
          optionsBuilder.UseSqlServer(connectionString);
          return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}