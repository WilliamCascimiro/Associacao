using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Associacao.Domain.Entities
{
    public class Configuracao
    {
        public int Id { get; set; }
        public DateTime DataCobrancaInicial { get; set; }
        public DateTime DataCobrancaFinal { get; set; }
        public float ValorMensalidade { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; }
    }
}
