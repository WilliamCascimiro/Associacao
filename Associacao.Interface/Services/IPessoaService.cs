﻿using Associacao.Domain.Entities;
using System.Threading.Tasks;

namespace Associacao.Interface.Services
{
    public interface IPessoaService
    {
        Task Cadastrar(Pessoa pessoa);
    }
}
