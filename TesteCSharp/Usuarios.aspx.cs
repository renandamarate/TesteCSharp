using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace TesteCSharp
{
    public partial class Usuarios : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            grdUsuarios.DataSource = Usuario.ObterUsuarios();
            grdUsuarios.DataBind();

            if (!this.IsPostBack)
            {
                ddlTipoContato1.DataSource = TipoContato.ObterTiposContato();
                ddlTipoContato1.DataBind();
                ddlTipoContato1.Items.Insert(0, new ListItem(""));

                ddlTipoContato2.DataSource = TipoContato.ObterTiposContato();
                ddlTipoContato2.DataBind();
                ddlTipoContato2.Items.Insert(0, new ListItem(""));

                ddlTipoContato3.DataSource = TipoContato.ObterTiposContato();
                ddlTipoContato3.DataBind();
                ddlTipoContato3.Items.Insert(0, new ListItem(""));

                ddlTipoContato4.DataSource = TipoContato.ObterTiposContato();
                ddlTipoContato4.DataBind();
                ddlTipoContato4.Items.Insert(0, new ListItem(""));

                ddlTipoContato5.DataSource = TipoContato.ObterTiposContato();
                ddlTipoContato5.DataBind();
                ddlTipoContato5.Items.Insert(0, new ListItem(""));

            }
        }

        protected void BindUsuarios()
        {
            grdUsuarios.DataSource = Usuario.ObterUsuarios();
            grdUsuarios.DataBind();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSenha.Text != txtConfirmarSenha.Text)
                    throw new Exception("Confirmação de Senha não confere com a Senha digitada.");

                List<Contato> lstContato = new List<Contato>() {
                    new Contato(ddlTipoContato1.SelectedValue, txtDDD1.Text, txtTelefone1.Text),
                    new Contato(ddlTipoContato2.SelectedValue, txtDDD2.Text, txtTelefone2.Text),
                    new Contato(ddlTipoContato3.SelectedValue, txtDDD3.Text, txtTelefone3.Text),
                    new Contato(ddlTipoContato4.SelectedValue, txtDDD4.Text, txtTelefone4.Text),
                    new Contato(ddlTipoContato5.SelectedValue, txtDDD5.Text, txtTelefone5.Text)
                };
                DateTime tmp;
                var resultado = DateTime.TryParse(txtDataNascimento.Text, out tmp);
                if (!resultado)
                    throw new Exception("Data de Nascimento não confere.");

                Usuario usuario = new Usuario()
                {
                    DesLogin = txtLogin.Text,
                    DesSenha = txtSenha.Text,
                    DtaNascimento = DateTime.Parse(txtDataNascimento.Text),
                    NumCPF = txtCPF.Text,
                    DesEmail = txtEmail.Text
                };
                usuario.Validar();
                usuario.ValidarContatos(lstContato);
                usuario.Salvar();
            }
            catch (Exception ex)
            {
                lblErro.Text = ex.Message;
            }
        }

        protected void grdUsuarios_RowUpdating(object sender, GridViewUpdateEventArgs e) {

            int userid = Convert.ToInt32(grdUsuarios.DataKeys[e.RowIndex].Value);  
            GridViewRow row = (GridViewRow) grdUsuarios.Rows[e.RowIndex];
            TextBox nome = (TextBox) row.Cells[1].Controls[0];  
            TextBox senha = (TextBox) row.Cells[2].Controls[0];  
            TextBox dataNasc = (TextBox) row.Cells[3].Controls[0];  
            TextBox CPF = (TextBox) row.Cells[4].Controls[0];  
            TextBox email = (TextBox) row.Cells[5].Controls[0];

            Usuario usuario = new Usuario()
            {
                CodUsuario = userid,
                DesNome = nome.Text,
                DesSenha = senha.Text,
                DtaNascimento = DateTime.Parse(dataNasc.Text),
                NumCPF = CPF.Text,
                DesEmail = email.Text
            };
            usuario.Editar();
            grdUsuarios.EditIndex = -1;
            BindUsuarios();
        }
        protected void grdUsuarios_RowEditing(object sender, GridViewEditEventArgs e)
        {

            grdUsuarios.EditIndex = e.NewEditIndex;
            BindUsuarios();
            //gvbind();
        }
        protected void grdUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e) {  
            grdUsuarios.PageIndex = e.NewPageIndex;
            BindUsuarios();
            //gvbind();  
        }  
        protected void grdUsuarios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) {  
            grdUsuarios.EditIndex = -1;
            BindUsuarios();
            //gvbind();  
        }
        protected void grdUsuarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)grdUsuarios.Rows[e.RowIndex];
            int userid = Convert.ToInt32(grdUsuarios.DataKeys[e.RowIndex].Value);
            Usuario usuario = new Usuario()
            {
                CodUsuario = userid
            };
            usuario.Remover();
            BindUsuarios();
            //conn.Open();
            //SqlCommand cmd = new SqlCommand("delete FROM detail where id='" + Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString()) + "'", conn);
            //cmd.ExecuteNonQuery();
            //conn.Close();
            //gvbind();
        }
    }
}