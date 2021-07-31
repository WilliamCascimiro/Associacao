using System;
using System.Collections.Generic;
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

        public string MesVencimento { get; set; }


        public Mensalidade ToEntity()
        {
            Mensalidade mensalidade = new()
            {
                Id = this.Id,
                DataVencimento = this.DataVencimento,
                DataPagamento = this.DataPagamento,
                Pago = this.Pago,
                Valor = this.Valor,
                IdPessoa = this.IdPessoa,
                Pessoa = this.Pessoa
            };
            return mensalidade;
        }

        public MensalidadeViewModel ToViewModel(Mensalidade mensalidade)
        {
            Id = mensalidade.Id;
            DataVencimento = mensalidade.DataVencimento;
            DataPagamento = mensalidade.DataPagamento;
            Pago = mensalidade.Pago;
            Valor = mensalidade.Valor;
            IdPessoa = mensalidade.IdPessoa;
            Pessoa = mensalidade.Pessoa;
            MesVencimento = DataVencimento.Month.ToString("d2");
            return this;
        }

        public List<MensalidadeViewModel> ToListViewModel(IList<Mensalidade> mensalidades)
        {
            List<MensalidadeViewModel> lUserViewModel = new();

            if (mensalidades != null)
            {
                foreach (var model in mensalidades)
                {
                    MensalidadeViewModel user = new();
                    user.ToViewModel(model);

                    lUserViewModel.Add(user);
                }
            }

            return lUserViewModel;
        }

    }
}
