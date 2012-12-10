<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<muagicungban.Entities.Order>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Quản lý đơn hàng
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content">
        <div class="margin">
            <div class="content-helper clear">
                <div class="central-column">
                    <div class="central-content">
                        <div>
                            <div class="mainbox-body">
                                <div class="nav_2" style="height: 96px;">
                                    <ul>
                                        <div class="nu bor_bt" style="font-size: 16px;">
                                            <li style="font-size: 14px; padding: 6px 3px 4px; min-width: auto;"><a href="http://<%: Request.Url.Authority %>/order/?type=sell" style="width: 155px;
                                                min-width: 0; padding: 2px 2px; <% if ((string)ViewData["type"] =="sell") { %>background-color:#1ECA17; <% } %>">Bán <span class="total">(<%: ViewData["sell"] %>)</span></a></li>
                                            <li style="font-size: 14px; padding: 6px 3px 4px; min-width: auto;"><a href="http://<%: Request.Url.Authority %>/order/?type=buy" style="width: 155px;
                                                min-width: 0; padding: 2px 2px; <% if ((string)ViewData["type"] =="buy") { %>background-color:#1ECA17; <% } %>">Mua <span class="total">(<%: ViewData["buy"] %>)</span></a></li>
                                            <div class="cb">
                                            </div>
                                        </div>
                                        <div class="nu bg bor_bt">
                                            <li style="width: 75px; min-width: 0;">
                                                <p>
                                                    Tình trạng</p>
                                            </li>
                                            <div id="status" <% if ((string)ViewData["type"] == "all") { %> style="display:none;" <% } %> >
                                                <li style="font-size: 12px; padding: 6px 3px 4px; min-width: auto;"><a href="http://<%: Request.Url.Authority %>/order/?type=<%:ViewData["type"] %>&&status=1" style="width: 120px;
                                                    min-width: 0; padding: 2px 2px; <% if ((string)ViewData["status"] =="1") { %>background-color:#1ECA17; <% } %>">Chưa thanh toán<br />
                                                    <span class="total">(<%: ViewData["not_paid"] %>)</span></a></li>
                                                <li style="font-size: 12px; padding: 6px 3px 4px; min-width: auto;"><a href="http://<%: Request.Url.Authority %>/order/?type=<%:ViewData["type"] %>&&status=2" style="width: 120px;
                                                    min-width: 0; padding: 2px 2px; <% if ((string)ViewData["status"] =="2") { %>background-color:#1ECA17; <% } %>">Thanh toán
                                                    <br />
                                                    (chưa giao hàng) <span class="total">(<%: ViewData["paid_not_delivery"] %>)</span></a></li>
                                                <li style="font-size: 12px; padding: 6px 3px 4px; min-width: auto;"><a href="http://<%: Request.Url.Authority %>/order/?type=<%:ViewData["type"] %>&&status=3" style="width: 120px;
                                                    min-width: 0; padding: 2px 2px; <% if ((string)ViewData["status"] =="3") { %>background-color:#1ECA17; <% } %>">Thanh toán
                                                    <br />
                                                    (đã giao hàng) <span class="total">(<%: ViewData["paid_delivery"] %>)</span></a></li>
                                            </div>
                                            <div class="cb">
                                            </div>
                                        </div>
                                    </ul>
                                    <div class="cb">
                                    </div>
                                </div>

                                    <div class="box_product_detail">
                                        <div class="box_product_detail_padding" style="">
                                            <div>
                                                <h1 class="mainbox-title">
                                                    <span>Đơn hàng</span></h1>
                                                <div class="mainbox-body">
                                                    <h1>
                                                        Đơn hàng</h1>

                                                    <div class="pagination-container" id="pagination_contents">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="table">
                                                            <tbody>
                                                                <tr>
                                                                    <th width="10%">
                                                                            ID
                                                                    </th>
                                                                    <th width="15%">
                                                                            Loại
                                                                    </th>
                                                                    <th width="20%">
                                                                            Tên hàng
                                                                    </th>
                                                                    <th width="15%">
                                                                        Thanh toán
                                                                    </th>
                                                                    <th width="15%">
                                                                            Giao hàng
                                                                    </th>
                                                                    <th width="15%">
                                                                            Ngày đặt
                                                                    </th>
                                                                </tr>
                                                                <%  int i = 0;
                                                                    foreach (var order in Model)
                                                                    {
                                                                        ++i;%>
                                                                <tr <% if (i%2==0) { %>class="table-row" <% } %>>
                                                                    <td class="center">
                                                                        <a href="http://<%: Request.Url.Authority %>/order/detail/<%: order.OrderID %>"><strong><%: order.OrderID %></strong></a>
                                                                    </td>
                                                                    <td>
                                                                        <% if (ViewContext.HttpContext.User.Identity.Name == order.Item.OwnerID)
                                                                           { %>
                                                                            Bán
                                                                        <% }
                                                                           else
                                                                           { %>
                                                                            Mua
                                                                        <% } %>
                                                                    </td>
                                                                    <td>
                                                                        <%: order.Item.Title %>
                                                                    </td>
                                                                    <td>
                                                                        <% if (order.IsPaid)
                                                                           { %>
                                                                        Rồi
                                                                        <% }
                                                                           else
                                                                           {%>
                                                                        Chưa
                                                                        <% } %>
                                                                    </td>
                                                                    <td>
                                                                       <% if (order.IsDelivery)
                                                                          { %> Rồi <% }
                                                                          else
                                                                          { %> Chưa <% } %>
                                                                    </td>
                                                                    <td>
                                                                        <%: order.OrderDate.ToString("dd/MM/yyyy") %>
                                                                    </td>
                                                                </tr>
                                                                <% } %>
                                                               
                                                                <tr class="table-footer">
                                                                    <td colspan="6">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <!--pagination_contents-->
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
