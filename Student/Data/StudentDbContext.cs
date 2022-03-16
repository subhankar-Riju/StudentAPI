using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Data
{
    public class StudentDbContext : DbContext
    {
       public  StudentDbContext(DbContextOptions<StudentDbContext> options):base(options)
        {
        }

        public DbSet<Students> students { get; set; }
        public DbSet<SignUp> signup { get; set; }
    }
}
