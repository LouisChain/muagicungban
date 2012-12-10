<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<muagicungban.Entities.Order>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Chi tiết đơn hàng
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content">
        <div class="margin">
            <div class="content-helper clear">
                <div class="central-column">
                    <div>
                        <div class="mainbox-body">
                            <div class="central-content">
                                <div class="breadcrumbs">
                                    Thông tin đơn hàng
                                </div>
                                <div class="cm-notification-container">
                                </div>
                                <div class="mainbox-container">
                                    <h1 class="mainbox-title">
                                        <span>Thông tin đơn hàng</span></h1>
                                    <div class="mainbox-body">
                                        <h1>
                                            Thông tin đơn hàng</h1>
                                        <div class="clear order-info">
                                            <table cellpadding="2" cellspacing="0" border="0" class="float-left">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <strong>Đơn hàng</strong>:&nbsp;
                                                        </td>
                                                        <td>
                                                            #<%: Model.OrderID %>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 40px;">
                                                        <td>
                                                            <strong>Loại đơn hàng</strong>:&nbsp;
                                                        </td>
                                                        <td>
                                                            <% if ((string)ViewData["type"] == "sell")
                                                               { %>BÁN<% }
                                                               else
                                                               { %>MUA<% } %>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <strong>Ngày đặt</strong>:&nbsp;
                                                        </td>
                                                        <td>
                                                            <%: Model.OrderDate.ToString("dd/MM/yyyy hh:mm:ss") %>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 40px;">
                                                        <td>
                                                            <strong>Giao hàng</strong>:&nbsp;
                                                        </td>
                                                        <td>
                                                        <% if (Model.Item.OwnerID == ViewContext.HttpContext.User.Identity.Name)
                                                           { %>
                                                            <% using (Ajax.BeginForm("CheckDelivery/" + Model.OrderID, new AjaxOptions { }))
                                                               { %>
                                                            <input type="radio" name="IsDelivery" value="true" <% if (Model.IsDelivery) { %>
                                                                checked="checked" <% } %> onclick="$(this.form).submit();" />
                                                            Đã giao
                                                            <input type="radio" name="IsDelivery" value="false" <% if (!Model.IsDelivery) { %>
                                                                checked="checked" <% } %> onclick="$(this.form).submit();" />
                                                            Chưa giao
                                                            <% } %>
                                                        <% }
                                                           else
                                                           {
                                                               if (Model.IsDelivery)
                                                               {
                                                               %> Đã giao
                                                            <% }
                                                               else
                                                               { %>Chưa giao<% } %>
                                                        <% } %>
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <strong>Thanh toán</strong>:&nbsp;
                                                        </td>
                                                        <td>
                                                        <% if (Model.Item.OwnerID == ViewContext.HttpContext.User.Identity.Name)
                                                           { %>
                                                            <% using (Ajax.BeginForm("CheckPaid/" + Model.OrderID, new AjaxOptions { }))
                                                               { %>
                                                            <input type="radio" name="IsPaid" value="true" <% if (Model.IsPaid) { %> checked="checked"
                                                                <% } %> onclick="$(this.form).submit();" />
                                                            Đã thanh toán
                                                            <input type="radio" name="IsPaid" value="false" <% if (!Model.IsPaid) { %> checked="checked"
                                                                <% } %> onclick="$(this.form).submit();" />
                                                            Chưa thanh toán
                                                            <% } %>
                                                        <% }
                                                           else
                                                           {%>
                                                                <% if (Model.IsPaid)
                                                                   { %>Đã thanh toán<% }
                                                                   else
                                                                   { %>Chưa thanh toán<% } %>
                                                        <% } %>
                                                        </td>
                                                    </tr>
                                                    <% if (Model.Item.OwnerID == ViewContext.HttpContext.User.Identity.Name)
                                                       { %>
                                                    <tr style="height: 40px;">
                                                        <td>
                                                            <strong>Cho phép cập nhật
                                                                <br />
                                                                thông tin giao hàng</strong>:&nbsp;
                                                        </td>
                                                        <td>
                                                        
                                                            <% using (Ajax.BeginForm("CheckAllowEditShipment/" + Model.OrderID, new AjaxOptions { }))
                                                               { %>
                                                            <input type="radio" name="AllowEditShipment" value="true" <% if (Model.AllowEditShipment) { %>
                                                                checked="checked" <% } %> onclick="$(this.form).submit();" />
                                                            Có
                                                            <input type="radio" name="AllowEditShipment" value="false" <% if (!Model.AllowEditShipment) { %>
                                                                checked="checked" <% } %> onclick="$(this.form).submit();" />
                                                            Không
                                                            <% } %>
                                                        
                                                        </td>
                                                    </tr>
                                                    <% } %>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div class="border">
                                            <div class="subheaders-group">
                                                <h2 class="subheader">
                                                    Thông tin sản phẩm
                                                </h2>
                                                <table cellpadding="0" cellspacing="0" border="0" class="table product-list" width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <th>
                                                                ID
                                                            </th>
                                                            <th>
                                                                Tên sản phẩm
                                                            </th>
                                                            <th>
                                                                Giá thanh toán
                                                            </th>
                                                        </tr>
                                                        <tr valign="top">
                                                            <td>
                                                                #<%: Model.ItemID %>
                                                            </td>
                                                            <td class="">
                                                                <%: Model.Item.Title %>
                                                            </td>
                                                            <td class="center">
                                                                <%: Model.Item.CurPrice.ToString("#,### VND") %>
                                                            </td>
                                                        </tr>
                                                        <tr class="table-footer">
                                                            <td colspan="3">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <h2 class="subheader">
                                                    THÔNG TIN NGƯỜI BÁN
                                                </h2>
                                                <h5 class="info-field-title">
                                                    THÔNG TIN TÀI KHOẢN</h5>
                                                <div style="margin: 7px 0px 0px 5px">
                                                    <div style="widtht: 90px; float: left; font-weight: bold">
                                                        Tên tài khoản :</div>
                                                    <div style="width: 585px; margin-left: 90px">
                                                        <%: Model.Item.Owner.Username %></div>
                                                </div>
                                                <div style="margin: 7px 0px 0px 5px">
                                                    <div style="widtht: 90px; float: left; font-weight: bold">
                                                        Họ tên :</div>
                                                    <div style="width: 585px; margin-left: 90px">
                                                        <%: Model.Item.Owner.Name %></div>
                                                </div>
                                                <div style="margin: 7px 0px 0px 5px">
                                                    <div style="widtht: 90px; float: left; font-weight: bold">
                                                        Số điện thoại :</div>
                                                    <div style="width: 585px; margin-left: 90px">
                                                        <%: Model.Item.Owner.Phone %></div>
                                                </div>
                                                <div style="margin: 7px 0px 0px 5px">
                                                    <div style="widtht: 90px; float: left; font-weight: bold">
                                                        Email :</div>
                                                    <div style="width: 585px; margin-left: 90px">
                                                        <%: Model.Item.Owner.Email %></div>
                                                </div>
                                                <h2 class="subheader">
                                                    THÔNG TIN NGƯỜI MUA
                                                </h2>
                                                <h5 class="info-field-title">
                                                    THÔNG TIN TÀI KHOẢN</h5>
                                                <div style="margin: 7px 0px 0px 5px">
                                                    <div style="widtht: 90px; float: left; font-weight: bold">
                                                        Tên tài khoản :</div>
                                                    <div style="width: 585px; margin-left: 90px">
                                                        <%: Model.BuyerID %></div>
                                                </div>
                                                <div style="margin: 7px 0px 0px 5px">
                                                    <div style="widtht: 90px; float: left; font-weight: bold">
                                                        Họ tên :</div>
                                                    <div style="width: 585px; margin-left: 90px">
                                                        <%: Model.Buyer.Name %></div>
                                                </div>
                                                <div style="margin: 7px 0px 0px 5px">
                                                    <div style="widtht: 90px; float: left; font-weight: bold">
                                                        Email :</div>
                                                    <div style="width: 585px; margin-left: 90px">
                                                        <%: Model.Buyer.Email %></div>
                                                </div>
                                                <div style="margin: 7px 0px 0px 5px">
                                                    <div style="widtht: 90px; float: left; font-weight: bold">
                                                        Số điện thoại :</div>
                                                    <div style="width: 585px; margin-left: 90px">
                                                        <%: Model.Buyer.Phone %></div>
                                                </div>
                                                <br>
                                                <h5 class="info-field-title">
                                                    THÔNG TIN GIAO HÀNG</h5>
                                                <div style="margin: 7px 0px 3px 5px">
                                                    <div style="width: 90px; float: left; font-weight: bold">
                                                        Khu vực giao:</div>
                                                    <div style="width: 585px; margin-left: 90px">
                                                    <% if (Model.BuyerID == ViewContext.HttpContext.User.Identity.Name && Model.AllowEditShipment)
                                                       { %>
                                                        <% using (Ajax.BeginForm("GetCountryArea/" + Model.ItemID, new AjaxOptions { HttpMethod = "Post", UpdateTargetId = "area" }))
                                                           { %>
                                                        <select name="CountryID" id="country" style="width: 133px;" onchange="$(this.form).submit();">
                                                            <option selected="selected">Chọn quốc gia</option>
                                                            <% foreach (var country in (List<muagicungban.Entities.ShipCountry>)ViewData["countries"])
                                                               { %>
                                                            <option value="<%: country.ShipCountryID %>">
                                                                <%:country.Name%></option>
                                                            <% } %>
                                                        </select>
                                                        <% } %>
                                                        <% using (Ajax.BeginForm("GetAreaPart/" + Model.ItemID, new AjaxOptions { UpdateTargetId = "part" }))
                                                           { %>
                                                        <select name="AreaID" id="area" style="width: 133px;" onchange="$(this.form).submit();">
                                                            <option>-------------</option>
                                                        </select>
                                                        <% } %>
                                                        <% using (Ajax.BeginForm("GetPartPrice/" + Model.ItemID, new AjaxOptions { UpdateTargetId = "shipprice", OnSuccess = "updatePrice" }))
                                                           { %>
                                                        <select name="pID" id="part" style="width: 133px;" onchange="$(this.form).submit();shippart.value = this.value;bt.disabled='';" >
                                                            <option>------------</option>
                                                        </select>
                                                        <% } %>
                                                    <% } else {%>
                                                        <% if (Model.DeliveryPart != null)
                                                           {%>
                                                      <%: Model.DeliveryPart.Name%> / <%: Model.DeliveryPart.CountryArea.AreaName%> / <%: Model.DeliveryPart.CountryArea.Country.Name%>
                                                    <% }
                                                       } %>
                                                    </div>
                                                </div><br />
                                                
                                                <% using (Ajax.BeginForm("UpdateShipmentInfo/" + Model.OrderID, new AjaxOptions { UpdateTargetId = "shipprice", OnSuccess = "bt.disabled='disabled'" }))
                                                   { %>
                                                <input name="PartID" type="hidden" value="<%: Model.DeliveryPartID %>" id="_Part" />
                                                <div id="shipprice" style="display: none;">
                                                    <%: Model.ShipPrice %></div>
                                                <div id="itemprice" style="display: none;">
                                                    <%: Model.ItemPrice %></div>
                                                <script>
                                                    var shipprice = document.getElementById("shipprice");
                                                    var itemprice = document.getElementById("itemprice");
                                                    var shippart = document.getElementById("_Part");
                                                </script>
                                                <div style="margin: 7px 0px 3px 5px">
                                                    <div style="width: 90px; float: left; font-weight: bold">
                                                        Địa chỉ:</div>
                                                    <div style="width: 585px; margin-left: 90px">
                                                    <% if (Model.BuyerID == ViewContext.HttpContext.User.Identity.Name && Model.AllowEditShipment)
                                                       { %>
                                                        <input type='text' name="Address" value="<%: Model.DeliveryAddress %>" onkeypress="bt.disabled='';" />
                                                    <% }
                                                       else
                                                       { %>
                                                        <%: Model.DeliveryAddress%>
                                                    <% } %>
                                                    </div>
                                                </div><br />
                                                <div style="margin: 7px 0px 3px 5px">
                                                    <div style="width: 90px; float: left; font-weight: bold">
                                                        Họ tên:</div>
                                                    <div style="width: 585px; margin-left: 90px">
                                                    <% if (Model.BuyerID == ViewContext.HttpContext.User.Identity.Name && Model.AllowEditShipment)
                                                       { %>
                                                        <input type="text" name="Name" value="<%: Model.ReceiverName %>" onkeypress="bt.disabled='';" />
                                                    <% }
                                                       else
                                                       { %>
                                                    <%: Model.ReceiverName%>
                                                    <% } %>
                                                    </div>
                                                </div><br />
                                                <div style="margin: 7px 0px 3px 5px">
                                                    <div style="width: 90px; float: left; font-weight: bold">
                                                        Điện thoại:</div>
                                                    <div style="width: 585px; margin-left: 90px">
                                                    <% if (Model.BuyerID == ViewContext.HttpContext.User.Identity.Name && Model.AllowEditShipment)
                                                       { %>
                                                        <input type="text" name="Phone" value="<%: Model.ReceiverPhone %>" onkeypress="bt.disabled='';" />
                                                    <% }
                                                       else
                                                       { %>
                                                        <%: Model.ReceiverPhone %>
                                                    <% } %>
                                                    </div>
                                                </div><br />
                                                <div style="margin: 7px 0px 0px 5px">
                                                    <div style="widtht: 90px; float: left; font-weight: bold">
                                                        Email:</div>
                                                    <div style="width: 585px; margin-left: 90px">
                                                    <% if (Model.BuyerID == ViewContext.HttpContext.User.Identity.Name && Model.AllowEditShipment)
                                                       { %>
                                                        <input type="text" name="Email" value="<%: Model.ReceiverEmail %>" onkeypress="bt.disabled='';" />
                                                    <% }
                                                       else
                                                       { %>
                                                        <%: Model.ReceiverEmail %> &nbsp;
                                                    <% } %>
                                                    </div>
                                                </div><br />
                                                    <% if (Model.BuyerID == ViewContext.HttpContext.User.Identity.Name && Model.AllowEditShipment)
                                                       { %>
                                                    <div style="width: 585px; margin-left: 94px;">
                                                        <input type="submit" id="updateShipmentButton" value="Cập nhật" />
                                                    </div>
                                                    <% } %>
                                                <% } %>
                                                <script>
                                                    var bt = document.getElementById("updateShipmentButton");
                                                </script>
                                                <h2 class="subheader">
                                                    Tóm tắt
                                                </h2>
                                                <table width="100%" class="fixed-layout">
                                                    <tbody>
                                                        <tr>
                                                            <td width="220">
                                                                <strong>Hình thức thanh toán:&nbsp;</strong>
                                                            </td>
                                                            <td>
                                                            <% if (Model.Item.OwnerID == ViewContext.HttpContext.User.Identity.Name && Model.AllowEditShipment)
                                                               { %>
                                                                <% using (Ajax.BeginForm("UpdatePaymentMethod/" + Model.OrderID, new AjaxOptions { OnSuccess = "el.style.display='inline'" }))
                                                                   { %>
                                                                <input style="width: 300px;" type="text" name="PaymentMethod" value="<%: Model.PaymentMethod %>"
                                                                    onkeypress="el.style.display='none'" onchange="$(this.form).submit();" />
                                                                <span style="color: Green; display: none;" id="updatePaymentMessage">Đã cập nhật</span>
                                                                <% } %>
                                                                <script>
                                                                    var el = document.getElementById("updatePaymentMessage");
                                                                </script>
                                                            <% }
                                                               else
                                                               {%>
                                                              <%: Model.PaymentMethod%>
                                                              <% } %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <strong>Giá mua sản phẩm</strong>:&nbsp;
                                                            </td>
                                                            <td id="showitemprice">
                                                                <%: Model.ItemPrice.ToString("0,### VND") %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <strong>Phí vận chuyển</strong>:&nbsp;
                                                            </td>
                                                            <td id="showshipprice">
                                                                <%: Model.ShipPrice.ToString("0,### VND") %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <strong>Tổng chi phí thanh toán:&nbsp;</strong>
                                                            </td>
                                                            <td id="sumprice">
                                                                <%: (Model.ItemPrice + Model.ShipPrice).ToString("#,### VND")%>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <script>
                                                    var showitemprice = document.getElementById("showitemprice");
                                                    var showshipprice = document.getElementById("showshipprice");
                                                    var sumprice = document.getElementById("sumprice");
                                                    function updatePrice() {
                                                        showshipprice.innerHTML = formatNumber(Number(shipprice.innerHTML),0) + " VND";
                                                        sumprice.innerHTML = formatNumber((Number(shipprice.innerHTML) + Number(itemprice.innerHTML)), 0) + " VND";
                                                    }
                                                    function formatNumber(num, fixed) {
                                                        var decimalPart;

                                                        var array = Math.floor(num).toString().split('');
                                                        var index = -3;
                                                        while (array.length + index > 0) {
                                                            array.splice(index, 0, ',');
                                                            index -= 4;
                                                        }

                                                        if (fixed > 0) {
                                                            decimalPart = num.toFixed(fixed).split(".")[1];
                                                            return array.join('') + "." + decimalPart;
                                                        }
                                                        return array.join('');
                                                    };
                                                </script>
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
