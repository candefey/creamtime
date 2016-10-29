<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionCliente.aspx.cs" Inherits="creamtime.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="cliente_gridview" runat="server" OnSelectedIndexChanged="cliente_gridview_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
</asp:Content>
