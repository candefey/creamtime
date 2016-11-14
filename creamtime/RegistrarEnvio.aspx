<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarEnvio.aspx.cs" Inherits="creamtime.RegistrarEnvio" %>
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
                                <h4>Registro de envios pendientes</h4>
                            </div>
                             <div class="panel-body table-responsive"">
                                <asp:GridView ID="envio_gridview" runat="server" CssClass="table table-striped table-bordered table-hover" Width="100%" OnRowCommand="envio_gridview_RowCommand">
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
                                <h4>Registro de envios realizados</h4>
                            </div>
                             <div class="panel-body table-responsive"">
                                <asp:GridView ID="envios_realizados_gridview" runat="server" CssClass="table table-striped table-bordered table-hover" Width="100%" OnRowCommand="envio_gridview_RowCommand">
                                </asp:GridView>
                             </div>
                     </div>
                    </div>
            </div>



    <asp:Label ID="lbl_envio" runat="server" Text="Label"></asp:Label>

     <div class="form-group">
        <label>Repartidor</label>
        <div class="dropdown">
        <asp:DropDownList  class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" ID="combo_repartidor" runat="server">
        </asp:DropDownList>
            </div>
    </div>
        <div class="form-group">
        <label>Demora</label>
        <div class="dropdown">
        <asp:DropDownList  class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" ID="combo_demora" runat="server">
        </asp:DropDownList>
            </div>
    </div>

      <asp:Button ID="boton_envio" class="btn btn-primary" runat="server" Text="Registrar Envio" OnClick="boton_envio_Click"/>

</asp:Content>
