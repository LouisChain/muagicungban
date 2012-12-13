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
                                    <table width="100%">
                                        <tr style="height:30px;background-color:Black;color:White;text-align:center;">
                                            <th style="width:15%;">
                                                Tên sản phẩm
                                            </th>
                                            <%--1--%>
                                            <th style="width:15%;">
                                                Hình ảnh
                                            </th>
                                            <%--2--%>
                                            <th style="width:25%;">
                                               Mô tả
                                            </th>
                                            <%--3--%>
                                            <th style="width:10%;">
                                                Danh mục
                                            </th>
                                            <%--4--%>
                                            <th>
                                                Cách bán
                                            </th>
                                            <%--5--%>
                                            <th>
                                                Giá tối đa
                                            </th>
                                            <%--6--%>
                                            <th>
                                                Kích hoạt
                                            </th>
                                            <%--7--%>
                                            <th>
                                                Đã bán
                                            </th>
                                            <%--8--%>
                                            <th>
                                                Đã kiểm duyệt
                                            </th>
                                            <%--9--%>
                                        </tr>
                                        <% int j = 0;
                                            foreach (var item in Model)
                                            {
                                                ++j;%>
                                        <tr <% if(j % 2 == 0) { %>style="background-color:#DDD7D7;"<% } %>>
                                            <td>
                                                <%--1--%>
                                                <% if (Roles != null && Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
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
                                                <%: item.SubCategory.Name %>
                                            </td>
                                            <td>
                                                <%--5--%>
                                                <% if (item.IsAuction)
                                                   { %>Đấu giá
                                                <% }
                                                   else
                                                   { %>Chỉ bán<% } %>
                                            </td>
                                            <td>
                                                <%--6--%>
                                                <%: item.MaxPrice.ToString("#,### VND") %>
                                            </td>
                                            <td align="center">
                                                <%--7--%>
                                                <% if (item.IsActive)
                                                   { %>OK<% }
                                                   else
                                                   { %>Chưa<% } %>
                                            </td>
                                            <td align="center">
                                                <%--8--%>
                                                <% if (item.IsSold)
                                                   { %>OK<% }
                                                   else
                                                   { %>Chưa<% } %>
                                            </td>
                                            <td align="center">
                                                <%--9--%>
                                                <% if (item.IsChecked)
                                                   { %>OK<% }
                                                   else
                                                   { %>Chưa<% } %>
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
