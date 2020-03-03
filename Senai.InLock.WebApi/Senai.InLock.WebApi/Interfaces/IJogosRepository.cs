using Senai.InLock.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Interfaces
{
    interface IJogosRepository
    {
        List<JogosDomain> Listar();

        void Atualizar(int id, JogosDomain jogo);

        void Deletar(int id);

        JogosDomain BuscarPorId(int id);

        void Cadastrar(JogosDomain novoJogo);
    }
}
