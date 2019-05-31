using Census.API.Infrastructure.EntityConfigurations;
using Census.API.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Census.API.Infrastructure
{
    public class CensusDbContext : DbContext
    {
        public CensusDbContext(DbContextOptions<CensusDbContext> options) : base(options)
        {
        }

        public DbSet<ActualCensusEntity> ActualCensusEntities { get; set; }

        public DbSet<EstimatedCensusEnity> EstimatedCensusEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ActualCensusEntityTypeConfiguration());
            builder.ApplyConfiguration(new EstimatedCensusEntityTypeConfiguration());
        }
    }
}
