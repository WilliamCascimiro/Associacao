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
    public class MensalidadeMap : IEntityTypeConfiguration<Mensalidade>
    {
        public void Configure(EntityTypeBuilder<Mensalidade> builder)
        {
            builder.ToTable("mensalidade");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(x => x.DataVencimento).HasColumnName("data_vencimento").HasMaxLength(100).IsRequired();
            builder.Property(x => x.DataPagamento).HasColumnName("data_pagamento").HasMaxLength(11);
            builder.Property(x => x.Pago).HasColumnName("pago").HasMaxLength(100);
            builder.Property(x => x.Valor).HasColumnName("valor").HasMaxLength(100);
            builder.Property(x => x.CriadoEm).HasColumnName("criado_em").HasMaxLength(100);

            builder.Property(x => x.IdPessoa).HasColumnName("id_pessoa");            
            builder.HasOne(p => p.Pessoa).WithMany(u => u.Mensalidades).HasForeignKey(p => p.IdPessoa);
        }
    }
}
