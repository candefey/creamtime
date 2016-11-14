<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InformeClientes.aspx.cs" Inherits="creamtime.InformeClientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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


    <div class="form-group">
        <label>Localidad</label>
        <div class="dropdown">
        <asp:DropDownList class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" ID="combo_localidad" runat="server" AutoPostBack="True" OnSelectedIndexChanged="combo_localidad_SelectedIndexChanged">
        </asp:DropDownList>
            </div>
     </div>

    <div class="form-group">
        <label>Barrio</label>
        <div class="dropdown">
        <asp:DropDownList class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" ID="combo_barrio" runat="server">
        </asp:DropDownList>
            </div>
     </div>

     <div class="form-group">
         <asp:CompareValidator ID="validator_compare_cliente_fecha" runat="server" ControlToValidate="txt_fecha_desde" ErrorMessage="*El campo debe ser de tipo fecha" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
        <asp:TextBox placeholder="Fecha Nacimiento Desde" class="form-control" ID="txt_fecha_desde" runat="server"></asp:TextBox>
    </div>
     <div class="form-group">    
         <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txt_fecha_hasta" ErrorMessage="*El campo debe ser de tipo fecha" Display="Dynamic" ForeColor="#CC3300" SetFocusOnError="True" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
        <asp:TextBox placeholder="Fecha Nacimiento Hasta" class="form-control" ID="txt_fecha_hasta" runat="server"></asp:TextBox>
    </div>

    <div class="form-group">
        <label>Sexo</label>
        <div class="dropdown">
        <asp:DropDownList class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" ID="combo_sexo" runat="server">
        </asp:DropDownList>
            </div>
     </div>



             <div class="col-lg-12">
                <div class="panel-boy">
                     <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4>Informe de Clientes</h4>
                            </div>
                             <div class="panel-body table-responsive"">
                                <asp:GridView ID="info_clientes_gridview" runat="server" CssClass="table table-striped table-bordered table-hover" Width="100%">
                                </asp:GridView>
                             </div>
                     </div>
                    </div>
            </div>


            <asp:Button ID="boton_informe" class="btn btn-primary" runat="server" Text="Generar Informe" OnClick="boton_informe_Click"/>


</asp:Content>
