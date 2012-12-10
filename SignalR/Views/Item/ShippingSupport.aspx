<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<muagicungban.Entities.Shipment>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ShippingSupport
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="helper-container">
        <div id="container" class="container-left">
            <div id="content">
                <div class="margin">
                    <div class="content-helper clear">
                        <div class="central-column">
                            <div class="mainbox-container">
                                <div class="mainbox-body">
                                    <div class="central-content">
                                        <div class="box_product_detail">
                                            <div class="box_product_detail_padding" style="padding: 30px 30px 30px 50px; font-size: 16px;">
                                                <h1>
                                                    Hỗ trợ giao hàng tại các khu vực</h1>
                                                <% if (TempData["error_message"] != null)
                                                   { %>
                                                <%: TempData["error_message"]%>
                                                <% }
                                                   else
                                                   {%>
                                                <table >
                                                    <tr style="background-color: #070706;color:white;text-align:center;">
                                                        <th>
                                                            Quốc gia
                                                        </th>
                                                        <th>
                                                            Tỉnh thành
                                                        </th>
                                                        <th>
                                                            Quận/Huyện
                                                        </th>
                                                        <th>
                                                            Chi phí (VNĐ)
                                                        </th>
                                                        <th>
                                                            Mô tả
                                                        </th>
                                                        <th style="width:100px;">
                                                            Chức năng
                                                        </th>
                                                    </tr>
                                                    <% int i = 0;
                                                       foreach (var shipment in Model)
                                                       { i++;%>
                                                    <tr <% if (i % 2 == 0) { %> style="background-color: #CBEBBD;" <% } %>>
                                                        <td align="center">
                                                            <%: shipment.AreaPart.CountryArea.Country.Name%>
                                                        </td>
                                                        <td align="center">
                                                            <%: shipment.AreaPart.CountryArea.AreaName%>
                                                        </td>
                                                        <td align="center">
                                                            <%: shipment.AreaPart.Name%>
                                                        </td>
                                                        <td align="center">
                                                            <%: shipment.Price.ToString("#,###")%>
                                                        </td>
                                                        <td align="center">
                                                            <%: shipment.Description%>
                                                        </td>
                                                        <td align="center">
                                                            <%: Html.ActionLink("Del", "ShippingSupportDelete", new { id = shipment.ShipmentID })%>
                                                        </td>
                                                    </tr>
                                                    <% } %>
                                                    <tr>
                                                        <td>
                                                            <% using (Ajax.BeginForm("GetCountryArea", new AjaxOptions { HttpMethod = "Post", UpdateTargetId = "area" }))
                                                               { %>
                                                            <select name="CountryID" id="country" onchange="$(this.form).submit();">
                                                                <option selected="selected">Select country</option>
                                                                <% foreach (var country in (List<muagicungban.Entities.ShipCountry>)ViewData["Countries"])
                                                                   { %>
                                                                <option value="<%: country.ShipCountryID %>">
                                                                    <%:country.Name%></option>
                                                                <% } %>
                                                            </select>
                                                            <% } %>
                                                        </td>
                                                        <td>
                                                            <% using (Ajax.BeginForm("GetAreaPart", new AjaxOptions { UpdateTargetId = "part" }))
                                                               { %>
                                                            <select name="AreaID" id="area" onchange="$(this.form).submit();">
                                                                <option>-------------</option>
                                                            </select>
                                                            <% } %>
                                                        </td>
                                                        <form action="/Item/ShippingSupportAdd" method="post">
                                                        <td>
                                                            <select name="PartID" id="part">
                                                                <option>------------</option>
                                                            </select>
                                                        </td>
                                                        <td>
                                                            <input type="text" name="Price" style="width:100px;" />
                                                        </td>
                                                        <td>
                                                            <input type="text" name="Description" />
                                                        </td>
                                                        <td>
                                                            <input type="hidden" name="ItemID" value="<%: ViewData["ItemID"] %>" />
                                                            <input type="submit" value="Add" />
                                                        </td>
                                                        </form>
                                                    </tr>
                                                </table>
                                                <%} %>
                                                <div style="padding-top:20px; text-align:center;">
                                                    <a href="http://<%: Request.Url.Authority %>/item/edit/<%: ViewData["ItemID"] %>"><input type="button" value="<<- Trở lại thông tin sản phẩm" /></a>
                                                    <a href="http://<%: Request.Url.Authority %>/item/PostingArea/<%: ViewData["ItemID"] %>"><input type="button" value="Chọn vị trí đăng ->>" /></a>
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
    </div>
</asp:Content>
