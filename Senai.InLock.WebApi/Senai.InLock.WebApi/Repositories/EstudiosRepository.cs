using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Repositories
{
    public class EstudiosRepository : IEstudiosRepository
    {

        private string stringConexao = "Data Source=DEV10\\SQLEXPRESS; initial catalog=InLock_Games_Tarde; user Id=sa; pwd=sa@132";

        public List<EstudiosDomain> Listar()
        {
            // Cria uma lista estudioss onde serão armazenados os dados
            List<EstudiosDomain> estudios = new List<EstudiosDomain>();

            // Declara a SqlConnection passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a instrução a ser executada
                string querySelectAll = "SELECT IdEstudio, NomeEstudio FROM Estudios";

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
                        // Instancia um objeto estudios 
                        EstudiosDomain estudio = new EstudiosDomain
                        {
                            // Atribui à propriedade IdEstudios o valor da coluna "IdEstudios" da tabela do banco
                            IdEstudio = Convert.ToInt32(rdr["IdEstudio"])

                            // Atribui à propriedade NomeEstudio o valor da coluna "NomeEstudio" da tabela do banco
                            ,
                            NomeEstudio = rdr["NomeEstudio"].ToString()

                           
                            
                           
                        };

                        // Adiciona o estudios criado à lista estudioss
                        estudios.Add(estudio);
                    }
                }
            }

            // Retorna a lista de estudioss
            return estudios;
        }
    }
    }

