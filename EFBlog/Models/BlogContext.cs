using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFBlog.Models
{
    public class BlogContext : DbContext
    {
        public DbSet<Blog> Blogs {get;set;}
        public DbSet<Post> Posts {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder
                //.UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("BloggingContext")!);
        }
    }
}