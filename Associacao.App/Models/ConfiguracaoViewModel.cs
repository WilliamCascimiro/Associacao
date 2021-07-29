using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Associacao.App.Models
{
    public class ConfiguracaoViewModel
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [DisplayName("Data de cobranca inicial")]
        public DateTime DataCobrancaInicial { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [DisplayName("Data de cobranca final")]
        public DateTime DataCobrancaFinal { get; set; }

        //[DataType(DataType.Currency)]
        //[DisplayFormat(DataFormatString = "{0:C0}")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [DisplayName("Valor da mensalidade")]
        public string ValorMensalidade { get; set; }

        //[DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        //[Required(ErrorMessage = "Campo obrigatório")]
        //[DisplayName("Data da última atualizacao")]
        //public DateTime DataUltimaAtualizacao { get; set; }
    }
}
