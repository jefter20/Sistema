using Sistema.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.DAO
{
    public class ProdutoDAO
    {
        public List<ProdutoEnt> Buscar(ProdutoEnt objTabela)
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Properties.Settings.Default.banco;
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;

                con.Open();

                comando.CommandText = "SELECT * FROM produtos WHERE nome LIKE @nome";

                comando.Parameters.Add("nome", SqlDbType.VarChar).Value = objTabela.Nome + "%";

                comando.Connection = con;

                SqlDataReader dr;
                List<ProdutoEnt> lista = new List<ProdutoEnt>();

                dr = comando.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ProdutoEnt dado = new ProdutoEnt();

                        dado.Id = Convert.ToInt32(dr["id"]);
                        dado.Nome = Convert.ToString(dr["nome"]);
                        dado.Descricao = Convert.ToString(dr["descricao"]);
                        dado.Valor = Convert.ToDecimal(dr["valor"]);

                        lista.Add(dado);
                    }
                }
                return lista;
            }
        }

        public int Inserir(ProdutoEnt objTabela)
        {
            using(SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Properties.Settings.Default.banco;
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;

                con.Open();

                comando.CommandText = "INSERT INTO produtos ([nome], [descricao], [valor]) VALUES (@nome, @descricao, @valor)";

                comando.Parameters.Add("nome", SqlDbType.VarChar).Value = objTabela.Nome;
                comando.Parameters.Add("descricao", SqlDbType.VarChar).Value = objTabela.Descricao;
                comando.Parameters.Add("valor", SqlDbType.Decimal).Value = objTabela.Valor;

                comando.Connection = con;

                int qtd = comando.ExecuteNonQuery();
                Console.Write(qtd);
                return qtd;
            }
        }

        public int Excluir(ProdutoEnt objTabela)
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Properties.Settings.Default.banco;
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;

                con.Open();

                comando.CommandText = "DELETE FROM produtos WHERE id = @id";

                comando.Parameters.Add("id", SqlDbType.Int).Value = objTabela.Id;
                
                comando.Connection = con;

                int qtd = comando.ExecuteNonQuery();
                Console.Write(qtd);
                return qtd;
            }
        }

        public int Editar(ProdutoEnt objTabela)
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Properties.Settings.Default.banco;
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;

                con.Open();

                comando.CommandText = "UPDATE produtos SET nome = @nome, descricao = @descricao, valor = @valor WHERE id = @id";

                comando.Parameters.Add("nome", SqlDbType.VarChar).Value = objTabela.Nome;
                comando.Parameters.Add("descricao", SqlDbType.VarChar).Value = objTabela.Descricao;
                comando.Parameters.Add("valor", SqlDbType.Decimal).Value = objTabela.Valor;
                comando.Parameters.Add("id", SqlDbType.Int).Value = objTabela.Id;

                comando.Connection = con;

                int qtd = comando.ExecuteNonQuery();
                Console.Write(qtd);
                return qtd;
            }
        }

        public List<ProdutoEnt> Lista()
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Properties.Settings.Default.banco;
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;

                con.Open();

                comando.CommandText = "SELECT * FROM produtos ORDER BY nome";

                comando.Connection = con;

                SqlDataReader dr;
                    List<ProdutoEnt> lista = new List<ProdutoEnt>();

                dr = comando.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ProdutoEnt dado = new ProdutoEnt();

                        dado.Id = Convert.ToInt32(dr["id"]);
                        dado.Nome = Convert.ToString(dr["nome"]);
                        dado.Descricao = Convert.ToString(dr["descricao"]);
                        dado.Valor = Convert.ToDecimal(dr["valor"]);

                        lista.Add(dado);
                    }
                }

                return lista;
            }
        }
    }
}
