using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Repositories
{
    public class JogosRepository : IJogosRepository
    {

        private string stringConexao = "Data Source=DEV1301\\SQLEXPRESS; initial catalog=InLock_Games_Tarde; user Id=sa; pwd=sa@132";

        public void Atualizar(int id, JogosDomain jogo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string queryUpdate = "UPDATE Jogos SET NomeJogo = @NomeJogo, Descricao = @Descricao, DataLancamento = @DataLancamento, Valor = @Valor WHERE IdJogo = @ID";

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    // Passa os valores dos parâmetros
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@NomeJogo", jogo.NomeJogo);
                    cmd.Parameters.AddWithValue("@Descricao", jogo.Descricao);
                    cmd.Parameters.AddWithValue("@DataLancamento", jogo.DataLancamento);
                    cmd.Parameters.AddWithValue("@Valor", jogo.Valor);
                    cmd.Parameters.AddWithValue("@IdEstudio", jogo.IdEstudio);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada passando o valor como parâmetro
                string queryDelete = "DELETE FROM Jogos WHERE IdJogo = @ID";

                // Declara o comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@ID", id);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public JogosDomain BuscarPorId(int id)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string querySelectById = "SELECT Jogos.IdJogo, Jogos.NomeJogo, Jogos.Descricao, Jogos.DataLancamento, Jogos.Valor, Estudios.NomeEstudio, Estudios.IdEstudio FROM Jogos" +
                                           " INNER JOIN Estudios ON Jogos.IdEstudio = Estudios.IdEstudio" +
                                        " WHERE IdJogo = @ID";

                // Abre a conexão com o banco de dados
                con.Open();

                // Declara o SqlDataReader para receber os dados do banco de dados
                SqlDataReader rdr;

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@ID", id);

                    // Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    // Caso o resultado da query possua registro
                    if (rdr.Read())
                    {
                        // Instancia um objeto jogo 
                        JogosDomain jogo = new JogosDomain
                        {
                            // Atribui à propriedade IdJogo o valor da coluna "IdJogo" da tabela do banco
                            IdJogo = Convert.ToInt32(rdr["IdJogo"])

                            // Atribui à propriedade Nome o valor da coluna "Nome" da tabela do banco
                            ,
                            NomeJogo = rdr["NomeJogo"].ToString()

                            // Atribui à propriedade Sobrenome o valor da coluna "Sobrenome" da tabela do banco
                            ,
                            Descricao = rdr["Descricao"].ToString()
                                ,
                            DataLancamento = Convert.ToDateTime(rdr["DataLancamento"])
                                ,
                            Valor = Convert.ToInt32(rdr["Valor"])
                                ,
                            Estudios = new EstudiosDomain
                            {
                                // Atribui à propriedade IdEstudio o valor da coluna IdEstudio da tabela do banco de dados
                                IdEstudio = Convert.ToInt32(rdr["IdEstudio"])

                                // Atribui à propriedade Nome o valor da coluna Nome da tabela do banco de dados
                                ,
                                NomeEstudio = rdr["NomeEstudio"].ToString()
                            }
                        };

                        // Retorna o funcionário buscado
                        return jogo;
                    }

                    // Caso o resultado da query não possua registros, retorna null
                    return null;
                }
            }
        }

        

        public void Cadastrar(JogosDomain novoJogo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string queryInsert = "INSERT INTO Jogos(NomeJogo, Descricao, DataLancamento, Valor, IdEstudio)" +
                    "VALUES (@NomeJogo, @Descricao, @DataLancamento, @Valor, @IdEstudio)";
                // Declara o comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@NomeJogo", novoJogo.NomeJogo);
                    cmd.Parameters.AddWithValue("@Descricao", novoJogo.Descricao);
                    cmd.Parameters.AddWithValue("@DataLancamento", novoJogo.DataLancamento);
                    cmd.Parameters.AddWithValue("@Valor", novoJogo.Valor);
                    cmd.Parameters.AddWithValue("@IdEstudio", novoJogo.IdEstudio);
                    // Abre a conexão com o banco de dados
                    con.Open();
                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<JogosDomain> Listar()
        {
            // Cria uma lista jogoss onde serão armazenados os dados
            List<JogosDomain> jogos = new List<JogosDomain>();

            // Declara a SqlConnection passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a instrução a ser executada
                string querySelectAll = "SELECT Jogos.IdJogo, Jogos.NomeJogo, Jogos.Descricao, Jogos.DataLancamento, Jogos.Valor, Estudios.NomeEstudio, Estudios.IdEstudio FROM Jogos" +
                                       " INNER JOIN Estudios ON Jogos.IdEstudio = Estudios.IdEstudio";

                // Abre a conexão com o banco de dados
                con.Open();

                // Declara o SqlDataReader para receber os dados do banco de dados
                SqlDataReader rdr;

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    // Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    // Enquanto houver registros para serem lidos no rdr, o laço se repete
                    while (rdr.Read())
                    {
                        // Instancia um objeto jogos 
                        JogosDomain jogo = new JogosDomain
                        {
                            // Atribui à propriedade IdJogos o valor da coluna "IdJogos" da tabela do banco
                            IdJogo = Convert.ToInt32(rdr["IdJogo"])
                            // Atribui à propriedade NomeJogo o valor da coluna "NomeJogo" da tabela do banco
                            ,
                            NomeJogo = rdr["NomeJogo"].ToString()
                            ,
                            Descricao = rdr["Descricao"].ToString()
                            ,
                            DataLancamento = Convert.ToDateTime(rdr["DataLancamento"])
                            ,
                            Valor = Convert.ToInt32(rdr["Valor"])
                            ,
                            Estudios = new EstudiosDomain
                            {
                                // Atribui à propriedade IdEstudio o valor da coluna IdEstudio da tabela do banco de dados
                                IdEstudio = Convert.ToInt32(rdr["IdEstudio"])

                            // Atribui à propriedade Nome o valor da coluna Nome da tabela do banco de dados
                            ,
                                NomeEstudio = rdr["NomeEstudio"].ToString()
                            }
                        };

                        // Adiciona o jogos criado à lista jogoss
                        jogos.Add(jogo);
                    }
                }
            }

            // Retorna a lista de jogoss
            return jogos;
        }
    }
}

