<%@ Page Title="Registrar Producto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AbmProducto.aspx.cs" Inherits="creamtime.AbmProducto" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent"  ClientIDMode="Static" runat="server">


<link href="Scripts/jquery-ui-1.12/jquery-ui-datepicker.css" rel="stylesheet" />  
<script src="Scripts/jquery-ui-1.12/jquery-ui.js"></script> 
<script>  
$(function ()  
{  
    $('#txt_fecha_alta').datepicker(
    {  
        dateFormat: 'dd/mm/yy',  
        changeMonth: true,  
        changeYear: true,  
        yearRange: '1925:2100',
    });  
})  
</script> 
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

   <div class="row" style="display:block;">
    <div class="col-lg-4">
     <div class="panel panel-default">
      <div class="panel-heading">
       <h4>Registro de Productos</h4>
      </div>
       <div class="panel-body">
        <div class="form-group">

            <div class="form-group">
                 <asp:RequiredFieldValidator ID="validator_producto_nombre" runat="server" ControlToValidate="txt_producto_nombre" ErrorMessage="*Este campo es obligatorio" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:TextBox placeholder="Nombre" class="form-control" ID="txt_producto_nombre" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Tipo de Producto</label>
                <div class="dropdown">
                <asp:DropDownList class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" ID="combo_tipo_producto" runat="server">
                </asp:DropDownList>
                    </div>
            </div>

            <div class="form-group">
                <asp:RequiredFieldValidator ID="validator_codigo_producto" runat="server" ControlToValidate="txt_codigo_producto" ErrorMessage="*Este campo es obligatorio" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="validator_regex_codigo_producto" runat="server"  ControlToValidate="txt_codigo_producto" ErrorMessage="*Formato invalido" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True" ValidationExpression="^([0-9]{8})$"></asp:RegularExpressionValidator>
                <asp:TextBox placeholder="Codigo del Producto" class="form-control" ID="txt_codigo_producto" runat="server"></asp:TextBox>
                <small id="codigoProdHelp" class="form-text text-muted">Formato valido de codigo de producto, 12345678</small>
            </div>

            <div class="form-group">
                <asp:RequiredFieldValidator ID="validator_precio" runat="server" ControlToValidate="txt_precio" ErrorMessage="*Este campo es obligatorio" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="validator_regex_precio" runat="server"  ControlToValidate="txt_precio" ErrorMessage="*Formato invalido" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True" ValidationExpression="\d+(?:.\d{1,2})?"></asp:RegularExpressionValidator>
                <asp:TextBox placeholder="Precio" class="form-control" ID="txt_precio" runat="server"></asp:TextBox>
                <small id="precioHelp" class="form-text text-muted">Formato valido de precio del producto es con ","(coma) como delimitador. Ej:100,00</small>
            </div>

            <div class="form-group">
                <asp:RequiredFieldValidator ID="validator_fecha_alta" runat="server" ControlToValidate="txt_fecha_alta" ErrorMessage="*Este campo es obligatorio" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="validator_compare_fecha_alta" runat="server" ControlToValidate="txt_fecha_alta" ErrorMessage="*El campo debe ser de tipo fecha" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                <asp:TextBox placeholder="Seleccione Fecha de Vigencia" class="form-control" ID="txt_fecha_alta" runat="server"></asp:TextBox>          
             </div>

            <div class="form-group">
                <label>Producto Vigente</label>
                <asp:CheckBox ID="chk_vigente" runat="server" />
                <small id="vigenteHelp" class="form-text text-muted">Seleccione para indicar que el producto se encuentra vigente</small>
            </div>

            <asp:Button ID="btn_producto_guardar" class="btn btn-primary" runat="server" Text="Guardar" OnClick="btn_producto_guardar_Click" Visible="false"/>
            <asp:Button ID="btn_producto_registrar" class="btn btn-primary" runat="server" Text="Registrar" OnClick="btn_producto_registrar_Click" />
    
        </div>
       </div>
      </div>
    </div>

    <div class="col-lg-8"> 
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4>Listado de Productos</h4>
            </div>
            <div class="panel-body table-responsive"">
                <asp:GridView ID="grillaProductos" CssClass="table table-striped table-bordered table-hover" Width="100%" runat="server" AutoGenerateColumns="False" OnRowDeleting="btn_eliminar_Click" OnRowCommand="grillaProductos_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="Nombre" HeaderText="Producto" />
                        <asp:BoundField DataField="NombreTipo" HeaderText="Tipo de Producto" />
                        <asp:BoundField DataField="Precio" HeaderText="Precio" />                        
                        <asp:BoundField DataField="Vigente" HeaderText="Vigente" />
                     
                        <asp:CommandField InsertVisible="False" ShowCancelButton="False" SelectText="Editar" ShowSelectButton="True" />
                        <asp:CommandField DeleteText="Eliminar" InsertVisible="False" ShowCancelButton="False" ShowDeleteButton="True" />
                     
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
   </div>

</asp:Content>