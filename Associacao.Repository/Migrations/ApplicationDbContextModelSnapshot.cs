﻿// <auto-generated />
using System;
using Associacao.Repository.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Associacao.Repository.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Associacao.Domain.Entities.Mensalidade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CriadoEm")
                        .HasMaxLength(100)
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("criado_em");

                    b.Property<DateTime?>("DataPagamento")
                        .HasMaxLength(11)
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("data_pagamento");

                    b.Property<DateTime>("DataVencimento")
                        .HasMaxLength(100)
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("data_vencimento");

                    b.Property<int>("IdPessoa")
                        .HasColumnType("integer")
                        .HasColumnName("id_pessoa");

                    b.Property<bool>("Pago")
                        .HasMaxLength(100)
                        .HasColumnType("boolean")
                        .HasColumnName("pago");

                    b.Property<float>("Valor")
                        .HasMaxLength(100)
                        .HasColumnType("real")
                        .HasColumnName("valor");

                    b.HasKey("Id");

                    b.HasIndex("IdPessoa");

                    b.ToTable("mensalidade");
                });

            modelBuilder.Entity("Associacao.Domain.Entities.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean")
                        .HasColumnName("ativo");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("bairro");

                    b.Property<string>("Cep")
                        .HasMaxLength(8)
                        .HasColumnType("character varying(8)")
                        .HasColumnName("cep");

                    b.Property<string>("Complemento")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("complemento");

                    b.Property<DateTime>("CriadoEm")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("criado_em");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("data_nascimento");

                    b.Property<bool>("Isento")
                        .HasColumnType("boolean")
                        .HasColumnName("isento");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("logradouro");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("nome");

                    b.Property<string>("Numero")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("numero");

                    b.Property<string>("NumeroCadastro")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("numero_cadastro");

                    b.Property<string>("Observacao")
                        .HasMaxLength(5000)
                        .HasColumnType("character varying(5000)")
                        .HasColumnName("observacao");

                    b.Property<int>("QuantidadeCasas")
                        .HasColumnType("integer")
                        .HasColumnName("quantidade_casas");

                    b.Property<string>("RG")
                        .HasMaxLength(9)
                        .HasColumnType("character varying(9)")
                        .HasColumnName("rg");

                    b.Property<string>("Telefone1")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)")
                        .HasColumnName("telefone1");

                    b.Property<string>("Telefone2")
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)")
                        .HasColumnName("telefone2");

                    b.HasKey("Id");

                    b.ToTable("pessoa");
                });

            modelBuilder.Entity("Associacao.Domain.Entities.Mensalidade", b =>
                {
                    b.HasOne("Associacao.Domain.Entities.Pessoa", "Pessoa")
                        .WithMany("Mensalidades")
                        .HasForeignKey("IdPessoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("Associacao.Domain.Entities.Pessoa", b =>
                {
                    b.Navigation("Mensalidades");
                });
#pragma warning restore 612, 618
        }
    }
}
