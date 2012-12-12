<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<muagicungban.Entities.Item>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% var Roles = (List<muagicungban.Entities.UserRoles>)ViewData["roles"]; %>
    <div id="content">
        <div class="margin">
            <div class="content-helper clear">
                <div class="central-column">
                <% Html.RenderPartial("ItemMenu"); %>
                    <div class="central-content">
                        <div class="mainbox-container">
                            <div class="mainbox-body">
                                <div id="content_general">
                                    <table>
                                        <tr>
                                            <th>
                                                Title
                                            </th>
                                            <%--1--%>
                                            <th>
                                                Image
                                            </th>
                                            <%--2--%>
                                            <th>
                                                Description
                                            </th>
                                            <%--3--%>
                                            <th>
                                                Category
                                            </th>
                                            <%--4--%>
                                            <th>
                                                Selling method
                                            </th>
                                            <%--5--%>
                                            <th>
                                                Price
                                            </th>
                                            <%--6--%>
                                            <th>
                                                IsActive
                                            </th>
                                            <%--7--%>
                                            <th>
                                                IsSold
                                            </th>
                                            <%--8--%>
                                            <th>
                                                IsChecked
                                            </th>
                                            <%--9--%>
                                        </tr>
                                        <% foreach (var item in Model)
                                           { %>
                                        <tr>
                                            <td>
                                                <%--1--%>
                                                <% if (Roles != null && Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "manager"))
                                                   { %>
                                                    <%: Html.ActionLink(item.Title, "EditItem", new { id = item.ItemID })%>
                                                <% }
                                                   else
                                                   { %>
                                                <%: Html.ActionLink(item.Title, "Edit", new { id = item.ItemID })%>
                                                <% } %>
                                            </td>
                                            <td>
                                                <%--2--%>
                                                <% if (item.ImageData != null)
                                                   { %>
                                                <img alt="" height="90" width="100" src="<%: Url.Action("GetImage", "Item", new { item.ItemID}) %>" />
                                                <% } %>
                                            </td>
                                            <td>
                                                <%--3--%>
                                                <%: item.Description%>
                                            </td>
                                            <td>
                                                <%--4--%>
                                            </td>
                                            <td>
                                                <%--5--%>
                                                <% if (item.IsAuction)
                                                   { %>Auction
                                                <% }
                                                   else
                                                   { %>Sell<% } %>
                                            </td>
                                            <td>
                                                <%--6--%>
                                                <%: item.MaxPrice%>
                                            </td>
                                            <td align="center">
                                                <%--7--%>
                                                <% if (item.IsActive)
                                                   { %>Yes<% }
                                                   else
                                                   { %>No<% } %>
                                            </td>
                                            <td align="center">
                                                <%--8--%>
                                                <% if (item.IsSold)
                                                   { %>Yes<% }
                                                   else
                                                   { %>No<% } %>
                                            </td>
                                            <td align="center">
                                                <%--9--%>
                                                <% if (item.IsChecked)
                                                   { %>Yes<% }
                                                   else
                                                   { %>No<% } %>
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
        i => Url.Action("List",new {page = i})) %>
    </div>
</asp:Content>
