<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TesteCSharp._Default" %>
<%@ Import Namespace="System.Web.Security" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row" id="FormLogin" runat="server" visible="false">
        <div class="col-md-4">
            <h2>Login</h2>
            <div>
                <asp:Label ID="lblErro" runat="server"></asp:Label>
            </div>
            <dl>
                <dt>
                    Usuário:
                </dt>
                <dd>
                    <asp:TextBox ID="txtUsuario" runat="server" placeholder="Usuário"></asp:TextBox>
                </dd>
                <dt>
                    Senha:
                </dt>
                <dd>
                    <asp:TextBox ID="txtSenha" runat="server" placeholder="Senha" TextMode="Password"></asp:TextBox>
                </dd>
            </dl>
            <asp:Button ID="btnEntrar" runat="server" Text="Entrar" OnClick="btnEntrar_Click" />
        </div>
    </div>

    <div class="row" id="FormLogout" runat="server" visible="false">
        <div class="col-md-4">
            <h2>Deslogar</h2>
            <div>
                <asp:Button ID="btnLogout" runat="server" Text="Sair" OnClick="btnLogout_Click" />
            </div>
        </div>
    </div>

</asp:Content>
