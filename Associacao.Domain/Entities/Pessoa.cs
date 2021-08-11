using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Associacao.Domain.Entities
{
    public class Pessoa : BaseDomain
    {
        public string NumeroCadastro { get; set; }
        public string Nome { get; set; }
        public string RG { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }        
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public string Observacao { get; set; }
        public int QuantidadeCasas { get; set; }
        public bool Isento { get; set; }
        public bool Ativo { get; set; }
        public bool Adimplente { get; set; }
        public string Imagem { get; set; }
        //public IFormFile ImagemUpload { get; set; }
        public virtual List<Mensalidade> Mensalidades { get; set; }

    }
}
