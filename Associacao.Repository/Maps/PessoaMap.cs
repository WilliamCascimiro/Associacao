using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Associacao.Domain.Entities;

namespace Associacao.Repository.Maps
{
    public class PessoaMap : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("pessoa");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(x => x.NumeroCadastro).HasColumnName("numero_cadastro").HasMaxLength(10).IsRequired();
            builder.Property(x => x.Nome).HasColumnName("nome").HasMaxLength(200).IsRequired();
            builder.Property(x => x.RG).HasColumnName("rg").HasMaxLength(9);
            builder.Property(x => x.DataNascimento).HasColumnName("data_nascimento");
            builder.Property(x => x.Telefone1).HasColumnName("telefone1").HasMaxLength(11).IsRequired();
            builder.Property(x => x.Telefone2).HasColumnName("telefone2").HasMaxLength(11);
            builder.Property(x => x.Bairro).HasColumnName("bairro").HasMaxLength(50).IsRequired();
            builder.Property(x => x.Logradouro).HasColumnName("logradouro").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Numero).HasColumnName("numero").HasMaxLength(10);
            builder.Property(x => x.Complemento).HasColumnName("complemento").HasMaxLength(100);
            builder.Property(x => x.Cep).HasColumnName("cep").HasMaxLength(8);
            builder.Property(x => x.QuantidadeCasas).HasColumnName("quantidade_casas").IsRequired();
            builder.Property(x => x.Observacao).HasColumnName("observacao").HasMaxLength(5000);
            builder.Property(x => x.Isento).HasColumnName("isento").IsRequired();
            builder.Property(x => x.Ativo).HasColumnName("ativo").IsRequired();
            builder.Property(x => x.CriadoEm).HasColumnName("criado_em");
            builder.Ignore(x => x.NomeResumido);
            builder.Ignore(x => x.Adimplente);
        }
    }
}
