using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Associacao.Domain.Entities
{
    public class Mensalidade : BaseDomain
    {
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public bool Pago { get; set; }
        public float Valor { get; set; }
        public int IdPessoa { get; set; }
        public virtual Pessoa Pessoa { get; set; }

        public Mensalidade()
        {

        }

        public Mensalidade(int idPessoa)
        {
            IdPessoa = idPessoa;
        }

        public Mensalidade(int idPessoa, DateTime dataVencimento, float valor)
        {
            IdPessoa = idPessoa;
            DataVencimento = dataVencimento;
            Valor = valor;
        }

        public void PagarMensalidade()
        {
            Pago = true;
            DataPagamento = DateTime.Now;
        }

        public void ReabrirMensalidade()
        {
            Pago = false;
            DataPagamento = null;
        }


    }
}
