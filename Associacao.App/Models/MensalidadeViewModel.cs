using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Associacao.Domain.Entities;

namespace Associacao.App.Models
{
    public class MensalidadeViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Data de vencimento é obrigatória")]
        [DataType(DataType.Date)]
        public DateTime DataVencimento { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Data de pagamento")]
        public DateTime? DataPagamento { get; set; }
        
        public bool Pago { get; set; }

        [Range(1, 1000, ErrorMessage = "Valor de R$ 1,00 até R$ 1.000,00")]
        public float Valor { get; set; }

        public int IdPessoa { get; set; }

        [DisplayName("Pessoa")]
        public virtual Pessoa Pessoa { get; set; }
    }
}
