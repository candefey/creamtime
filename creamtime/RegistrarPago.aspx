<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarPago.aspx.cs" Inherits="creamtime.RegistrarPago" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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

           <br/>
        <strong>
        <asp:Label ID="lbl_success" class="alert alert-success" runat="server" Text=""></asp:Label>
        <asp:Label ID="lbl_error" class="alert alert-danger" runat="server" Text=""></asp:Label>

        </strong>


           <div class="col-lg-12">
                <div class="panel-boy">
                     <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4>Registro de pagos pendientes</h4>
                            </div>
                             <div class="panel-body table-responsive"">
                                <asp:GridView ID="pago_gridview" runat="server" CssClass="table table-striped table-bordered table-hover" Width="100%" OnRowCommand="pago_gridview_RowCommand">
                                    <Columns>
                                        <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" />
                                    </Columns>
                                </asp:GridView>
                             </div>
                     </div>
                    </div>
            </div>
             <div class="col-lg-12">
                <div class="panel-boy">
                     <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4>Registro de pagos realizados</h4>
                            </div>
                             <div class="panel-body table-responsive"">
                                <asp:GridView ID="pagos_realizados_gridview" runat="server" CssClass="table table-striped table-bordered table-hover" Width="100%">
                                </asp:GridView>
                             </div>
                     </div>
                    </div>
            </div>



    <asp:Label ID="lbl_pedido" runat="server" Text="Label"></asp:Label>

     <div class="form-group">
        <label>Estado</label>
        <div class="dropdown">
        <asp:DropDownList  class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" ID="combo_estado" runat="server">
        </asp:DropDownList>
            </div>
    </div>


      <asp:Button ID="boton_pago" class="btn btn-primary" runat="server" Text="Registrar Pago" OnClick="boton_pago_Click"/>
</asp:Content>
