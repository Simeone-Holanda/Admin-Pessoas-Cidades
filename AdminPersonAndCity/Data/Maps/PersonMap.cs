using AdminPersonAndCity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPersonAndCity.Data.Maps
{
    public class PersonMap : IEntityTypeConfiguration<PersonModel>
    {
        public void Configure(EntityTypeBuilder<PersonModel> builder)
        {
            // campos que não aparecem é pq n tem uma mapeamento ou foi feito por data anotation

            builder.ToTable("persons");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(70);
            builder.Property(x => x.PersonType).IsRequired().HasMaxLength(2);
            builder.Property(x => x.CpfCnpj).IsRequired().HasMaxLength(14);
            builder.Property(x => x.Cep).IsRequired().HasMaxLength(8);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Compl).IsRequired().HasMaxLength(20);
            builder.Property(x => x.District).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(20);
            builder.Property(x => x.RegStatus).IsRequired().HasMaxLength(2);
            builder.HasOne(x => x.City);
        }

    }
}
