using PlanoContaHandsOn.Domain.Enums;

namespace PlanoContaHandsOn.Infrastructure.Data.Configurations;

public class PlanoContaConfiguration : IEntityTypeConfiguration<PlanoConta>
{
    public void Configure(EntityTypeBuilder<PlanoConta> builder)
    {
        builder.ToTable("PlanoConta");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Codigo)
            .HasConversion(
                codigoObj => codigoObj.Value,
                dbValue => Codigo.Criar(dbValue))
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(c => c.Tipo)
            .IsRequired();

        builder.Property(c => c.AceitaLancamento)
            .IsRequired();

        builder.HasIndex(p => new { p.Codigo })
            .IsUnique();

        builder.HasIndex(p => new { p.Nome, p.Codigo });

        builder.HasOne<PlanoConta>()
            .WithMany()
            .HasForeignKey(c => c.IdPai)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new
            {
                Id = new Guid("c8b512f4-9478-4222-811d-487b3b8a3e4e"),
                Nome = "Receitas",
                Codigo = Codigo.Criar("1"),
                AceitaLancamento = false,
                Tipo = Tipo.Receita,
                IdPai = (Guid?)null
            },
            new
            {
                Id = new Guid("d6e1b2f8-2c39-4f7a-8f6a-3e21b1e9b8a1"),
                Nome = "Despesas",
                Codigo = Codigo.Criar("2"),
                AceitaLancamento = false,
                Tipo = Tipo.Despesa,
                IdPai = (Guid?)null
            }
        );
    }
}