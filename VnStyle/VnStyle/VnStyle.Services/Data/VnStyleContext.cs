//using System;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Diagnostics;
using VnStyle.Services.Data.Domain;
using VnStyle.Services.Data.Domain.Memberships;

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

            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ArticleLanguage> ArticleLanguages { get; set; }
        public DbSet<MetaTag> MetaTags { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<RelatedArticle> RelatedArticles { get; set; }


        public DbSet<AspNetClient> AspNetClients { get; set; }
        public DbSet<AspNetRefreshToken> AspNetRefreshTokens { get; set; }
        public DbSet<AspNetRegister> AspNetRegisters { get; set; }
        public DbSet<AspNetRole> AspNetRoles { get; set; }
        public DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<AspNetUserLogin> AspNetUserLogin { get; set; }
        public DbSet<AspNetUserProfile> AspNetUserProfiles { get; set; }
        public DbSet<AspNetUserRole> AspNetUserRoles { get; set; }





        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region "Article"
            modelBuilder.Entity<Article>().HasKey(p => p.Id);
            modelBuilder.Entity<Article>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            #endregion


            modelBuilder.Entity<ArticleLanguage>().HasKey(p => p.Id);
            modelBuilder.Entity<ArticleLanguage>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ArticleLanguage>().HasRequired(p => p.Article).WithMany(p => p.ArticleLanguages).HasForeignKey(p => p.ArticleId).WillCascadeOnDelete(true);



            modelBuilder.Entity<MetaTag>().HasKey(p => p.Id);
            modelBuilder.Entity<MetaTag>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            modelBuilder.Entity<AspNetClient>().HasKey(p => p.Id);
            modelBuilder.Entity<AspNetClient>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<AspNetRefreshToken>().HasKey(p => p.Id);
            modelBuilder.Entity<AspNetRefreshToken>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<AspNetRegister>().HasKey(p => p.Id);
            modelBuilder.Entity<AspNetRegister>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<AspNetRole>().HasKey(p => p.Id);
            modelBuilder.Entity<AspNetRole>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<AspNetUserClaim>().HasKey(p => p.Id);
            modelBuilder.Entity<AspNetUserClaim>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            modelBuilder.Entity<AspNetUser>().HasKey(p => p.Id);
            modelBuilder.Entity<AspNetUser>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<AspNetUserLogin>().HasKey(p => new { p.LoginProvider, p.ProviderKey });


            modelBuilder.Entity<AspNetUserProfile>().HasKey(p => p.Id);
            modelBuilder.Entity<AspNetUserProfile>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            modelBuilder.Entity<AspNetUserRole>().HasKey(p => new { p.RoleId, p.UserId });

            modelBuilder.Entity<File>().HasKey(p => new { p.Id });

            modelBuilder.Entity<Category>().HasKey(p => new { p.Id });

            modelBuilder.Entity<RelatedArticle>().HasKey(p => new { p.Id });
            modelBuilder.Entity<RelatedArticle>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<RelatedArticle>().HasRequired(p => p.Article1).WithMany(p => p.RelatedArticles1).HasForeignKey(p => p.Article1Id).WillCascadeOnDelete(true);
            modelBuilder.Entity<RelatedArticle>().HasRequired(p => p.Article2).WithMany(p => p.RelatedArticles2).HasForeignKey(p => p.Article2Id).WillCascadeOnDelete(false);

            //modelBuilder.Entity<Student>().HasRequired(m => m.BirthCity)
            //    .WithMany(m => m.Students).HasForeignKey(m => m.BirthCityId);
            //modelBuilder.Entity<Student>().HasRequired(m => m.LivingCity)
            //    .WithMany().HasForeignKey(m => m.LivingCityId);

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
