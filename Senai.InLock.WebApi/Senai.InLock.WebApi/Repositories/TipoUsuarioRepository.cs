using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        private string stringConexao = "Data Source=DEV1301\\SQLEXPRESS; initial catalog=InLock_Games_Tarde; user Id=sa; pwd=sa@132";

        public TipoUsuarioDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string querySelectById = "SELECT IdTipoUsuario, Titulo FROM TipoUsuario" +
                                        " WHERE IdTipoUsuario = @ID";
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
                        // Instancia um objeto tipousuario
                        TipoUsuarioDomain tipousuario = new TipoUsuarioDomain
                        {
                            // Atribui à propriedade IdTipoUsuario o valor da coluna "IdTipoUsuario" da tabela do banco
                            IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"])
                            // Atribui à propriedade Nome o valor da coluna "Nome" da tabela do banco
                            ,
                            Titulo = rdr["Titulo"].ToString()
                        };
                        // Retorna o funcionário buscado
                        return tipousuario;
                    }
                    // Caso o resultado da query não possua registros, retorna null
                    return null;
                }
            }
        }

        public void Cadastrar(TipoUsuarioDomain novoTipoUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string queryInsert = "INSERT INTO TipoUsuario(Titulo, IdTipoUsuario) VALUES (@Titulo, @IdTipoUsuario)";
                // Declara o comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@Titulo", novoTipoUsuario.Titulo);
                    cmd.Parameters.AddWithValue("@IdTipoUsuario", novoTipoUsuario.IdTipoUsuario);
                    // Abre a conexão com o banco de dados
                    con.Open();
                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<TipoUsuarioDomain> Listar()
        {
            List<TipoUsuarioDomain> tipousuarios = new List<TipoUsuarioDomain>();
            // Declara a SqlConnection passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a instrução a ser executada
                string querySelectAll = "SELECT IdTipoUsuario, Titulo FROM TipoUsuario";
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
                        // Instancia um objeto TipoUsuarios
                        TipoUsuarioDomain tipousuario = new TipoUsuarioDomain
                        {
                            // Atribui à propriedade IdTipoUsuarios o valor da coluna "IdTipoUsuarios" da tabela do banco
                            IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"])
                            // Atribui à propriedade NomeTipoUsuario o valor da coluna "NomeTipoUsuario" da tabela do banco
                            ,
                            Titulo = rdr["Titulo"].ToString()
                        };
                        // Adiciona o TipoUsuarios criado à lista TipoUsuarioss
                        tipousuarios.Add(tipousuario);
                    }
                }
            }
            // Retorna a lista de TipoUsuarioss
            return tipousuarios;
        }
    }
}



