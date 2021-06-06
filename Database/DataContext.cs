using Entities;
using MySql.Data.EntityFramework;
using System.Data.Entity;

namespace Database
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DataContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Program> Programs { get; set; }

        public DbSet<Speciality> Specialities { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<TrainingForm> TrainingForms { get; set; }

        public DbSet<University> Universities { get; set; }

        public DataContext() :base("EnrolleeGuideDb")
        {

        }
    }
}
