using Census.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Census.API.Infrastructure.EntityConfigurations
{
    class ActualCensusEntityTypeConfiguration
        : IEntityTypeConfiguration<ActualCensusEntity>
    {
        public void Configure(EntityTypeBuilder<ActualCensusEntity> builder)
        {
            builder.ToTable("Actuals")
                .HasKey(c => c.StateId);

            builder.Property(c => c.StateId)
                .HasColumnName("State")
                .IsRequired();

            builder.Property(c => c.Population)
                .HasColumnName("ActualPopulation")
                .IsRequired();

            builder.Property(c => c.Households)
                .HasColumnName("ActualHouseholds")
                .IsRequired();
        }
    }
}
