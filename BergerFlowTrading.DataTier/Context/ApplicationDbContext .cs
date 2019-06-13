using BergerFlowTrading.Model;
using BergerFlowTrading.Model.Identity;
using BergerFlowTrading.Model.Logs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BergerFlowTrading.DataTier.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options)
              : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.Relational().ColumnType = "decimal(18, 10)";
            }
        }

        public virtual DbSet<Exchange> Exchanges { get; set; }
        public virtual DbSet<ExchangeCustomSettings> ExchangeCustomSettings { get; set; }
        public virtual DbSet<UserExchangeSecret> UserExchangeSecrets { get; set; }
        public virtual DbSet<LimitArbitrageStrategy4Settings> LimitArbitrageStrategy4Settings { get; set; }


        //Logs
        public virtual DbSet<PlatformJob> PlatformJob { get; set; }
        public virtual DbSet<PlatformLogs> PlatformLogs { get; set; }
        public virtual DbSet<StrategyRuns> StrategyRuns { get; set; }
        public virtual DbSet<StrategyLog> StrategyLog { get; set; }
        public virtual DbSet<ExchangeLogs> ExchangeLogs { get; set; }
    }
}
