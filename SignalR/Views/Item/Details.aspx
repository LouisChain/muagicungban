<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<muagicungban.Entities.Item>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
    var bidsHistory = new Array();

    $(function () {
        // Proxy created on the fly          
        var player = $.connection.player;

        var timeout;

        hideDiv = function(div1) {
            var div = document.getElementById(div1);
            div.style.display = "none";
        };

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
             timer<%: Model.ItemID %>.TotalSecond(Number(time));
             currentPrice<%: Model.ItemID %>.innerHTML = price;
             showCurrentPrice<%: Model.ItemID %>.innerHTML = showprice;
             //currentUser<%: Model.ItemID %>.value = user;
             //alert(currentUser<%: Model.ItemID %>.value);
             //if ( bidsHistory[0] < 14 ) bidsHistory[0] += 1;
             for (var i = bidsHistory[0]; i > 1; i--)
             {
                bidsHistory[i].innerHTML = bidsHistory[i-1].innerHTML;
             };
             //alert(bidsHistory[0]);
             bidsHistory[1].innerHTML = "<td>" + user + "</td><td>" + showprice + "</td><td>" + bidTime + "</td>";
             //alert(bidsHistory[1].innerHTML);
        };
        <% if (Model.IsAuction) { %>
            $("#bid<%: Model.ItemID %>").click(function () {
                // Call the chat method on the server
                // alert("OK");
                //alert(Number(currentPrice<%: Model.ItemID %>.innerHTML));
                if (confirm("Chắc chứ!!!"))
                    player.server.placebid(<%: Model.ItemID %>, Number(currentPrice<%: Model.ItemID %>.innerHTML) + Number(increasePrice<%: Model.ItemID %>.value));
                //alert(<%: Model.ItemID %> + " " + Number(current-price<%: Model.ItemID %>.innerHTML) + " " + Number(increasePrice<%: Model.ItemID %>.value);
            });
        <% } else { %>
            $("#buy<%: Model.ItemID %>").click(function () {
                // Call the chat method on the server
                // alert(Number(currentPrice[<%: Model.ItemID %>].innerHTML)+ 1000 ); //[RUN]

                // player.server.send(Number(currentPrice[<%: Model.ItemID %>].innerHTML));
                if (confirm("Chắc chứ!"))
                    player.server.buy(<%: Model.ItemID %>);

                // alert("OK");
                // player.server.PlaceBid(<%: Model.ItemID %>, Number($('#current-price').val() + 1));

            });
       <% } %>
        // Start the connection
        $.connection.hub.start();
    });
    </script>
    <div id="messages" style="display: none; z-index: 5; float: left; position: fixed;
        left: 30%; right: 20%; bottom: 70px; border-width: 3px; border-top-left-radius: 40px;
        border-top-right-radius: 40px; border-bottom-right-radius: 40px; border-bottom-left-radius: 40px;
        groove rgb(9, 231, 222); font-size: 19px; background-color: rgb(27, 143, 138);
        color: rgb(255, 255, 255); border-color: #DD1F1F; width: 50%; height: 50px; text-align: center;
        padding-top: 5px; font-weight: bolder;">
    </div>
    <div class="helper-container">
        <div id="container" class="container-left">
            <div id="content">
                <div class="margin">
                    <div class="content-helper clear">
                        <div class="central-column">
                            <div>
                                <div class="mainbox-body">
                                    <% Html.RenderPartial("ItemMenu"); %>
                                    <div class="central-content">
                                        <div class="box_product_detail">
                                            <div class="box_product_detail_padding" style="">
                                                <div class="tt_product_detail">
                                                    <h1>
                                                        <%: Model.Description %>
                                                    </h1>
                                                </div>
                                                <div class="box_info_product_detail">
                                                    <div class="box_price_product_detail m10b">
                                                        <div class="bt_price_detail" id="showCurrentPrice<%: Model.ItemID %>">
                                                            <%: Model.CurPrice.ToString("#,### VND") %></div>
                                                        <div id="current-price<%: Model.ItemID %>" name="current-price<%: Model.ItemID %>"
                                                            style="display: none;">
                                                            <%: Model.CurPrice %></div>
                                                       
                                                        <script type="text/javascript"> 
                    currentPrice<%: Model.ItemID %> = document.getElementById("current-price<%: Model.ItemID %>"); 
                    showCurrentPrice<%: Model.ItemID %> = document.getElementById("showCurrentPrice<%: Model.ItemID %>");            
                                                        </script>
                                                        <div class="price_old">
                                                            <p class="text" style="float: right; position: absolute; top: 25px; padding-bottom: 15px;">
                                                                Tối đa:
                                                                <%: Model.MaxPrice.ToString("#,###") %>
                                                            </p>
                                                            <% if (Model.IsAuction)
                       { %>
                                                            <div style="float: right; position: relative; right: 10px; padding-bottom: 15px;
                                                                font-size: 16px; font-weight: bolder;">
                                                                +
                                                                <input type="text" name="increase-price<%: Model.ItemID %>" id="increase-price<%: Model.ItemID %>"
                                                                    style="width: 150px; height: 25px; font-weight: bolder; font-size: 18px;" value="1000" />
                                                            </div>
                                                            <script>
                                increasePrice<%: Model.ItemID %> = document.getElementById("increase-price<%: Model.ItemID %>");
                                                            </script>
                                                            <% } %>
                                                            <div class="cm-reload-12443" id="add_to_cart_update_12443">
                                                                <span id="cart_add_block_12443"><span id="wrap_button_cart_12443" class="button-submit-action">
                                                                    <% if (Model.IsAuction)
                            { %>
                                                                    <div class="button feature" id="bid<%: Model.ItemID %>" style="position: relative;
                                                                        bottom: 0px; left: 0px;">
                                                                        BID
                                                                        <% }
                            else
                            { %>
                                                                        <div class="button feature" id="buy<%: Model.ItemID %>" style="position: relative;
                                                                            bottom: 0px; left: 0px;">
                                                                            BUY
                                                                            <% } %>
                                                                        </div>
                                                                </span></span><span id="cart_buttons_block_12443"></span>
                                                                <!--add_to_cart_update_12443-->
                                                            </div>
                                                        </div>
                                                        <div class="box_deal">
                                                        </div>
                                                    </div>
                                                    <div class="box_price_product_detail">
                                                        <div class="box_times">
                                                            <p class="font13 p10b">
                                                                Thời gian còn lại</p>
                                                            <div class="remain_times hasCountdown" rel="1178251" id="timer<%: Model.ItemID %>">
                                                                <span>0</span>:<span>0</span>:<span>0</span>
                                                            </div>
                                                        </div>
                                                        <script type="text/javascript">
                    timer<%: Model.ItemID %> = new countDown1("<%: (Model.EndDate - DateTime.Now).TotalSeconds %>", "timer<%: Model.ItemID %>");
                                                        </script>
                                                        <div class="cb">
                                                        </div>
                                                    </div>
                                                    <div class="box_shadow">
                                                    </div>
                                                </div>
                                                <div style="float: left; width: 350px; padding-right: 10px; padding-left: 10px; padding-top: 10px;
                                                    border-radius: 20px; height: 330px; border-bottom-style: groove; border-bottom-width: 2px;
                                                    border-top-style: groove;overflow:auto">
                                                    <div style="text-align: center; height: 30px; font-size: 20px; font-weight: bolder;">
                                                        LỊCH SỬ ĐẤU GIÁ
                                                    </div>
                                                    <table cellspacing="0px" border="0px" width="100%" style="border-radius: 5px; font-size: 14px;">
                                                        <tbody>
                                                            <tr style="background-color: #AAF13F; border: 0px; font-size: 16px; font-weight: bolder;">

                                                                <td width="30%">
                                                                    Bidder
                                                                </td>
                                                                <td width="28%">
                                                                    Mức giá
                                                                </td>
                                                                <td width="42%">
                                                                    Thời gian
                                                                </td>
                                                            </tr>
                                                            <% int i = 0; %>
                                                            <% var bids = Model.Bids.OrderByDescending(b => b.Amount).Take(14).ToList(); %>
                                                            <% foreach (var _bid in bids)
                                                            {
                                                                ++i; %>
                                                            <tr <% if ( i % 2 == 0) { %>style="background-color: #E8F0DF;" <% } %> id="bHis<%: _bid.BidID %>">
                                                                    
                                                                <td>
                                                                    <%: _bid.BidderID %>
                                                                </td>
                                                                <td>
                                                                    <%: _bid.Amount.ToString("#,### VNĐ") %>
                                                                </td>
                                                                <td>
                                                                    <%: _bid.DatePlace.ToString("dd/MM/yyyy hh:mm") %>
                                                                </td>
                                                            </tr>
                                                            <% }
                                                                while (i < 14)
                                                                { %>
                                                                    <tr id="bHis<%: ++i %>"></tr>
                                                              <%}
                                                        i = 0; %>
                                                        </tbody>
                                                    </table>
                                                    <script type="text/javascript">
                                                    <% foreach (var _bid in bids)
                                                       { %>
                                                        bidsHistory[<%: ++i %>] = document.getElementById("bHis<%: _bid.BidID %>");
                                                    <% }  
                                                    while (i < 14)
                                                    { %>
                                                        bidsHistory[<%: ++i %>] = document.getElementById("bHis<%: i %>");
                                                    <%}%>
                                                    bidsHistory[0] = <%: i %>;
                                                    </script>
                                                </div>
                                                <div class="box_img_product_detail">
                                                    <div class="box_stand_star">
                                                        <a class="cr" href="">
                                                            <%: Model.Title %></a>
                                                        <div class="cb">
                                                        </div>
                                                    </div>
                                                    <div class="pic_product_detail">
                                                        <span class="pic_product_detail">
                                                            <div class="slider-wrapper theme-default">
                                                                <img id="det_img_1618606641" src='http://<%: Request.Url.Authority %>/item/getImage?ItemId=<%: Model.ItemID %>'
                                                                    width="300" alt="" border="0">
                                                            </div>
                                                        </span>
                                                    </div>
                                                </div>
                                                <div class="cb">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="box_product_detail box_product_feature" >
                                            <div class="box_product_feature_inner">
                                            </div>
                                            <div class="cb">
                                            </div>
                                            <div style="padding: 30px 30px 30px 50px; font-size:16px;">
                                            <h1>
                                                    Hỗ trợ giao hàng tại các khu vực</h1>

                                                <table >
                                                    <tr style="background-color: #070706;color:white;text-align:center;">
                                                        <th style="width:100px;">
                                                            Quốc gia
                                                        </th>
                                                        <th style="width:150px;">
                                                            Tỉnh thành
                                                        </th>
                                                        <th style="width:200px;">
                                                            Quận/Huyện
                                                        </th>
                                                        <th style="width:150px;">
                                                            Chi phí (VNĐ)
                                                        </th>
                                                        <th style="width:200px;">
                                                            Mô tả
                                                        </th>
                                                        <th style="width:100px;">
                                                            #
                                                        </th>
                                                    </tr>
                                                    <% int j = 0;
                                                       foreach (var shipment in Model.Shipments)
                                                       { j++; %>
                                                    <tr <% if (j % 2 == 0) { %> style="background-color: #CBEBBD;" <% } %>>
                                                        <td align="center">
                                                            <%: shipment.AreaPart.CountryArea.Country.Name %>
                                                        </td>
                                                        <td align="center">
                                                            <%: shipment.AreaPart.CountryArea.AreaName %>
                                                        </td>
                                                        <td align="center">
                                                            <%: shipment.AreaPart.Name %>
                                                        </td>
                                                        <td align="center">
                                                            <%: shipment.Price.ToString("#,###") %>
                                                        </td>
                                                        <td align="center">
                                                            <%: shipment.Description %>
                                                        </td>
                                                        <td align="center">
                                                            
                                                        </td>
                                                    </tr>
                                                    <% } %>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
