using Associacao.App.Models;
using Associacao.Domain.Entities;
using AutoMapper;

namespace Associacao.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Pessoa, PessoaViewModel>().ReverseMap();
            CreateMap<Mensalidade, MensalidadeViewModel>().ReverseMap();
            CreateMap<Configuracao, ConfiguracaoViewModel>().ReverseMap();
        }
    }
}
