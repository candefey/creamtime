<%@ Page Title="Registrarse" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AbmCliente.aspx.cs" Inherits="creamtime.AbmCliente" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent"  ClientIDMode="Static" runat="server">

<link href="Scripts/jquery-ui-1.12/jquery-ui-datepicker.css" rel="stylesheet" />  
<script src="Scripts/jquery-ui-1.12/jquery-ui.js"></script> 
<script>  
    $(document).ready(function () {

        setTimeout(function () {
            $(".alert").fadeIn();
            $(".alert").fadeTo(250, 0).slideUp(250, function () {
                $(this).remove();
            });
        }, 2500);
    }); 

        $('#txt_cliente_fecha_nac').datepicker(
  {
      dateFormat: 'dd/mm/yy',
      changeMonth: true,
      changeYear: true,
      yearRange: '1925:2100',
      maxDate: '-18Y',
  });

    }); 
</script>  
        <br/>
        <strong>
        <asp:Label ID="lbl_success" class="alert alert-success" runat="server" Text=""></asp:Label>
        <asp:Label ID="lbl_error" class="alert alert-danger" runat="server" Text=""></asp:Label>
        <asp:Label ID="lbl_warning" class="alert alert-warning" runat="server" Text=""></asp:Label>
        </strong>

    <h3>Registrate para hacer un pedido</h3>

     <div class="form-group">
         <asp:RequiredFieldValidator ID="validator_cliente_nombre" runat="server" ControlToValidate="txt_cliente_nombre" ErrorMessage="*Este campo es obligatorio" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:TextBox placeholder="Nombre" class="form-control" ID="txt_cliente_nombre" runat="server"></asp:TextBox>
     </div>

     <div class="form-group">
        <asp:RequiredFieldValidator ID="validator_cliente_apellido" runat="server" ControlToValidate="txt_cliente_apellido" ErrorMessage="*Este campo es obligatorio" Display="Dynamic" ForeColor="#CC3300"  SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:TextBox placeholder="Apellido" class="form-control" ID="txt_cliente_apellido" runat="server"></asp:TextBox>
     </div>

     <div class="form-group">
         <asp:RequiredFieldValidator ID="validator_cliente_dni" runat="server" ControlToValidate="txt_cliente_dni" ErrorMessage="*Este campo es obligatorio" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:TextBox placeholder="Numero de Documento" class="form-control" ID="txt_cliente_dni" runat="server"></asp:TextBox>
     </div>

     <div class="form-group">
         <asp:RequiredFieldValidator ID="validator_cliente_fecha" runat="server" ControlToValidate="txt_cliente_fecha_nac" ErrorMessage="*Este campo es obligatorio" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True"></asp:RequiredFieldValidator>
         <asp:CompareValidator ID="validator_compare_cliente_fecha" runat="server" ControlToValidate="txt_cliente_fecha_nac" ErrorMessage="*El campo debe ser de tipo fecha" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
        <asp:TextBox placeholder="Seleccione Fecha de Nacimiento" class="form-control" ID="txt_cliente_fecha_nac" runat="server"></asp:TextBox>
          <small id="fechaHelp" class="form-text text-muted">Debes ser mayor de 18 años para registrarte</small>
     </div>

    <div class="form-group">
        <label>Sexo</label>
        <div class="dropdown">
        <asp:DropDownList  class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" ID="combo_cliente_sexo" runat="server">
        </asp:DropDownList>
            </div>
    </div>

    <div class="form-group">
        <asp:RequiredFieldValidator ID="validator_cliente_telefono" runat="server" ControlToValidate="txt_cliente_telefono" ErrorMessage="*Este campo es obligatorio" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="validator_regex_cliente_telefono" runat="server"  ControlToValidate="txt_cliente_telefono" ErrorMessage="*Formato invalido" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True" ValidationExpression="\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})"></asp:RegularExpressionValidator>
        <asp:TextBox placeholder="Telefono Fijo o Celular" class="form-control" ID="txt_cliente_telefono" runat="server"></asp:TextBox>
        <small id="telHelp" class="form-text text-muted">Formato valido de telefono (considerar espacios), Cel: 351 454 5454 / Fijo: 351 460 1111</small>
    </div>

    <div class="form-group">
        <asp:RequiredFieldValidator ID="validator_cliente_email" runat="server" ControlToValidate="txt_cliente_email" ErrorMessage="*Este campo es obligatorio" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="validator_regex_cliente_email" runat="server" ControlToValidate="txt_cliente_email" ErrorMessage="*Formato invalido" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        <asp:TextBox placeholder="Email" type="email" class="form-control" ID="txt_cliente_email" runat="server"></asp:TextBox>
        <small id="emailHelp" class="form-text text-muted">Formato valido de email: ejemplo@ejemplo.com</small>
    </div>
     
    <div class="form-group">
        <label>Localidad</label>
        <div class="dropdown">
        <asp:DropDownList class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" ID="combo_cliente_localidad" runat="server" AutoPostBack="True" OnSelectedIndexChanged="combo_cliente_localidad_SelectedIndexChanged">
        </asp:DropDownList>
            </div>
     </div>

    <div class="form-group">
        <label>Barrio</label>
        <div class="dropdown">
        <asp:DropDownList class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" ID="combo_cliente_barrio" runat="server">
        </asp:DropDownList>
            </div>
     </div>

    <div class="form-group">
        <asp:RequiredFieldValidator ID="validator_cliente_domicilio" runat="server" ControlToValidate="txt_cliente_domicilio" ErrorMessage="*Este campo es obligatorio" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:TextBox placeholder="Calle" class="form-control" ID="txt_cliente_domicilio" runat="server"></asp:TextBox>
        <small id="calleHelp" class="form-text text-muted">Chacabuco, Av.Colon, Independencia</small>
    </div>

    <div class="form-group">
        <asp:RequiredFieldValidator ID="validator_cliente_numero" runat="server" ControlToValidate="txt_cliente_numero" ErrorMessage="*Este campo es obligatorio" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:TextBox placeholder="Numero o Departamento" class="form-control" ID="txt_cliente_numero" runat="server"></asp:TextBox>
         <small id="numeroHelp" class="form-text text-muted">1212, Depto:8 'A'</small>
    </div>

    <h4>Defina un usuario, para acceder a nuestra pagina</h4>

     <div class="form-group">
         <asp:RequiredFieldValidator ID="validator_usuario" runat="server" ControlToValidate="txt_usuario" ErrorMessage="*Este campo es obligatorio" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:TextBox placeholder="Usuario" class="form-control" ID="txt_usuario" runat="server"></asp:TextBox>
    </div>

    <div class="form-group">
        <asp:RequiredFieldValidator ID="validator_contrasenia" runat="server" ControlToValidate="txt_contrasenia" ErrorMessage="*Este campo es obligatorio" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:TextBox placeholder="Contraseña" type="password" class="form-control" ID="txt_contrasenia" runat="server"></asp:TextBox>
    </div>

    <div class="form-group">
        <asp:RequiredFieldValidator ID="validator_contrasenia2" runat="server" ControlToValidate="txt_contrasenia2" ErrorMessage="*Este campo es obligatorio" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:TextBox placeholder="Repita Contraseña" type="password" class="form-control" ID="txt_contrasenia2" runat="server"></asp:TextBox>
    </div>
    
    <asp:Button ID="btn_cliente_registrar" class="btn btn-primary" runat="server" Text="Registrarse" OnClick="btn_cliente_registrar_Click" />


</asp:Content>

