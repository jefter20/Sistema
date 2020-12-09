using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema.Model;
using Sistema.Entidades;

namespace Sitema.View
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsuario.Text == "") 
                {
                    MessageBox.Show("Preencha o campo Usuário!");
                    txtUsuario.Focus();
                    return;
                }

                if (txtSenha.Text == "")
                {
                    MessageBox.Show("Preencha o campo Senha!");
                    txtSenha.Focus();
                    return;
                }

                UsuarioEnt obj = new UsuarioEnt();
                obj.Usuario = txtUsuario.Text;
                obj.Senha = txtSenha.Text;

                obj = new UsuarioModel().Login(obj);

                if(obj.Usuario == null)
                {
                    lblMensagem.Text = "Usuário ou senha incorretos!";
                    lblMensagem.ForeColor = Color.Red;
                    return;
                }
                
                frmCadUsuario form = new frmCadUsuario();
                this.Hide();
                form.Show();
                form.ListarGrid();
            }

            catch (Exception ex)
            {

                MessageBox.Show("Erro ao logar" + ex.Message);
            }           
        }

        private void CadUsuario_Click(object sender, EventArgs e)
        {
            frmCadUsuario form = new frmCadUsuario();
            this.Hide();
            form.Show();            
        }
    }
}
