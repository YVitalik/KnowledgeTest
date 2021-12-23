using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DomainDbContext : DbContext
    {
        public DomainDbContext(DbContextOptions<DomainDbContext> options) : base(options)
        {

        }

        public DbSet<Test> Tests { get; set; }
        public DbSet<TestDetail> TestDetails { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }
        public DbSet<UserTest> UserTests { get; set; }
    }
}
