using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Contato
    {
        public int CodContato { get; set; }
        public int CodUsuario { get; set; }
        public int CodTipoContato { get; set; }
        public string NumDDD { get; set; }
        public string NumTelefone { get; set; }
        public DateTime DtaCadastro { get; set; }

        public Contato () { }
        public Contato (string selectedValue, string NumDDD, string NumTelefone)
        {
            int tmp;
            var resultado = int.TryParse(selectedValue, out tmp);
            if (resultado)
                CodTipoContato = tmp;
            this.NumDDD = NumDDD;
            this.NumTelefone = NumTelefone;
        }

        static public List<Contato> ObterContatos()
        {
            List<Contato> lstContato = new List<Contato>();
            ParametrosConexao parametrosConexao = new ParametrosConexao();
            parametrosConexao.Open();
            
            parametrosConexao.strSQL = "SELECT * FROM cntcontato cnt";

            parametrosConexao.PrepareCommand();
            parametrosConexao.dt = parametrosConexao.ExecuteCommand();

            foreach (DataRow row in parametrosConexao.dt.Rows)
            {
                lstContato.Add(new Contato() {
                    CodContato = (int)row["cod_contato"],
                    CodUsuario = (int)row["cod_usuario"],
                    CodTipoContato = (int)row["cod_tipo_contato"],
                    DtaCadastro = DateTime.Parse(row["dta_cadastro"].ToString()),
                    NumDDD = row["num_ddd"].ToString(),
                    NumTelefone = row["num_telefone"].ToString()
                });
            }

            return lstContato;
        }


        public bool Validar()
        {
            if (CodTipoContato.Equals(0))
                return false;
            if (NumDDD.Length < 2 || NumDDD.Length > 3)
                return false;
            if (NumTelefone.Length != 8 & CodTipoContato != 2)
                return false;
            else if (NumTelefone.Length != 9 & CodTipoContato == 2)
                return false;
            return true;
        }
    }

    public class ContatoViewModel
    {
        public DateTime DtaCadastro { get; set; }
        public string NomeUsuario { get; set; }
        public string NomeTipoContato { get; set; }
        public string NumDDD { get; set; }
        public string NumTelefone { get; set; }

        static public List<ContatoViewModel> ObterContatos()
        {
            var contatos = Contato.ObterContatos();
            var usuarios = Usuario.ObterUsuarios();
            var tipoContato = TipoContato.ObterTiposContato();

            var res = new List<ContatoViewModel>();
            foreach (var contato in contatos)
            {
                res.Add(new ContatoViewModel
                {
                    DtaCadastro = contato.DtaCadastro,
                    NumDDD = contato.NumDDD,
                    NumTelefone = contato.NumTelefone,
                    NomeUsuario = usuarios.Find(p => p.CodUsuario == contato.CodUsuario).DesNome,
                    NomeTipoContato = tipoContato.Find(p => p.CodTipoContato == contato.CodTipoContato).DesTipoContato
                });
            }
            return res;
        }
    }
}
