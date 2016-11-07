<%@ Page Title="Nueva Compra" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarCompraMP.aspx.cs" Inherits="creamtime.RegistrarCompraMP" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
     <div class="panel-body">
     <div class="row" id="labels" style="display:block;">             
    <strong>
        <asp:Label ID="lbl_success_" class="alert alert-success" runat="server" Text=""></asp:Label>
        <asp:Label ID="lbl_error_" class="alert alert-danger" runat="server" Text=""></asp:Label>
        <asp:Label ID="lbl_warning_" class="alert alert-warning" runat="server" Text=""></asp:Label>
    </strong>
    </div> 

    <div class="row" style="display:block;">

    <div class="col-lg-4">
    <div class="panel panel-default">
       <div class="panel-heading">
        <h3>Compra de Materias Primas</h3>
    </div>
     <div class="panel-body">
    
  <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
   <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
         <script>

             $(document).ready(function () {
                 today = new Date();
                 dd = today.getDate();
                 mm = today.getMonth() + 1;
                 yyyy = today.getFullYear();
                 if (dd < 10) {
                     dd = '0' + dd
                 }
                 if (mm < 10) {
                     mm = '0' + mm
                 }
                 var today = dd + '/' + mm + '/' + yyyy;
                 document.getElementById('fecha_hoy').innerHTML = today;  });
         </script>
  <script>
  $( function() {
    $( document ).tooltip();
  });
  $(document).ready(function () {

      setTimeout(function () {
          $(".alert").fadeIn();
          $(".alert").fadeTo(250, 0).slideUp(250, function () {
              $(this).remove();
          });
      }, 2500);
  });
</script>
  <style>
  title {
    display: inline-block;
    width: 5em;
  }
  </style>
         <asp:Label ID="Label1" runat="server" Text="Fecha Compra"></asp:Label>    
         <asp:Label ID="fecha_hoy" runat="server" Text=""></asp:Label>
    <h4>Detalle de la Compra </h4>
    <div class="form-group">
        <label>Proveedor</label>
        <div class="dropdown">
            <asp:DropDownList class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" ID="combo_proveedores" runat="server" AutoPostBack="True" OnSelectedIndexChanged="combo_proveedores_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
    </div>


    <div class="form-group">
        <label>Materia Prima</label>
        <div class="dropdown">
            <asp:DropDownList class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" ID="combo_mp" runat="server">
            </asp:DropDownList>
         </div>
   </div>
   <div class="form-group">
        <asp:RequiredFieldValidator ID="validator_cantidad" runat="server" ControlToValidate="txt_cantidad" ErrorMessage="*Este campo es obligatorio" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="validator_razon_compare" ControlToValidate="txt_cantidad" Operator="DataTypeCheck" Type="Integer" ErrorMessage="*Tipo de dato inválido" runat="server" ForeColor="#CC3300" SetFocusOnError="True" Display="Dynamic"></asp:CompareValidator>
        <asp:TextBox placeholder="Cantidad" class="form-control" ID="txt_cantidad" title="Ingrese la cantidad en unidades " runat="server"></asp:TextBox>
    </div>
         <asp:Button ID="btn_agregar" class="btn btn-info" runat="server" Text="Agregar" OnClick="btn_agregar_Click"/>
    </div>
     <div class="panel-footer">
     </div>
 </div>
 </div>
    <div class="col-lg-8">
     <div class="panel panel-default">
        <div class="panel-heading">
        <h4>Detalle de Compra</h4>
        </div>
         <div class="panel-body table-responsive"">
                <asp:GridView CssClass="table table-striped table-bordered table-hover" Width="100%" ID="grillaDetalles" runat="server">
                    <Columns>
                        
                    </Columns>
                </asp:GridView>
                  
         </div>
         <div>
         <asp:Button ID="btn_confirmar" class="btn btn-info" runat="server" Text="Confirmar" OnClick="btn_confirmar_Click"/>
         </div>
     </div>
    </div>
 </div>
         </div>

</asp:Content>
