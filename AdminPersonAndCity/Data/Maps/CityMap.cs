using AdminPersonAndCity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPersonAndCity.Data.Maps
{
    public class CityMap : IEntityTypeConfiguration<CityModel>
    {
        public void Configure(EntityTypeBuilder<CityModel> builder)
        {
            builder.ToTable("cities");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(25);
            builder.Property(x => x.State).HasMaxLength(20);
        }
    }
}
