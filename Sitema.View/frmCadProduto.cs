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
    public partial class frmCadProduto : Form
    {
        ProdutoEnt objTabela = new ProdutoEnt();
        public frmCadProduto()
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

                        List<ProdutoEnt> lista = new List<ProdutoEnt>();
                        lista = new ProdutoModel().Buscar(objTabela);
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
                        if (txtNome.Text == "" | txtDescricao.Text == "" | txtValor.Text == "")
                        {
                            MessageBox.Show("Preencha todos os campos!");
                            txtNome.Focus();
                            return;
                        }

                        objTabela.Nome = txtNome.Text;
                        objTabela.Descricao = txtDescricao.Text;
                        objTabela.Valor = Convert.ToDecimal(txtValor.Text);

                        int x = ProdutoModel.Inserir(objTabela);

                        if (x > 0)
                        {
                            MessageBox.Show(string.Format("Produto {0} inserido com sucesso!", txtNome.Text));
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

                        int x = ProdutoModel.Excluir(objTabela);

                        if (x > 0)
                        {
                            MessageBox.Show(string.Format("Produto {0} excluido com sucesso!", txtNome.Text));
                            LimparCampos();
                            DesabilitarCampos();
                            ListarGrid();
                        }
                        else
                        {
                            MessageBox.Show("Produto não Excluido!");
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
                        objTabela.Descricao = txtDescricao.Text.ToString();
                        objTabela.Valor = Convert.ToDecimal(txtValor.Text);

                        int x = ProdutoModel.Editar(objTabela);

                        if (x > 0)
                        {
                            MessageBox.Show(string.Format("Produto {0} alterado com sucesso!", txtNome.Text));
                            LimparCampos();
                            DesabilitarCampos();
                            ListarGrid();
                        }
                        else
                        {
                            MessageBox.Show("Produto não alterado!");
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
            txtDescricao.Enabled = true;
            txtValor.Enabled = true;
        }

        private void DesabilitarCampos()
        {
            txtNome.Enabled = false;
            txtDescricao.Enabled = false;
            txtValor.Enabled = false;
        }

        private void LimparCampos()
        {
            txtNome.Text = "";
            txtDescricao.Text = "";
            txtValor.Text = "";
        }

        public void ListarGrid()
        {
            try
            {
                List<ProdutoEnt> lista = new List<ProdutoEnt>();
                lista = new ProdutoModel().Lista();
                Grid.AutoGenerateColumns = false;
                Grid.DataSource = lista;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao listar Dados" + ex.Message);
            }
        }

        private void frmCadProduto_Load(object sender, EventArgs e)
        {
            ListarGrid();

            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CodigoId = Grid.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = Grid.CurrentRow.Cells[1].Value.ToString();
            txtDescricao.Text = Grid.CurrentRow.Cells[2].Value.ToString();
            txtValor.Text = Grid.CurrentRow.Cells[3].Value.ToString();

            HabilitarCampos();

            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
        }

        private void btnNovo_Click_1(object sender, EventArgs e)
        {
            opcoes = "Novo";
            iniciarOpcoes();
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            opcoes = "Salvar";
            iniciarOpcoes();
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            opcoes = "Excluir";
            iniciarOpcoes();
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            opcoes = "Editar";
            iniciarOpcoes();
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                ListarGrid();
                return;
            }

            opcoes = "Buscar";
            iniciarOpcoes();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            frmCadUsuario form = new frmCadUsuario();
            this.Hide();
            form.Show();
            form.ListarGrid();
        }
    }
}
