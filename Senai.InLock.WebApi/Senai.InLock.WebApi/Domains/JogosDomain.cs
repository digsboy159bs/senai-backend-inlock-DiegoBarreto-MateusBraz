using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Domains
{
    public class JogosDomain
    {
        public int IdJogo { get; set; }

        public string NomeJogo { get; set; }

        public string Descricao { get; set; }

        public DateTime DataLancamento { get; set; }

        public int Valor { get; set; }

        public int IdEstudio { get; set; }

        public EstudiosDomain Estudios { get; set; }


    }
}
