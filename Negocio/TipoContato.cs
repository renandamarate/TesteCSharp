using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TipoContato
    {
        public int CodTipoContato { get; set; }
        public string DesTipoContato { get; set; }

        static public List<TipoContato> ObterTiposContato()
        {
            List<TipoContato> lstTiposContato = new List<TipoContato>();
            ParametrosConexao parametrosConexao = new ParametrosConexao();
            parametrosConexao.Open();
            try
            {
                parametrosConexao.strSQL = "SELECT * FROM tcotipo_contato";

                parametrosConexao.PrepareCommand();
                parametrosConexao.dt = parametrosConexao.ExecuteCommand();

                foreach (DataRow row in parametrosConexao.dt.Rows)
                {
                    lstTiposContato.Add(new TipoContato()
                    {
                        CodTipoContato = (int)row["cod_tipo_contato"],
                        DesTipoContato = row["des_tipo_contato"].ToString()
                    });
                }

                return lstTiposContato;
            }
            finally
            {
                parametrosConexao.Close();
            }
        }
    }
}
