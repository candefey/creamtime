<%@ Page Title="Informe Pedidos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InformePedidos.aspx.cs" Inherits="creamtime.InformePedidos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<link href="Scripts/jquery-ui-1.12/jquery-ui-datepicker.css" rel="stylesheet" />  

<script src="Scripts/jquery-ui-1.12/jquery-ui.js"></script> 

<script>  
    $(document).ready(function () {


        $('#MainContent_txt_fecha_desde').datepicker(
  {
      dateFormat: 'dd/mm/yy',
      changeMonth: true,
      changeYear: true,
      yearRange: '1925:2100',
  });

        $('#MainContent_txt_fecha_hasta').datepicker(
{
    dateFormat: 'dd/mm/yy',
    changeMonth: true,
    changeYear: true,
    yearRange: '1925:2100',
});


    });
</script> 

    <div class="row" style="display:block;">
     <div class="col-lg-4">
      <div class="panel panel-default">
       <div class="panel-heading">
         <h4>Informe de Pedidos</h4>
       </div>
       <div class="panel-body">
        <div class="form-group">

            <div class="form-group">
                 <asp:CompareValidator ID="validator_compare_fecha_desde" runat="server" ControlToValidate="txt_fecha_desde" ErrorMessage="*El campo debe ser de tipo fecha" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                <asp:TextBox placeholder="Fecha Desde" class="form-control" ID="txt_fecha_desde" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">    
                <asp:CompareValidator ID="validator_compare_fecha_hasta" runat="server" ControlToValidate="txt_fecha_hasta" ErrorMessage="*El campo debe ser de tipo fecha" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                <asp:TextBox placeholder="Fecha Hasta" class="form-control" ID="txt_fecha_hasta" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:TextBox placeholder="Apellido Cliente" class="form-control" ID="txt_apellido_cliente" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Estado</label>
                <div class="dropdown">
                    <asp:DropDownList class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" ID="combo_estado" runat="server">
                    </asp:DropDownList>
                </div>
            </div>

            <asp:Button ID="btn_filtrar" class="btn btn-primary" runat="server" Text="Filtrar" OnClick="btn_filtrar_Click" Visible="true"/>
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
                <asp:GridView ID="grillaPedidos" CssClass="table table-striped table-bordered table-hover" Width="100%" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="Nro_Pedido" HeaderStyle-HorizontalAlign="Center" HeaderText="Numero" />
                        <asp:BoundField DataField="Fecha_Pedido" DataFormatString="{0:d}" HeaderStyle-HorizontalAlign="Center" HeaderText="Fecha" />
                        <asp:BoundField DataField="Monto" ItemStyle-HorizontalAlign="Center" DataFormatString="${0:###,###,###.00}" HeaderStyle-HorizontalAlign="Center" HeaderText="Monto Total" />                        
                        <asp:BoundField DataField="Estado.Nombre" HeaderStyle-HorizontalAlign="Center" HeaderText="Estado" />                     
                        <asp:BoundField DataField="Cliente.Apellido" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="Apellido del Cliente" />
                        <asp:BoundField DataField="Cliente.Nombre" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="Nombre del Cliente" />                        
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    </div>
</asp:Content>
