using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Negocio;

namespace TesteCSharp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErro.Text = string.Empty;
            if (this.Context.User.Identity.IsAuthenticated)
            {
                FormLogout.Visible = true;
            }
            else
            {
                FormLogin.Visible = true;
            }
        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            
            try
            {
                Usuario usuario = new Usuario()
                {
                    DesLogin = txtUsuario.Text,
                    DesSenha = txtSenha.Text
                };

                if (usuario.Autenticar())
                {

                    System.Web.Security.FormsAuthentication.SetAuthCookie(usuario.DesNome, true);
                    Response.Redirect("~/Usuarios", false);

                }
                else
                    lblErro.Text = "Usuário e Senha não conferem";
            }
            catch(Exception ex)
            {
                lblErro.Text = ex.Message;
            }
        }
            
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}