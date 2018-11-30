<%@ Page Title="Usuários" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="TesteCSharp.Usuarios" EnableEventValidation="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <%--<script type="text/javascript">
        $(document).ready(function () {
            var tab = document.getElementById('<%= hidTAB.ClientID%>').value;
            $('#tabs a[href="' + tab + '"]').tab('show');
        });
    </script>--%>
    <h2><%: Title %>.</h2>
    <div>
        <asp:Label ID="lblErro" runat="server"></asp:Label>
    </div>

    <asp:HiddenField ID="hidTAB" runat="server" Value="#novo_usuario" />

    <ul class="nav nav-tabs">
        <li class="active"><a data-toggle="tab" href="#novo_usuario">Novo</a></li>
        <li><a data-toggle="tab" href="#pesquisar_usuario">Pesquisar</a></li>
      </ul>

  <div class="tab-content">
    <div id="novo_usuario" class="tab-pane fade in active">
        <dl>
            <dt>
                Nome:
            </dt>
            <dd>
                <asp:TextBox ID="txtNome" runat="server"></asp:TextBox>
            </dd>
            <dt>
                Login:
            </dt>
            <dd>
                <asp:TextBox ID="txtLogin" runat="server"></asp:TextBox>
            </dd>
            <dt>
                Senha:
            </dt>
            <dd>
                <asp:TextBox ID="txtSenha" runat="server" TextMode="Password"></asp:TextBox>
            </dd>
            <dt>
                Confirmar Senha:
            </dt>
            <dd>
                <asp:TextBox ID="txtConfirmarSenha" runat="server" TextMode="Password"></asp:TextBox>
            </dd>
            <dt>
                Data de Nascimento:
            </dt>
            <dd>
                <asp:TextBox ID="txtDataNascimento" runat="server"></asp:TextBox>
            </dd>
            <dt>
                CPF:
            </dt>
            <dd>
                <asp:TextBox ID="txtCPF" runat="server"></asp:TextBox>
            </dd>
            <dt>
                E-mail:
            </dt>
            <dd>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            </dd>
          <%--  <dt>
                Tipo de Contato:
            </dt>
            <dd>
                <asp:DropDownList ID="ddlTipoContato" runat="server" DataTextField="DesTipoContato" DataValueField="CodTipoContato"></asp:DropDownList>
            </dd>--%>
            <dt>
                DDD/Telefone:
            </dt>
            <dd>
                <asp:DropDownList ID="ddlTipoContato1" runat="server" DataTextField="DesTipoContato" DataValueField="CodTipoContato"></asp:DropDownList>
                <asp:TextBox ID="txtDDD1" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtTelefone1" runat="server"></asp:TextBox>
            </dd>
            <dd>
                <asp:DropDownList ID="ddlTipoContato2" runat="server" DataTextField="DesTipoContato" DataValueField="CodTipoContato"></asp:DropDownList>
                <asp:TextBox ID="txtDDD2" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtTelefone2" runat="server"></asp:TextBox>
            </dd>
            <dd>
                <asp:DropDownList ID="ddlTipoContato3" runat="server" DataTextField="DesTipoContato" DataValueField="CodTipoContato"></asp:DropDownList>
                <asp:TextBox ID="txtDDD3" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtTelefone3" runat="server"></asp:TextBox>
            </dd>
            <dd>
                <asp:DropDownList ID="ddlTipoContato4" runat="server" DataTextField="DesTipoContato" DataValueField="CodTipoContato"></asp:DropDownList>
                <asp:TextBox ID="txtDDD4" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtTelefone4" runat="server"></asp:TextBox>
            </dd>
            <dd>
                <asp:DropDownList ID="ddlTipoContato5" runat="server" DataTextField="DesTipoContato" DataValueField="CodTipoContato"></asp:DropDownList>
                <asp:TextBox ID="txtDDD5" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtTelefone5" runat="server"></asp:TextBox>
            </dd>
        </dl>
        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
    </div>
    <div id="pesquisar_usuario" class="tab-pane fade">
       
            <div>  
                <asp:GridView ID="grdUsuarios" runat="server" AutoGenerateColumns="false" DataKeyNames="CodUsuario" OnPageIndexChanging="grdUsuarios_PageIndexChanging" OnRowCancelingEdit="grdUsuarios_RowCancelingEdit" OnRowDeleting="grdUsuarios_RowDeleting" OnRowEditing="grdUsuarios_RowEditing" OnRowUpdating="grdUsuarios_RowUpdating">  
                <Columns>
                    <asp:BoundField runat="server" DataField="CodUsuario" HeaderText="Cod"  />
                    <asp:BoundField runat="server" DataField="DesNome" HeaderText="Nome" />
                    <asp:BoundField runat="server" DataField="DesSenha" HeaderText="Senha" />
                    <asp:BoundField runat="server" DataField="DtaNascimento" HeaderText="Data de Nascimento" />
                    <asp:BoundField runat="server" DataField="NumCPF" HeaderText="CPF" />
                    <asp:BoundField runat="server" DataField="DesEmail" HeaderText="Email" />
                    <asp:CommandField ShowEditButton="true" />  
                    <asp:CommandField ShowDeleteButton="true" />
                </Columns>
            </asp:GridView>
                </div>  
            <div>  
                <asp:Label ID="lblresult" runat="server"></asp:Label>  
            </div>  
    </div>
  </div>
</asp:Content>
