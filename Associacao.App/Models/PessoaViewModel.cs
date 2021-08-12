using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Associacao.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Associacao.App.Models
{
    public class PessoaViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Número de Cadastro")]
        [DefaultValue(1)]
        [DisplayName("Número de Cadastro")]
        public string NumeroCadastro { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "O nome precisa ter entre 2 e 200 caracteres.")]
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [MinLength(9)]
        [MaxLength(9)]
        [DisplayName("RG")]
        public string RG { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayName("Data de Nascimento")]
        public DateTime? DataNascimento { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DisplayName("Telefone 1")]
        [MaxLength(11)]
        [MinLength(10)]
        public string Telefone1 { get; set; }

        [DisplayName("Telefone 2")]
        [MaxLength(11)]
        [MinLength(10)]
        public string Telefone2 { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(2)]
        [MaxLength(50)]
        [DisplayName("Bairro")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(100)]
        [MinLength(2)]
        [DisplayName("Logradouro")]
        public string Logradouro { get; set; }

        [DisplayName("Número")]
        [MaxLength(10)]
        public string Numero { get; set; }

        [DisplayName("Complemento")]
        [MaxLength(100)]
        public string Complemento { get; set; }

        [DisplayName("CEP")]
        [MaxLength(8)]
        public string CEP { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Observacao")]
        [MaxLength(5000)]
        public string Observacao { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DefaultValue(1)]
        [DisplayName("Quantida de Casa(s)")]
        public int QuantidadeCasas { get; set; }

        [DisplayName("Ativo")]
        public bool Ativo { get; set; }

        [DisplayName("Isento")]
        public bool Isento { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CriadoEm { get; set; }

        public virtual List<Mensalidade> Mensalidades { get; set; }

        public string Imagem { get; set; }

        [ScaffoldColumn(false)]
        public IFormFile ImagemUpload { get; set; }

        public bool Adimplente { get; set; }

        public Pessoa ToEntity()
        {
            Pessoa pessoa = new()
            {
                Id = this.Id,
                NumeroCadastro = this.NumeroCadastro,
                Nome = this.Nome,
                RG = this.RG,
                DataNascimento = this.DataNascimento,
                Telefone1 = this.Telefone1,
                Telefone2 = this.Telefone2,
                Bairro = this.Bairro,
                Logradouro = this.Logradouro,
                Numero = this.Numero,
                Complemento = this.Complemento,
                Cep = this.CEP,
                Observacao = this.Observacao,
                QuantidadeCasas = this.QuantidadeCasas,
                Ativo = this.Ativo,
                Isento = this.Isento,
            };
            return pessoa;
        }

        public PessoaViewModel ToViewModel(Pessoa pessoa)
        {
            Id = pessoa.Id;
            NumeroCadastro = pessoa.NumeroCadastro;
            Nome = pessoa.Nome;
            RG = pessoa.RG;
            DataNascimento = pessoa.DataNascimento;
            Telefone1 = pessoa.Telefone1;
            Telefone2 = pessoa.Telefone2;
            Bairro = pessoa.Bairro;
            Logradouro = pessoa.Logradouro;
            Numero = pessoa.Numero;
            Complemento = pessoa.Complemento;
            CEP = pessoa.Cep;
            Observacao = pessoa.Observacao;
            QuantidadeCasas = pessoa.QuantidadeCasas;
            Ativo = pessoa.Ativo;
            Isento = pessoa.Isento;

            return this;
        }

    }
}