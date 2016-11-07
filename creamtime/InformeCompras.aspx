<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableViewState="true" AutoEventWireup="true" CodeBehind="InformeCompras.aspx.cs" Inherits="creamtime.InformeCompras" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
  $( function() {
    $( document ).tooltip();
  });
 
</script>
    <h4>Informe Compras Realizadas a Proveedores</h4>
    <div class="row" id="labels" style="display:block;">             
    <strong>
        <asp:Label ID="lbl_success_" class="alert alert-success" runat="server" Text=""></asp:Label>
        <asp:Label ID="lbl_error_" class="alert alert-danger" runat="server" Text=""></asp:Label>
        <asp:Label ID="lbl_warning_" class="alert alert-warning" runat="server" Text=""></asp:Label>
    </strong>
    </div> 
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4>Filtros</h4>
        </div>
        
         <div class="panel-body table-responsive"">
             <div class="form-group">
                <label>              
               Seleccione un filtro</label>
                <div class="dropdown">
                        <asp:DropDownList class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" ID="combo_filtros" runat="server" AutoPostBack="True" OnSelectedIndexChanged="combo_filtros_SelectedIndexChanged">
                            <asp:ListItem>Sin Filtro</asp:ListItem>
                            <asp:ListItem>Proveedor</asp:ListItem>
                            <asp:ListItem>Materia Prima</asp:ListItem>
                            <asp:ListItem>Rango Monto</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                 </div>
            <div class="form-group">
                <label>              
                Proveedor</label>
                    <div class="dropdown">
                        <asp:DropDownList class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" ID="combo_proveedores" runat="server" AutoPostBack="False">
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
             <asp:Label ID="Label1" runat="server" Text="Monto Total Compra"></asp:Label>   
            <div class="form-group">
                <asp:CompareValidator ID="validator_desde" ControlToValidate="txt_desde" Operator="DataTypeCheck" Type="Integer" ErrorMessage="*Tipo de dato inválido" runat="server" ForeColor="#CC3300" SetFocusOnError="True" Display="Dynamic"></asp:CompareValidator>
                <asp:TextBox min="0" placeholder="Desde" class="form-control" ID="txt_desde" title="Ingrese el monto desde. Debe ser un número entero." runat="server"></asp:TextBox>
            </div> 
             <div class="form-group">
                <asp:CompareValidator ID="validator_hasta" ControlToValidate="txt_hasta" Operator="DataTypeCheck" Type="Integer" ErrorMessage="*Tipo de dato inválido" runat="server" ForeColor="#CC3300" SetFocusOnError="True" Display="Dynamic"></asp:CompareValidator>
                <asp:TextBox  min="0" placeholder="Hasta" class="form-control" ID="txt_hasta" title="Ingrese el monto desde. Debe ser un número entero." runat="server"></asp:TextBox>
            </div>
             <asp:Button ID="btn_filtrar" class="btn btn-info" runat="server" Text="Ver Compras" OnClick="btn_filtrar_Click"/>              
         </div>
     </div>

    <div class="panel panel-default">
        <div class="panel-heading">
            <h4>Resultado</h4>
        </div>
         <div class="panel-body table-responsive"">
                <asp:GridView CssClass="table table-striped table-bordered table-hover" Width="100%" ID="grillaCompras" runat="server" AutoGenerateColumns="False" OnRowCommand="grillaCompras_RowCommand" >
                    <Columns>
                
                        <asp:BoundField DataField="Nro" HeaderText="Nro" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                        <asp:BoundField DataField="Monto" HeaderText="Monto Total" />
                        <asp:CommandField ShowSelectButton="True" />
                
                    </Columns>
                </asp:GridView>                  
         </div>
        <div class="panel-body table-responsive"">
                <asp:GridView CssClass="table table-striped table-bordered table-hover" Width="100%" ID="grillaDetalleCompras" runat="server" AutoGenerateColumns="False" >
                    <Columns>                
                        <asp:BoundField HeaderText="Materia Prima" DataField="NombreMp" />
                        <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
                        <asp:BoundField HeaderText="SubTotal" DataField="Monto" />
                    </Columns>
                </asp:GridView>                  
         </div>
     </div>
</asp:Content>
