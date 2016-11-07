<%@ Page Title="Registrar Pedido" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarPedido.aspx.cs" Inherits="creamtime.RegistrarPedido" %>

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
</script>

    
    <div class="row" style="display:block;">
    <div class="col-lg-10">
        <br/>
        <strong>
            <asp:Label ID="lbl_success" class="alert alert-success" runat="server" Text=""></asp:Label>
            <asp:Label ID="lbl_error" class="alert alert-danger" runat="server" Text=""></asp:Label>
            <asp:Label ID="lbl_warning" class="alert alert-warning" runat="server" Text=""></asp:Label>
        </strong>
        <br />
    </div>
    </div>

    <div class="row" style="display:block;">
    <div class="col-lg-4">
     <div class="panel panel-default">
      <div class="panel-heading">
       <h4>Registro Pedidos</h4>
      </div>
       <div class="panel-body">
        <div class="form-group">

            <div class="form-group">
                <label>Producto</label>
                <div class="dropdown">
                <asp:DropDownList class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" ID="combo_producto" runat="server" AutoPostBack="True" OnSelectedIndexChanged="combo_producto_SelectedIndexChanged">
                </asp:DropDownList>
                   </div>
            </div>

            <asp:PlaceHolder ID="contenedorSabores" runat="server" Visible="false">
              <div class="panel panel-default">
               <div class="panel-body">
                <div class="form-group">
                    <label id="lbl_sabores" runat="server" Visible="false">
                        Sabores
                    </label>

                </div>
                
                <div class="form-group">
                <div class="dropdown">
                <asp:DropDownList class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" ID="ddl_1" runat="server" AutoPostBack="True" Visible="false">
                </asp:DropDownList>
                   </div>
                </div>

                <div class="form-group">
                <div class="dropdown">
                <asp:DropDownList class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" ID="ddl_2" runat="server" AutoPostBack="True" Visible="false">
                </asp:DropDownList>
                   </div>
                </div>

                <div class="form-group">
                <div class="dropdown">
                <asp:DropDownList class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" ID="ddl_3" runat="server" AutoPostBack="True" Visible="false">
                </asp:DropDownList>
                   </div>
                </div>

                <div class="form-group">
                <div class="dropdown">
                <asp:DropDownList class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" ID="ddl_4" runat="server" AutoPostBack="True" Visible="false">
                </asp:DropDownList>
                   </div>
                </div>

                <asp:Button ID="btn_agregar" class="btn btn-primary" Text="Agregar" runat="server" OnClick="btn_agregarDetalle" />
               </div>
              </div>
            </asp:PlaceHolder>
                
            <div class="form-group">
                <label>Envio a Domicilio</label>
                <asp:CheckBox ID="chk_envio" runat="server" />
                <small id="envioHelp" class="form-text text-muted">Seleccione para solicitar delivery a domicilio.</small>
            </div>

            <asp:Button ID="btn_producto_registrar" class="btn btn-primary" runat="server" Text="Registrar Pedido" OnClick="btn_registrar_pedido_Click" />
            
        </div>
       </div>
     </div>
    </div>
    

    <div class="col-lg-8"> 
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4>Carrito de Compras</h4>
            </div>
            <div class="panel-body table-responsive"">
                <asp:GridView ID="grillaProductos" CssClass="table table-striped table-bordered table-hover" Width="100%" runat="server" AutoGenerateColumns="False" OnRowDeleting="btn_eliminar_Click">
                    <Columns>
                        <asp:BoundField DataField="Producto.Nombre" HeaderText="Producto" />
                        <asp:BoundField DataField="Precio" HeaderText="Precio" />

                        <asp:CommandField DeleteText="Eliminar" InsertVisible="False" ShowCancelButton="False" ShowDeleteButton="True" />
                     
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
   </div>
</asp:Content>


    
