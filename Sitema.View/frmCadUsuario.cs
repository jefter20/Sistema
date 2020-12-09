using Sistema.Entidades;
using Sistema.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sitema.View
{
    public partial class frmCadUsuario : Form
    {
        UsuarioEnt objTabela = new UsuarioEnt();
        public frmCadUsuario()
        {
            InitializeComponent();
        }

        private string opcoes = "";
        private string CodigoId;

        private void iniciarOpcoes()
        {
            switch (opcoes)
            {
                case "Buscar":
                    try
                    {
                        objTabela.Nome = txtBuscar.Text;

                        List<UsuarioEnt> lista = new List<UsuarioEnt>();
                        lista = new UsuarioModel().Buscar(objTabela);
                        Grid.AutoGenerateColumns = false;
                        Grid.DataSource = lista;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao buscar Dados" + ex.Message);
                    }
                    break;

                case "Novo":
                    HabilitarCampos();
                    LimparCampos();
                    break;

                case "Salvar":
                    try
                    {
                        if (txtNome.Text == "" | txtUsuario.Text == "" | txtSenha.Text == "")
                        {
                            MessageBox.Show("Preencha todos os campos!");
                            txtNome.Focus();
                            return;
                        }

                        objTabela.Nome = txtNome.Text;
                        objTabela.Usuario = txtUsuario.Text;
                        objTabela.Senha = txtSenha.Text;

                        int x = UsuarioModel.Inserir(objTabela);
                                                
                        if (x > 0)
                        {
                            MessageBox.Show(string.Format("Usuario {0} inserido com sucesso!", txtNome.Text));
                            LimparCampos();
                            DesabilitarCampos();
                            ListarGrid();
                        }
                        else
                        {
                            MessageBox.Show("Não inserido");    
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro ao salvar " + ex.Message);
                        
                    }
                    break;

                case "Excluir":
                    try
                    { 
                        objTabela.Id = Convert.ToInt32(CodigoId);

                        int x = UsuarioModel.Excluir(objTabela);

                        if (x > 0)
                        {
                            MessageBox.Show(string.Format("Usuario {0} excluido com sucesso!", txtNome.Text));
                            LimparCampos();
                            DesabilitarCampos();
                            ListarGrid();
                        }
                        else
                        {
                            MessageBox.Show("Usuário não Excluido!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro ao excluir " + ex.Message);

                    }
                    break;

                case "Editar":
                    try
                    {
                        objTabela.Id = Convert.ToInt32(CodigoId);
                        objTabela.Nome = txtNome.Text.ToString();
                        objTabela.Usuario = txtUsuario.Text.ToString();
                        objTabela.Senha = txtSenha.Text.ToString();
                                                
                        int x = UsuarioModel.Editar(objTabela);

                        if (x > 0)
                        {
                            MessageBox.Show(string.Format("Usuario {0} alterado com sucesso!", txtNome.Text));
                            LimparCampos();
                            DesabilitarCampos();
                            ListarGrid();
                        }
                        else
                        {
                            MessageBox.Show("Usuário não alterado!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro ao alterar " + ex.Message);

                    }
                    break;
            }
        }
        private void HabilitarCampos()
        {
            txtNome.Enabled = true;
            txtUsuario.Enabled = true;
            txtSenha.Enabled = true;
        }

        private void DesabilitarCampos()
        {
            txtNome.Enabled = false;
            txtUsuario.Enabled = false;
            txtSenha.Enabled = false;
        }

        private void LimparCampos()
        {
            txtNome.Text = "";
            txtUsuario.Text = "";
            txtSenha.Text = "";
        }

        private void frmCadUsuario_Load(object sender, EventArgs e)
        {
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnBuscar.Enabled = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            if (txtBuscar.Text == "")
            {
                MessageBox.Show("Insira sua pesquisa no campo ao lado!");
            }

            opcoes = "Buscar";
            iniciarOpcoes();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            opcoes = "Novo";
            iniciarOpcoes();
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            opcoes = "Salvar";
            iniciarOpcoes();
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            opcoes = "Excluir";
            iniciarOpcoes();
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            opcoes = "Editar";
            iniciarOpcoes();
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        public void ListarGrid()
        {
            try
            {
                List<UsuarioEnt> lista = new List<UsuarioEnt>();
                lista = new UsuarioModel().Lista();
                Grid.AutoGenerateColumns = false;
                Grid.DataSource = lista;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao listar Dados" + ex.Message);
            }
        }

        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CodigoId = Grid.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = Grid.CurrentRow.Cells[1].Value.ToString();
            txtUsuario.Text = Grid.CurrentRow.Cells[2].Value.ToString();
            txtSenha.Text = Grid.CurrentRow.Cells[3].Value.ToString();

            HabilitarCampos();

            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                ListarGrid();
            }

            opcoes = "Buscar";
            iniciarOpcoes();
        }
    }
}
    