using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Associacao.App.Models
{
    public class PessoaViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Número de Cadastro")]
        [MinLength(1)]
        [MaxLength(10)]
        [DisplayName("Número de Cadastro")]
        public string NumeroCadastro { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [MinLength(2)]
        [MaxLength(200)]
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [DisplayName("RG")]
        [MinLength(9)]
        [MaxLength(9)]
        public string RG { get; set; }

        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        //[DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
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

        [DefaultValue(true)]
        [DisplayName("Isento")]
        public bool Isento { get; set; }

        //[Range(1, 100)]
        //[DataType(DataType.Currency)]
        //[Column(TypeName = "decimal(18, 2)")]
        //public decimal Price { get; set; }
    }
}