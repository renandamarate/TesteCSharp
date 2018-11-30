<%@ Page Title="Contato" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contatos.aspx.cs" Inherits="TesteCSharp.Contatos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    
    <div id="busca">
        <dl>
            <dt>
                Usuário:
            </dt>
            <dd>
                <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
            </dd>
            <dt>
                Tipo de Contato:
            </dt>
            <dd>
                <asp:TextBox ID="txtTipoContato" runat="server"></asp:TextBox>
            </dd>
            <dt>
                Data de cadastro:
            </dt>
            <dd>
                <asp:TextBox ID="txtDataCadastro" runat="server"></asp:TextBox>
            </dd>
        </dl>
        <asp:Button ID="btnBuscar" runat="server" Text="Salvar" OnClick="btnBuscar_Click" />
    </div>
    <div id="tabela" runat="server" Visible="false">
        <asp:GridView ID="grdContatos" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="DtaCadastro" HeaderText="Data de Cadastro" />
                <asp:BoundField DataField="NomeUsuario" HeaderText="Nome" />
                <asp:BoundField DataField="NomeTipoContato" HeaderText="Tipo" />
                <asp:BoundField DataField="NumDDD" HeaderText="DDD" />
                <asp:BoundField DataField="NumTelefone" HeaderText="Telefone" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
