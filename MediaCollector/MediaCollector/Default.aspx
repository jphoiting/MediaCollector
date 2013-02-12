<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MediaCollector._Default" %>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".clearMe").click(function () {
                $(this).val("");
            });
        });
</script>
    <div>
        <h3>Search Amazon:</h3>
        <ol class="round">
            <li class="one">
                <h5>Input ISBN</h5>
                <asp:TextBox CssClass="clearMe" ID="textboxIdentifierInput" runat="server"></asp:TextBox>
                <asp:Button ID="buttonSearchAmazon" runat="server" Text="Search Amazon" OnClick="buttonSearchAmazon_Click" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="textboxIdentifierInput" Display="Dynamic" ErrorMessage="Please enter an Ean code"></asp:RequiredFieldValidator>
            </li>
        </ol>
    </div>
    <div style="float: right;">
        <h3><asp:Literal runat="server" ID="literalTitles"></asp:Literal></h3>
        <asp:Image ID="imageCover" runat="server" />
    </div>
</asp:Content>