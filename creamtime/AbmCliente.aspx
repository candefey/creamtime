<%@ Page Title="Registrarse" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AbmCliente.aspx.cs" Inherits="creamtime.AbmCliente" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

        <br/>
        <strong>
        <asp:Label ID="lbl_success" class="alert alert-success" runat="server" Text=""></asp:Label>
        <asp:Label ID="lbl_error" class="alert alert-danger" runat="server" Text=""></asp:Label>
        <asp:Label ID="lbl_warning" class="alert alert-warning" runat="server" Text=""></asp:Label>
        </strong>

    <h3>Registrate para hacer un pedido</h3>

     <div class="form-group">
        <asp:TextBox placeholder="Nombre" class="form-control" ID="txt_cliente_nombre" runat="server"></asp:TextBox>
     </div>

     <div class="form-group">
        <asp:TextBox placeholder="Apellido" class="form-control" ID="txt_cliente_apellido" runat="server"></asp:TextBox>
     </div>

     <div class="form-group">
        <asp:TextBox placeholder="Numero de Documento" type="number" class="form-control" ID="txt_cliente_dni" runat="server"></asp:TextBox>
     </div>

     <div class="form-group">
        <asp:TextBox placeholder="Seleccione Fecha de Nacimiento" class="form-control" ID="txt_cliente_fecha_nac" runat="server"></asp:TextBox>
     </div>

    <div class="form-group">
        <label>Sexo</label>
        <div class="dropdown">
        <asp:DropDownList  class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" ID="combo_cliente_sexo" runat="server">
        </asp:DropDownList>
            </div>
    </div>

    <div class="form-group">
        <asp:TextBox placeholder="Telefono Fijo o Celular" class="form-control" ID="txt_cliente_telefono" runat="server"></asp:TextBox>
        <small id="telHelp" class="form-text text-muted">Formato valido de telefono: 0351-154545454 / 0351-4601111</small>
    </div>

    <div class="form-group">
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
        <asp:TextBox placeholder="Calle" class="form-control" ID="txt_cliente_domicilio" runat="server"></asp:TextBox>
        <small id="calleHelp" class="form-text text-muted">Chacabuco, Av.Colon, Independencia</small>
    </div>

    <div class="form-group">
        <asp:TextBox placeholder="Numero o Departamento" class="form-control" ID="txt_cliente_numero" runat="server"></asp:TextBox>
         <small id="numeroHelp" class="form-text text-muted">1212, Depto:8 'A'</small>
    </div>

    <h4>Defina un usuario, para acceder a nuestra pagina</h4>

     <div class="form-group">
        <asp:TextBox placeholder="Usuario" class="form-control" ID="txt_usuario" runat="server"></asp:TextBox>
    </div>

    <div class="form-group">
        <asp:TextBox placeholder="Contraseña" type="password" class="form-control" ID="txt_contrasenia" runat="server"></asp:TextBox>
    </div>

    <div class="form-group">
        <asp:TextBox placeholder="Repita Contraseña" type="password" class="form-control" ID="txt_contrasenia2" runat="server"></asp:TextBox>
    </div>
    
    <asp:Button ID="btn_cliente_registrar" class="btn btn-info" runat="server" Text="Registrarse" OnClick="btn_cliente_registrar_Click" />


</asp:Content>

