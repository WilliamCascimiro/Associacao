﻿using System.Collections.Generic;
using Associacao.Domain.Entities;

namespace Associacao.Interface.Repositories
{
    public interface IPessoaRepository
    {
        List<Pessoa> Get();
        List<Pessoa> Search(string text);
        Pessoa Detail(int id);
        void Create(Pessoa pessoa);
        int Alterar(Pessoa pessoa);
        List<Pessoa> GetComplete();
        List<Pessoa> GetComplete(string cadastro, string nome, int slcPagamento);
        void GetComplete2();
    }
}
