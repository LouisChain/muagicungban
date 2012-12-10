<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<muagicungban.Entities.Item>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
    var timer = new Array();
    var currentUser = new Array();
    var currentPrice = new Array();
    var showCurrentPrice = new Array();
    var increasePrice = new Array();
    
    $(function () {
        // Proxy created on the fly          
        var player = $.connection.player;

        var timeout;

        hideDiv = function(div1) {
            var div = document.getElementById(div1);
            div.style.display = "none";
        }

        // Declare a function on the chat hub so the server can invoke it          
        player.client.showMessage = function (message, time) {
            //alert(message);
            clearTimeout(timeout);
            var msg = document.getElementById("messages");
            //alert(message);
            msg.style.display = "block";
            msg.innerHTML = message;
            timeout = setTimeout(function(){hideDiv("messages")}, time);
            //alert(message);
        };

        player.client.updateItem = function (id, price, showprice, time, user, bidTime) {
             timer[id].TotalSecond(Number(time));
             currentPrice[id].innerHTML = price;
             showCurrentPrice[id].innerHTML = showprice;
             currentUser[id].innerHTML = user;
        };


<% foreach (var m in Model)
    { %>
        $("#bid<%: m.ItemID %>").click(function () {
            // Call the chat method on the server
            // alert(Number(currentPrice[<%: m.ItemID %>].innerHTML)+ 1000 ); //[RUN]

            // player.server.send(Number(currentPrice[<%: m.ItemID %>].innerHTML));

            player.server.placebid(<%: m.ItemID %>, Number(currentPrice[<%: m.ItemID %>].innerHTML) + Number(increasePrice[<%: m.ItemID %>].value));

            // alert("OK");
            // player.server.PlaceBid(<%: m.ItemID %>, Number($('#current-price').val() + 1));

        });
<% } %>
       
        // Start the connection
        $.connection.hub.start();
    });
    </script>
    <div id="messages" style="display: none; z-index: 5; float: left; position: fixed;
        left: 30%; right: 20%; bottom: 40px; border-width: 3px; border-top-left-radius: 40px;
        border-top-right-radius: 40px; border-bottom-right-radius: 40px; border-bottom-left-radius: 40px;
        groove rgb(9, 231, 222); font-size: 19px; background-color: rgb(27, 143, 138);
        color: rgb(255, 255, 255); border-color: #DD1F1F; width: 50%; height: 50px; text-align: center;
        padding-top: 5px; font-weight: bolder;">
    </div>

    <% if (Model.Any())
       { %>
    <div id="content">
        <div class="margin">
            <div class="content-helper clear">
                <div class="central-column">
                    <div class="central-content">
                        <% var top1 = Model.First();
                           Html.RenderPartial("Item_Top1", top1); %>
                        <div class="pagination-container" id="pagination_contents">
                            <div class="cm-pagination-wraper">
                                <a class="hidden" rev="pagination_contents" rel="" href="" name="pagination"></a>
                            </div>
                            <div id="list_product">
                                <% int i = 0; %>
                                <% foreach (var m in Model.Skip(1))
                                   {
                                       ++i;
                                       if (i % 3 == 1)
                                       { %>
                                <div class="productbox-container first" style="float: left">
                                    <%
        }
                                       else if (i % 3 == 2)
                                       { %>
                                    <div class="productbox-container " style="float: left">
                                        <%
        }
                                       else
                                       {%>
                                        <div class="productbox-container last" style="float: left">
                                            <% } %>
                                            <% Html.RenderPartial("ItemSummary", m); %>
                                        </div>
                                        <% } %>
                                    </div>
                                </div>
                                <%--<div class="mainbox-container">
                        <div class="mainbox-body"></div>
                    </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
<% } %>
</asp:Content>
