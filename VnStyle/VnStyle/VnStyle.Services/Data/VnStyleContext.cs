using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Diagnostics;
using VnStyle.Services.Data.Domain;

namespace VnStyle.Services.Data
{
    public class VnStyleContext : DbContext
    {
        public VnStyleContext() : this("Name=VnStyleContext")
        {

        }
        public VnStyleContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            var type = typeof(SqlProviderServices);

            // Log Entity Framework Query to console of Visual Studio
            Database.Log = s => Debug.WriteLine(s, "EF Information");

            //RetryDbConfiguration.SuspendExecutionStrategy = true;

            // ROLA - This is a hack to ensure that Entity Framework SQL Provider is copied across to the output folder.
            // As it is installed in the GAC, Copy Local does not work. It is required for probing.
            // Fixed "Provider not loaded" error
            //var ensureDLLIsCopied = SqlProviderServices.Instance;

            Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Post> Posts { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasKey(p => p.Id);
        }
    }
}
