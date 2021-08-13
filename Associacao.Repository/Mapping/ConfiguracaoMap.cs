using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Associacao.Domain.Entities;

namespace Associacao.Repository.Mapping
{
    public class ConfiguracaoMap : IEntityTypeConfiguration<Configuracao>
    {
        public void Configure(EntityTypeBuilder<Configuracao> builder)
        {
            builder.ToTable("configuracao");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(x => x.DataCobrancaInicial).HasColumnName("data_cobranca_inicial").IsRequired();
            builder.Property(x => x.DataCobrancaFinal).HasColumnName("data_cobranca_final").IsRequired();
            builder.Property(x => x.ValorMensalidade).HasColumnName("valor_mensalidade").IsRequired();
            builder.Property(x => x.DataUltimaAtualizacao).HasColumnName("data_ultima_atualizacao");
            builder.Property(x => x.CriadoEm).HasColumnName("criado_em").HasMaxLength(100);
        }
    }
}
