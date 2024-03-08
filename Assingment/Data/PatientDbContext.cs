using Microsoft.EntityFrameworkCore;
using Assingment.Models;

namespace Assingment.Data
{
        public class PatientDbContext : DbContext
        {
            public PatientDbContext(DbContextOptions<PatientDbContext> options)
                : base(options)
            {
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlServer("server=DESKTOP-4M61261\\MSSQL; database=yusra; user id=sa; password=yusra@@; TrustServerCertificate=true;");
                }
            }

            public DbSet<Patient> Patients { get; set; }
            public object Users { get; internal set; }
        }
    }