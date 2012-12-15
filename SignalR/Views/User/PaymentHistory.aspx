<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<muagicungban.Entities.PaymentHistory>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	PaymentHistory
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="content">
        <div class="margin">
            <div class="content-helper clear">
                <div class="central-column">
                    <div class="central-content">
                        <div class="mainbox-container">
                            <div class="mainbox-body">
                                <div id="content_general">
                                    <table cellspacing="0" style="width:100%;font-size:14px;" >
                                        <tr style="text-align:left;background-color:#B4D5B4;height:40px;" >
                                            <th>
                                                #
                                            </th>
                                            <th>
                                                Ngày thanh toán
                                            </th>
                                            <th>
                                                Thanh toán
                                            </th>
                                            <th>
                                                Còn lại
                                            </th>
                                            <th align="center">
                                                Nội dung
                                            </th>
                                            <th align="center">
                                                Tên tài khoản
                                            </th>
                                        </tr>
                                        <% int j = 0;
                                            foreach (var item in Model)
                                            {
                                                ++j;%>
                                        <tr style="height:30px;<% if (j % 2 == 0) { %> background-color:#EDF0F0; <% } %>">
                                            <td style="padding-right:5px;">
                                                <%: item.HistoryID %>
                                            </td>
                                            <td>
                                                <%: item.PaidDate%>
                                            </td>
                                            <td>
                                                <%: item.PaidMoney.ToString("#,### VNĐ")%>
                                            </td>
                                            <td>
                                                <%: item.TotalMoney.ToString("#,### VNĐ")%>
                                            </td>
                                            <td align="center">
                                                <%: item.PaidContent %>
                                            </td>
                                            <td align="center">
                                                <%: item.Username %>
                                            </td>

                                        </tr>
                                        <% } %>
                                        <tr style="background-color:#B4D5B4;height:3px;">
                                            <td colspan="6"></td>
                                        </tr>
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

