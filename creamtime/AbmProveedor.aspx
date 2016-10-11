<%@ Page Title="Registro Proveedor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AbmProveedor.aspx.cs" Inherits="creamtime.AbmProveedor" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="/Scripts/Proveedor/abmProveedor.js"" type="text/javascript"></script>
    <br />
    <strong>
        <asp:Label ID="lbl_success" class="alert alert-success" runat="server" Text=""></asp:Label>
        <asp:Label ID="lbl_error" class="alert alert-danger" runat="server" Text=""></asp:Label>
        <asp:Label ID="lbl_warning" class="alert alert-warning" runat="server" Text=""></asp:Label>
    </strong>

  <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
   <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
  <script>
  $( function() {
    $( document ).tooltip();
  } );
  </script>
  <style>
  title {
    display: inline-block;
    width: 5em;
  }
  </style>

    <h3>Registro de Proveedor</h3>
    <div class="form-group">
         <asp:RequiredFieldValidator ID="validator_razon_social" runat="server" ControlToValidate="txt_razon" ErrorMessage="*Este campo es obligatorio" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:TextBox placeholder="Razón Social" class="form-control" ID="txt_razon" runat="server"></asp:TextBox>
    </div>
     <div class="form-group">
        <label id="lbl_fecha_alta_text">Fecha de Alta: </label>
        <label id="lbl_fecha_alta"></label>
    </div>

    <div class="form-group">
        <asp:RequiredFieldValidator ID="validator_cuit" runat="server" ControlToValidate="txt_cuit" ErrorMessage="*Este campo es obligatorio" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="validator_cuit_compare" ControlToValidate="txt_cuit" Operator="DataTypeCheck" Type="Integer" ErrorMessage="*Tipo de dato inválido" runat="server" ForeColor="#CC3300" SetFocusOnError="True" Display="Dynamic"></asp:CompareValidator>
        <asp:TextBox placeholder="Cuit" class="form-control" ID="txt_cuit" title="Formato válido de cuit: 27356478993" runat="server"></asp:TextBox>        
    </div>
   

    <div class="form-group">
        <asp:RequiredFieldValidator ID="validator_proveedor_telefono" runat="server" ControlToValidate="txt_proveedor_telefono" ErrorMessage="*Este campo es obligatorio" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="validator_regex_proveedor_telefono" runat="server"  ControlToValidate="txt_proveedor_telefono" ErrorMessage="*Formato invalido." Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True" ValidationExpression="\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4}){0,50}"></asp:RegularExpressionValidator>
        <asp:TextBox placeholder="Telefono Fijo o Celular" class="form-control" ID="txt_proveedor_telefono" tittle="Formato valido de telefono: 0351-154545454 / 0351-4601111" runat="server"></asp:TextBox>
    </div>
    <div class="form-group">
        <asp:RequiredFieldValidator ID="validator_cliente_email" runat="server" ControlToValidate="txt_proveedor_email" ErrorMessage="*Este campo es obligatorio" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="validator_regex_proveedor_email" runat="server" ControlToValidate="txt_proveedor_email" ErrorMessage="*Formato invalido" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        <asp:TextBox placeholder="Email" type="email" class="form-control" ID="txt_proveedor_email" title="Formato valido de email: ejemplo@ejemplo.com" runat="server"></asp:TextBox>
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
