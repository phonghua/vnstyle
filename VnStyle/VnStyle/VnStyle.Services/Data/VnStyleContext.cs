using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Diagnostics;
using VnStyle.Services.Data.Domain;

namespace VnStyle.Services.Data
{
    public class VnStyleContext : DbContext, IDbContext
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

        #region "Public functions"
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public new DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Entry(entity);
        }

        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : class, new()
        {
            throw new NotImplementedException();

            //if (parameters != null && parameters.Length > 0)
            //{
            //    for (var i = 0; i <= parameters.Length - 1; i++)
            //    {
            //        var p = parameters[i] as DbParameter;
            //        if (p == null)
            //        {
            //            throw new Exception("Not support parameter type");
            //        }

            //        commandText += i == 0 ? " " : ", ";

            //        commandText += "@" + p.ParameterName;
            //        if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
            //        {
            //            //output parameter
            //            commandText += " output";
            //        }
            //    }
            //}
            //var result = Database.SqlQuery<TEntity>(commandText, parameters).ToList();
            //return result;
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return Database.SqlQuery<TElement>(sql, parameters);
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            throw new NotImplementedException();

            //int? previousTimeout = null;
            //if (timeout.HasValue)
            //{
            //    //store previous timeout
            //    previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
            //    ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            //}

            //var transactionalBehavior = doNotEnsureTransaction
            //    ? TransactionalBehavior.DoNotEnsureTransaction
            //    : TransactionalBehavior.EnsureTransaction;
            //var result = Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);

            //if (timeout.HasValue)
            //{
            //    //Set previous timeout back
            //    ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
            //}

            ////return result
            //return result;
        }
        #endregion
    }
}
