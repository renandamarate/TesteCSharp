using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Negocio
{
    public class Usuario
    {
        #region Propriedades
        public int CodUsuario { get; set; }
        public string DesNome { get; set; }
        public string DesLogin { get; set; }
        public string DesSenha { get; set; }
        public DateTime DtaNascimento { get; set; }
        public string NumCPF { get; set; }
        public string DesEmail { get; set; }
        public List<Contato> LstContato { get; set; }
        #endregion

        public bool Autenticar()
        {
            ParametrosConexao parametrosConexao = new ParametrosConexao();
            parametrosConexao.Open();
            try
            {
                parametrosConexao.strSQL =
                    "SELECT                                        " +
                    "  usr.cod_usuario,                            " +
                    "  usr.des_nome                                " +
                    "FROM                                          " +
                    "  usrusuario usr                              " +
                    "WHERE TRUE                                    " +
                    "  AND usr.des_login = :des_login              " +
                    "  AND usr.des_senha = :des_senha              ";

                parametrosConexao.PrepareCommand();
                parametrosConexao.AddParametro(":des_login", this.DesLogin);
                parametrosConexao.AddParametro(":des_senha", GetMd5Hash(this.DesSenha));
                parametrosConexao.dt = parametrosConexao.ExecuteCommand();

                if (parametrosConexao.dt.Rows.Count > 0)
                {
                    this.CodUsuario = (int)parametrosConexao.dt.Rows[0]["cod_usuario"];
                    this.DesNome = parametrosConexao.dt.Rows[0]["des_nome"].ToString();
                    this.DesSenha = string.Empty;
                    //FormsAuthentication.SetAuthCookie(DesNome, true);
                    HttpContext.Current.Session["Usuario"] = this;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                parametrosConexao.Close();
            }
        }

        public void Salvar()
        {
            ParametrosConexao parametrosConexao = new ParametrosConexao();
            parametrosConexao.Open(true);

            try
            {
                if (this.CodUsuario > 0)
                {
                    parametrosConexao.strSQL =
                        "UPDATE                                                                         " +
                        "  usrusuario                                                                   " +
                        "SET                                                                            " +
                        "  des_nome = :des_nome, des_login = :des_login, des_senha = :des_senha,        " +
                        "  dta_nascimento = :dta_nascimento, num_cpf = :num_cpf, des_email = :des_email " +
                        "WHERE                                                                          " +
                        "  cod_usuario = :cod_usuario                                                   " +
                        "RETURNING cod_usuario;--";
                }
                else
                {
                    parametrosConexao.strSQL =
                        "INSERT INTO usrusuario                                                        " +
                        "  (des_nome, des_login, des_senha, dta_nascimento, num_cpf, des_email)        " +
                        "VALUES                                                                        " +
                        "  (:des_nome, :des_login, :des_senha, :dta_nascimento, :num_cpf, :des_email) " +
                        "RETURNING cod_usuario;--                                                        ";
                }

                parametrosConexao.PrepareCommand();
                parametrosConexao.AddParametro(":des_nome", this.DesNome);
                parametrosConexao.AddParametro(":des_login", this.DesLogin);
                parametrosConexao.AddParametro(":des_senha", GetMd5Hash(this.DesSenha));
                parametrosConexao.AddParametro(":dta_nascimento", this.DtaNascimento);
                parametrosConexao.AddParametro(":num_cpf", this.NumCPF);
                parametrosConexao.AddParametro(":des_email", this.DesEmail);
                parametrosConexao.AddParametro(":cod_usuario", this.CodUsuario);
                this.CodUsuario = (int)parametrosConexao.oCmd.ExecuteScalar();

                foreach (Contato contato in this.LstContato)
                {
                    parametrosConexao.strSQL =
                        "INSERT INTO cntcontato                                       " +
                        "  (cod_tipo_contato, cod_usuario, num_ddd, num_telefone)     " +
                        "VALUES                                                       " +
                        "  (:cod_tipo_contato, :cod_usuario, :num_ddd, :num_telefone); ";

                    parametrosConexao.PrepareCommand();
                    parametrosConexao.AddParametro(":cod_tipo_contato", contato.CodTipoContato);
                    parametrosConexao.AddParametro(":cod_usuario", this.CodUsuario);
                    parametrosConexao.AddParametro(":num_ddd", contato.NumDDD);
                    parametrosConexao.AddParametro(":num_telefone", contato.NumTelefone);
                    var e = parametrosConexao.oCmd.ExecuteNonQuery();
                }
            }
            finally
            {
                parametrosConexao.Close();
            }
        }

        public void Editar()
        {
            ParametrosConexao parametrosConexao = new ParametrosConexao();
            parametrosConexao.Open(true);

            try
            {
                    parametrosConexao.strSQL =
                        "UPDATE                                                                         " +
                        "  usrusuario                                                                   " +
                        "SET                                                                            " +
                        "  des_nome = :des_nome, des_senha = :des_senha,        " +
                        "  dta_nascimento = :dta_nascimento, num_cpf = :num_cpf, des_email = :des_email " +
                        "WHERE                                                                          " +
                        "  cod_usuario = :cod_usuario                                                   " +
                        "RETURNING cod_usuario                                                          ";
              
                parametrosConexao.PrepareCommand();
                parametrosConexao.AddParametro(":des_nome", this.DesNome);
                parametrosConexao.AddParametro(":des_senha", GetMd5Hash(this.DesSenha));
                parametrosConexao.AddParametro(":dta_nascimento", this.DtaNascimento);
                parametrosConexao.AddParametro(":num_cpf", this.NumCPF);
                parametrosConexao.AddParametro(":des_email", this.DesEmail);
                parametrosConexao.AddParametro(":cod_usuario", this.CodUsuario);
            }
            finally
            {
                parametrosConexao.Close();
            }
        }

        public void Remover()
        {
            ParametrosConexao parametrosConexao = new ParametrosConexao();
            parametrosConexao.Open(true);

            try
            {

                parametrosConexao.strSQL =
                    "DELETE FROM                                                                    " +
                    "  cntcontato                                                                   " +
                    "WHERE                                                                          " +
                    "  cod_usuario = :cod_usuario;                                                  ";

                parametrosConexao.PrepareCommand();
                parametrosConexao.AddParametro(":cod_usuario", this.CodUsuario);
                var e = parametrosConexao.oCmd.ExecuteNonQuery();

                parametrosConexao.strSQL =
                    "DELETE FROM                                                                    " +
                    "  usrusuario                                                                   " +
                    "WHERE                                                                          " +
                    "  cod_usuario = :cod_usuario;                                                  ";

                parametrosConexao.PrepareCommand();
                parametrosConexao.AddParametro(":cod_usuario", this.CodUsuario);
                e = parametrosConexao.oCmd.ExecuteNonQuery();
            }
            finally
            {
                parametrosConexao.Close();
            }
        }

        static string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Cnverte a String para um array de Bytes e computa o hash
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Cria um StringBuilder para coletar os Bytes
                StringBuilder sBuilder = new StringBuilder();

                // Percorre o array de bytes criptografados e passa para o valor hexadecimal
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Retorna a string no formato Hexadecimal
                return sBuilder.ToString().ToUpper();
            }
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(DesEmail))
                throw new Exception("Email não preenchido.");
            if (string.IsNullOrEmpty(DesNome))
                throw new Exception("Nome não preenchido.");
            if (string.IsNullOrEmpty(DesSenha))
                throw new Exception("Senha não preenchida.");
            if (DtaNascimento.Equals(DateTime.MinValue) || DtaNascimento >= DateTime.Today)
                throw new Exception("Data inválida.");
            //if (string.IsNullOrEmpty(NumCPF) || NumCPF.Length > 13 || NumCPF.Length < 11)
            if (string.IsNullOrEmpty(NumCPF) || NumCPF.Length > 11)
                throw new Exception("CPF inválido.");
        }


        public static List<Usuario> ObterUsuarios()
        {
            List<Usuario> lstUsuarios = new List<Usuario>();
            ParametrosConexao parametrosConexao = new ParametrosConexao();
            parametrosConexao.Open();

            parametrosConexao.strSQL = "SELECT * FROM usrusuario usr";

            parametrosConexao.PrepareCommand();
            parametrosConexao.dt = parametrosConexao.ExecuteCommand();

            foreach (DataRow row in parametrosConexao.dt.Rows)
            {
                DateTime tmp;
                var res = DateTime.TryParse(row["dta_nascimento"].ToString(), out tmp);
                var NumCPF = row["num_cpf"].ToString();
                var DesEmail = row["des_email"].ToString();
                lstUsuarios.Add(new Usuario()
                {
                    CodUsuario = (int)row["cod_usuario"],
                    DesNome = (string)row["des_nome"],
                    DesLogin = (string)row["des_login"],
                    DesSenha = (string)row["des_senha"],
                    DtaNascimento = res ? tmp : DateTime.MinValue,
                    NumCPF = row["num_cpf"].ToString(),
                    DesEmail = row["des_email"].ToString()
                });
            }

            return lstUsuarios;
        }


        public void ValidarContatos(List<Contato> contatos)
        {
            LstContato = new List<Contato>();
            foreach (var contato in contatos)
            {
                if (contato.Validar())
                    if (LstContato.FindAll(p => p.CodTipoContato == contato.CodTipoContato).Count < 2)
                        LstContato.Add(contato);
            }
        }
    }
}
