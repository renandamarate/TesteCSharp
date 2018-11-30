using System;
using System.Web.UI;
using Negocio;

namespace TesteCSharp
{
    public partial class Contatos : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                grdContatos.DataSource = ContatoViewModel.ObterContatos();
                grdContatos.DataBind();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            tabela.Visible = true;

            var contatos = ContatoViewModel.ObterContatos();
            if (!string.IsNullOrEmpty(txtDataCadastro.Text))
                contatos.RemoveAll(p => !p.DtaCadastro.ToString().Contains(txtDataCadastro.Text));

            if (!string.IsNullOrEmpty(txtTipoContato.Text))
                contatos.RemoveAll(p => !p.NomeTipoContato.Contains(txtTipoContato.Text));

            if (!string.IsNullOrEmpty(txtUsuario.Text))
                contatos.RemoveAll(p => !p.NomeUsuario.Contains(txtUsuario.Text));

            grdContatos.DataSource = contatos;
            grdContatos.DataBind();
        }
    }
}