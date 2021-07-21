using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Associacao.Domain.Entities
{
    public class Mensalidade : BaseDomain
    {
        public DateTime DataVencimento { get; private set; }
        public DateTime? DataPagamento { get; private set; }
        public bool Pago { get; private set; }
        public float Valor { get; private set; }
        public int IdPessoa { get; private set; }
        public virtual Pessoa Pessoa { get; private set; }

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
