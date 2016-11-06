<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarEnvio.aspx.cs" Inherits="creamtime.RegistrarEnvio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


           <div class="col-lg-8">
                <div class="panel-boy">
                     <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4>Registro de envios</h4>
                            </div>
                             <div class="panel-body table-responsive"">
                                <asp:GridView ID="envio_gridview" runat="server" CssClass="table table-striped table-bordered table-hover" Width="100%">
                                </asp:GridView>
                             </div>
                     </div>
                    </div>
            </div>

</asp:Content>
