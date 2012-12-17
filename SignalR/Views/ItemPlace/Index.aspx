<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<muagicungban.Entities.ItemPlace>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Quản lý đăng tin
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content">
        <div class="margin">
            <div class="content-helper clear">
                <div class="central-column">
                <% Html.RenderPartial("ItemPlaceMenu"); %>
                    <div class="central-content">
                        <div class="mainbox-container">
                            <div class="mainbox-body">
                                <div id="content_general">
                                    <table style="font-size:16px;width:100%;">
                                        <tr>
                                            <th style="width:10%;">
                                                Tên vị trí
                                            </th>
                                            <th>
                                                Mã sản phẩm
                                            </th>
                                            <th>
                                                Ngày hiển thị
                                            </th>
                                            <th>
                                                Ngày kết thúc
                                            </th>
                                            <th>
                                                Thanh toán
                                            </th>
                                            <th>
                                                Số tiền
                                            </th>
                                        </tr>
                                        <% foreach (var item in Model)
                                           { %>
                                        <tr>
                                            <td>
                                                <%: item.PlaceName %>
                                            </td>
                                            <td>
                                                <%: item.ItemID %>
                                            </td>
                                            <td>
                                                <%: item.StartDate.ToString("dd/MM/yyyy hh:mm") %>
                                            </td>
                                            <td>
                                                <%: item.EndDate.ToString("dd/MM/yyyy hh:mm") %>
                                            </td>
                                            <td>
                                                <% if (item.IsPaid)
                                                   { %>Rồi<% }
                                                   else
                                                   { %>Chưa<% } %>
                                            </td>
                                            <td>
                                                <%: item.PaidMoney.ToString("#,### VND") %>
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
    <div class="pagination cm-pagination-wraper center">
        <%: Html.PageLinks(new muagicungban.Models.PagingInfo {CurrentPage = (int)ViewData["currentPage"], 
                                                           TotalItems = (int)ViewData["totalItems"], 
                                                           ItemsPerPage = (int)ViewData["pageSize"] },
        i => Url.Content(Request.Url.AbsolutePath + "?page=" + i + ((ViewData["key"] != null) ? "&key=" + ViewData["key"] : "") +((ViewData["category"] != null) ? "&category=" + ViewData["category"] : "")))%>
    </div>
</asp:Content>
