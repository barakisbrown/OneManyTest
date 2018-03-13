using System.Data.Common;
using System.Data.Entity;
using MySql.Data.Entity;

namespace OneMany.Model
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("name = MyConnectionString")
        {
            
        }

        public MyDbContext(DbConnection existingConnection, bool contextOwnConnection) : base(existingConnection,
            contextOwnConnection)
        {
            // Database.SetInitializer<MyDbContext>(new DropCreateDatabaseAlways<MyDbContext>());
            Database.SetInitializer<MyDbContext>(new CreateDatabaseIfNotExists<MyDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // MAP ENTITY TO TABLE
            modelBuilder.Entity<Publisher>().ToTable("Pubishers");
            modelBuilder.Entity<ComicInfo>().ToTable("Comics");
            modelBuilder.Entity<Writer>().ToTable("Writers");

            // PRIMARY KEY
            modelBuilder.Entity<Publisher>().HasKey(p => p.PublisherId);
            modelBuilder.Entity<ComicInfo>().HasKey(c => c.ComicInfoId);
            modelBuilder.Entity<Writer>().HasKey(w => w.WriterId);

            // Configure one to many
            modelBuilder.Entity<ComicInfo>().HasRequired(p => p.Publisher)
                .WithMany(c => c.Comics)
                .HasForeignKey(p => p.PublisherId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ComicInfo>().HasRequired(w => w.Writer)
                .WithMany(c => c.Comics)
                .HasForeignKey(w => w.WriterId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Writer>().HasRequired(p => p.Publisher)
                .WithMany(w => w.Writers)
                .HasForeignKey(p => p.PublisherId)
                .WillCascadeOnDelete(true);
        }

        public DbSet<ComicInfo> Comics { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Writer> Writer { get; set; }
    }
}