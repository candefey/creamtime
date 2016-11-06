<%@ Page Title="Registrar Pedido" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarPedido.aspx.cs" Inherits="creamtime.RegistrarPedido" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent"  ClientIDMode="Static" runat="server">


<link href="Scripts/jquery-ui-1.12/jquery-ui-datepicker.css" rel="stylesheet" />  
<script src="Scripts/jquery-ui-1.12/jquery-ui.js"></script> 



    <div class="row" style="display:block;">
    <div class="col-lg-4">
        <br/>
        <strong>
            <asp:Label ID="lbl_success" class="alert alert-success" runat="server" Text=""></asp:Label>
            <asp:Label ID="lbl_error" class="alert alert-danger" runat="server" Text=""></asp:Label>
            <asp:Label ID="lbl_warning" class="alert alert-warning" runat="server" Text=""></asp:Label>
        </strong>
    </div>
    </div>


    
