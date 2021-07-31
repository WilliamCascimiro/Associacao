using Microsoft.EntityFrameworkCore;
using Associacao.Domain.Entities;
using Associacao.Repository.Maps;

namespace Associacao.Repository.Common
{ 
    public class ApplicationDbContext : DbContext
    {
        //public ApplicationDbContext()
        //{
        //    ChangeTracker.AutoDetectChangesEnabled = false;
        //}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Mensalidade> Mensalidades { get; set; }
        public DbSet<Configuracao> Configuracoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            //modelBuilder.ApplyConfiguration(new PessoaMap());
        }
    }
}
