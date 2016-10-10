<%@ Page Title="Registro Proveedor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AbmProveedor.aspx.cs" Inherits="creamtime.AbmProveedor" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="/Scripts/Proveedor/abmProveedor.js"" type="text/javascript"></script>
    <br />
    <strong>
        <asp:Label ID="lbl_success" class="alert alert-success" runat="server" Text=""></asp:Label>
        <asp:Label ID="lbl_error" class="alert alert-danger" runat="server" Text=""></asp:Label>
        <asp:Label ID="lbl_warning" class="alert alert-warning" runat="server" Text=""></asp:Label>
    </strong>

    <h3>Registro de Proveedor</h3>
    <div class="form-group">
        <asp:TextBox placeholder="Razón Social"  required="true" class="form-control" ID="txt_razon" runat="server"></asp:TextBox>
    </div>

    <div class="form-group">
        <asp:TextBox placeholder="Cuit" class="form-control" required="true" ID="txt_cuit" runat="server"></asp:TextBox>
    </div>

    <div class="form-group">
        <label id="lbl_fecha_alta_text">Fecha de Alta: </label>
        <label id="lbl_fecha_alta"></label>
    </div>

    <div class="form-group">
        <asp:TextBox placeholder="Telefono Fijo o Celular" required="true" class="form-control" ID="txt_proveedor_telefono" runat="server"></asp:TextBox>
        <small id="telHelp" class="form-text text-muted">Formato valido de telefono: 0351-154545454 / 0351-4601111</small>
    </div>
    <div class="form-group">
        <asp:TextBox placeholder="Email" type="email" class="form-control" ID="txt_proveedor_email" runat="server"></asp:TextBox>
        <small id="emailHelp" class="form-text text-muted">Formato valido de email: ejemplo@ejemplo.com</small>
    </div>

    <div class="form-group">
        <label>Localidad</label>
        <div class="dropdown">
            <asp:DropDownList class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" ID="combo_proveedor_localidad" runat="server" AutoPostBack="True" OnSelectedIndexChanged="combo_proveedor_localidad_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
    </div>

    <div class="form-group">
        <label>Barrio</label>
        <div class="dropdown">
            <asp:DropDownList class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" ID="combo_proveedor_barrio" runat="server">
            </asp:DropDownList>
        </div>
    </div>

    <div class="form-group">
        <asp:TextBox placeholder="Calle" class="form-control" ID="txt_proveedor_domicilio" runat="server"></asp:TextBox>
        <small id="calleHelp" class="form-text text-muted">Chacabuco, Av.Colon, Independencia</small>
    </div>

    <div class="form-group">
        <asp:TextBox placeholder="Numero o Departamento" class="form-control" ID="txt_proveedor_numero" runat="server"></asp:TextBox>
        <small id="numeroHelp" class="form-text text-muted">1212, Depto:8 'A'</small>
    </div>

    <asp:Button ID="btn_proveedor_registrar" class="btn btn-info" runat="server" Text="Registrar" OnClick="btn_proveedor_registrar_Click" />

</asp:Content>
