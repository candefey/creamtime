<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="creamtime._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron" style="background-color=#249393;">
        <h1>CREAMTIME</h1>
        <p class="lead">Somos una empresa familiar que ofrece productos helados de la mejor calidad. Todos los productos son fabricados con ingredientes de primera marca logrando un exquisito sabor.</p>
        
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Nuestra Filosofía</h2>
            <p>
                Creamtime es una empresa que se enfoca en brindar al cliente una buena atención, ofreciendo calidad, variedad y facilidad de compra.
            </p>
            <p>
                <a class="btn btn-default" href="">Más Información &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Nuestros Productos</h2>
            <p>
                Ofrecemos los siguientes productos
            </p>
            <p>
                <a class="btn btn-default" href="">Más Detalles &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Nuestra Meta </h2>
            <p>
                Llevar nuestros productos a todo el país.
            </p>
            <p>
                <a class="btn btn-default" href="">Más Detalles &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
