<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="MediaCollector.About" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        (function ($) {
            $.fn.sorted = function (customOptions) {
                var options = {
                    reversed: false,
                    by: function (a) { return a.text(); }
                };
                $.extend(options, customOptions);
                $data = $(this);
                arr = $data.get();
                arr.sort(function (a, b) {
                    var valA = options.by($(a));
                    var valB = options.by($(b));
                    if (options.reversed) {
                        return (valA < valB) ? 1 : (valA > valB) ? -1 : 0;
                    } else {
                        return (valA < valB) ? -1 : (valA > valB) ? 1 : 0;
                    }
                });
                return $(arr);
            };
        })(jQuery);

       $(document).ready(function () { 
           
           // get the first collection
           var $articles = $("#articles");

           // clone applications to get a second collection
           var $data = $articles.clone();

           var $filteredData = $data.find('li');

           // attempt to call Quicksand on every onclick
           $(".filter").click(function (e) {
               var filterOption = $(this).attr("data-value");

               if (filterOption == 'all') {
                   var $filteredData = $data.find('li');
                } else {
                   var $filteredData = $data.find('li[data-id=' + filterOption + ']');
               }

               /*var $sortedData = $filteredData.sorted({
                   by: function (v) {
                       return $(v).find('p').text().toLowerCase();
                   }
               });*/

               // finally, call quicksand
               $articles.quicksand($filteredData, {
                   duration: 800,
                   easing: 'swing'
               });
               return false;
           });
           
           //$articles.masonry({ itemSelector: '.box' });
        });
    </script>
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Your app description page.</h2>
    </hgroup>
    <asp:Repeater runat="server" ID="repeaterMediaTypes">
        <HeaderTemplate>
            <ul id="mediaTypes">
                    <li>
                        <a href="#" data-value="all" class="filter">All</a>
                    </li>
        </HeaderTemplate>
        <ItemTemplate>
            <li>
                <a href="#" data-value="<%# Eval("MediaTypeId") %>" class="filter"><%# Eval("MediaType1") %></a>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Repeater runat="server" ID="repeaterMedia">
        <HeaderTemplate>
            <ul id="articles">
        </HeaderTemplate>
        <ItemTemplate>
            <li class="box" data-id="<%# Eval("fkMediaTypeId") %>">
                <p>        
                    <%# Eval("Title") %>
                </p>
                <img src="<%# Eval("Image") %>" />
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>