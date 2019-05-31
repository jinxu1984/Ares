using Census.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Census.API.Infrastructure.EntityConfigurations
{
    public class EstimatedCensusEntityTypeConfiguration 
        : IEntityTypeConfiguration<EstimatedCensusEnity>
    {
        public void Configure(EntityTypeBuilder<EstimatedCensusEnity> builder)
        {
            builder.ToTable("Estimates")
                .HasKey(c => new { c.StateId, c.DistrictId });

            builder.Property(c => c.StateId)
                .HasColumnName("State")
                .IsRequired();

            builder.Property(c => c.DistrictId)
                .HasColumnName("Districts")
                .IsRequired();

            builder.Property(c => c.Population)
                .HasColumnName("EstimatesPopulation")
                .IsRequired();

            builder.Property(c => c.Households)
                .HasColumnName("EstimatesHouseholds")
                .IsRequired();
        }
    }
}
