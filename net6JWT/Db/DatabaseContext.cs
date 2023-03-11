
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using net6JWT.DataAccess.EntityframeworkCore.Abstract;
using net6JWT.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace net6JWT.Db
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-HTHP4A7\\SQLEXPRESS;Database=multi;User Id=okaya;Password=password;MultipleActiveResultSets=true;TrustServerCertificate=True");
            }
        }

        public DbSet<User> Users { get; set; }




    }
}
