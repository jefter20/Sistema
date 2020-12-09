using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.DAO;
using Sistema.Entidades;
using System.Data;

namespace Sistema.DAO
{
    public class UsuarioDAO
    {
        public int Inserir(UsuarioEnt objTabela)
        {
            using(SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Properties.Settings.Default.banco;
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;

                con.Open();

                comando.CommandText = "INSERT INTO usuarios ([nome], [usuario], [senha]) VALUES (@nome, @usuario, @senha)";

                comando.Parameters.Add("nome", SqlDbType.VarChar).Value = objTabela.Nome;
                comando.Parameters.Add("usuario", SqlDbType.VarChar).Value = objTabela.Usuario;
                comando.Parameters.Add("senha", SqlDbType.VarChar).Value = objTabela.Senha;

                comando.Connection = con;

                int qtd = comando.ExecuteNonQuery();
                Console.Write(qtd);
                return qtd;
            }
        }

        public int Excluir(UsuarioEnt objTabela)
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Properties.Settings.Default.banco;
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;

                con.Open();

                comando.CommandText = "DELETE FROM usuarios WHERE id = @id";

                comando.Parameters.Add("id", SqlDbType.Int).Value = objTabela.Id;
                
                comando.Connection = con;

                int qtd = comando.ExecuteNonQuery();
                Console.Write(qtd);
                return qtd;
            }
        }

        public int Editar(UsuarioEnt objTabela)
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Properties.Settings.Default.banco;
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;

                con.Open();

                comando.CommandText = "UPDATE usuarios SET nome = @nome, usuario = @usuario, senha = @senha WHERE id = @id";

                comando.Parameters.Add("nome", SqlDbType.VarChar).Value = objTabela.Nome;
                comando.Parameters.Add("usuario", SqlDbType.VarChar).Value = objTabela.Usuario;
                comando.Parameters.Add("senha", SqlDbType.VarChar).Value = objTabela.Senha;
                comando.Parameters.Add("id", SqlDbType.Int).Value = objTabela.Id;

                comando.Connection = con;

                int qtd = comando.ExecuteNonQuery();
                Console.Write(qtd);
                return qtd;
            }
        }

        public UsuarioEnt Login(UsuarioEnt obj)
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Properties.Settings.Default.banco;
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;

                con.Open();

                comando.CommandText = "SELECT * FROM usuarios WHERE usuario = @usuario AND senha  =@senha";

                comando.Connection = con;

                comando.Parameters.Add("usuario", SqlDbType.VarChar).Value = obj.Usuario;
                comando.Parameters.Add("senha", SqlDbType.VarChar).Value = obj.Senha;

                SqlDataReader dr;
                
                dr = comando.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        UsuarioEnt dado = new UsuarioEnt();
                                                
                        dado.Usuario = Convert.ToString(dr["usuario"]);
                        dado.Senha = Convert.ToString(dr["senha"]);                       
                    }
                }
                else
                {
                    obj.Usuario = null;
                    obj.Senha = null;
                }

                return obj;
            }
        }

        public List<UsuarioEnt> Lista()
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = Properties.Settings.Default.banco;
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.Text;

                con.Open();

                comando.CommandText = "SELECT * FROM usuarios ORDER BY nome";

                comando.Connection = con;

                SqlDataReader dr;
                    List<UsuarioEnt> lista = new List<UsuarioEnt>();

                dr = comando.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        UsuarioEnt dado = new UsuarioEnt();

                        dado.Id = Convert.ToInt32(dr["id"]);
                        dado.Nome = Convert.ToString(dr["nome"]);
                        dado.Usuario = Convert.ToString(dr["usuario"]);
                        dado.Senha = Convert.ToString(dr["senha"]);

                        lista.Add(dado);
                    }
                }

                return lista;
            }
        }
    }

    
}
