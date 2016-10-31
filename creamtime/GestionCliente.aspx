<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionCliente.aspx.cs" Inherits="creamtime.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

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

        $('#MainContent_txt_cliente_fecha_nacim').datepicker(
   {
       dateFormat: 'dd/mm/yy',
       changeMonth: true,
       changeYear: true,
       yearRange: '1925:2100',
   });


    }); 
</script>

    <div class="panel-body">
  
    <div class="col-lg-8">
     <div class="panel panel-default">
        <div class="panel-heading">
            <h4>Listado de Clientes</h4>
            </div>
         <div class="panel-body table-responsive"">
    <asp:GridView ID="cliente_gridview" runat="server" CssClass="table table-striped table-bordered table-hover" Width="100%" OnRowCommand="cliente_gridview_RowCommand" OnRowDeleting="cliente_gridview_RowDeleting">
        <Columns>
            <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" />
            <asp:CommandField DeleteText="Eliminar" ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
             </div>
         </div>
        </div>

        <strong>
      <asp:Label ID="lbl_warning" class="alert alert-warning" runat="server" Text=""></asp:Label>
        <asp:Label ID="lbl_success" class="alert alert-success" runat="server" Text=""></asp:Label>
    <asp:Label ID="lbl_error" class="alert alert-danger" runat="server" Text=""></asp:Label>
            </strong>

        <div class="row" style="display:block;">
     <div class="col-lg-8">
    <div class="panel panel-default">
       <div class="panel-heading">

    <h4>Actualizacion de datos del cliente: <asp:Label ID="lbl_dni" runat="server" Text=""></asp:Label></h4>
    

    <div class="form-group">
         <asp:RequiredFieldValidator ID="validator_cliente_nombre" runat="server" ControlToValidate="txt_cliente_nombre" ErrorMessage="*Este campo es obligatorio" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:TextBox placeholder="Nombre" class="form-control" ID="txt_cliente_nombre" runat="server"></asp:TextBox>
     </div>

     <div class="form-group">
        <asp:RequiredFieldValidator ID="validator_cliente_apellido" runat="server" ControlToValidate="txt_cliente_apellido" ErrorMessage="*Este campo es obligatorio" Display="Dynamic" ForeColor="#CC3300"  SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:TextBox placeholder="Apellido" class="form-control" ID="txt_cliente_apellido" runat="server"></asp:TextBox>
     </div>

     <div class="form-group">
         <asp:RequiredFieldValidator ID="validator_cliente_fecha" runat="server" ControlToValidate="txt_cliente_fecha_nacim" ErrorMessage="*Este campo es obligatorio" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True"></asp:RequiredFieldValidator>
         <asp:CompareValidator ID="validator_compare_cliente_fecha" runat="server" ControlToValidate="txt_cliente_fecha_nacim" ErrorMessage="*El campo debe ser de tipo fecha" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
        <asp:TextBox placeholder="Seleccione Fecha de Nacimiento" class="form-control" ID="txt_cliente_fecha_nacim" runat="server"></asp:TextBox>
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

    <h4>Actualizacion de nombre de usuario</h4>

     <div class="form-group">
         <asp:RequiredFieldValidator ID="validator_usuario" runat="server" ControlToValidate="txt_usuario" ErrorMessage="*Este campo es obligatorio" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:TextBox placeholder="Usuario" class="form-control" ID="txt_usuario" runat="server"></asp:TextBox>
    </div>

    <asp:Button ID="boton_actualizar" class="btn btn-primary" runat="server" Text="Actualizar" OnClick="boton_actualizar_Click" />

        </div>
           <div class="panel-footer">
     </div>
        </div>
         </div>
            </div>

        </div>

</asp:Content>
