using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Configuration;

namespace Negocio
{
    public class ParametrosConexao
    {
        #region Atributos
        private string _strSQL;
        #endregion

        #region Propriedades
        public NpgsqlConnection oConn { get; set; }
        public NpgsqlCommand oCmd;
        public NpgsqlDataAdapter da { get; set; }
        public DataTable dt { get; set; }
        public NpgsqlTransaction oConnTransacao { get; set; }
        public string strSQL { get; set; }
        public bool inTransaction { get; set; }
        #endregion Propriedades

        #region Métodos
        /// <summary>
        /// Abre a conexão com o bannco de dados
        /// </summary>
        /// <param name="flgTransaction">Define se será aberta uma transação (begin)</param>
        public void Open(bool flgTransaction = false)
        {
            // Instancia o objeto de conexão
            this.oConn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);

            // Abre a conexão efetivamente
            this.oConn.Open();
            this.inTransaction = false;

            // Verifica se irá trabalhar com transaction
            if (flgTransaction)
                this.BeginTransaction();

            this.oCmd = new NpgsqlCommand();
            this.da = new NpgsqlDataAdapter();
            this.dt = new DataTable();
            this.strSQL = string.Empty;
        }

        public void Close()
        {
            this.oConn.Close();
            this.oConn.Dispose();
            this.oCmd.Dispose();
            this.da.Dispose();
            this.dt.Dispose();
            this.strSQL = string.Empty;

            if (this.inTransaction)
                this.oConnTransacao.Dispose();
        }

        public void PrepareCommand()
        {
            this.oCmd = new NpgsqlCommand(this.strSQL, this.oConn);
        }

        public DataTable ExecuteCommand(string strTableName = "dt")
        {
            DataTable dt = new DataTable(strTableName);

            try
            {
                this.da = new NpgsqlDataAdapter(this.oCmd);
                this.da.Fill(dt);

                return dt;
            }
            finally
            {
                dt.Dispose();
            }
        }

        public void AddParametro(string parametro, object valor, bool considerarEmpty = false)
        {
            // Se estiver true, somente adiciona o parâmetro se o valor não estiver vazio
            if (considerarEmpty)
            {
                if (string.IsNullOrEmpty(valor.ToString().Trim()))
                    return;
            }

            if (!parametro.Contains(":"))
                parametro = ":" + parametro;

            this.oCmd.Parameters.AddWithValue(parametro, valor);
        }

        public void BeginTransaction()
        {
            this.oConnTransacao = this.oConn.BeginTransaction();
            this.inTransaction = true;
        }

        public void Commit()
        {
            this.oConnTransacao.Commit();
            this.oConnTransacao.Dispose();
            this.oConnTransacao = null;
            this.inTransaction = false;
        }

        public void Rollback()
        {
            this.oConnTransacao.Rollback();
            this.oConnTransacao.Dispose();
            this.oConnTransacao = null;
            this.inTransaction = false;
        }
        #endregion Métodos
    }
}
