<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<muagicungban.Entities.User>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    List
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
                                    <table>
                                        <tr>
                                            <th>
                                                Username
                                            </th>
                                            <th>
                                                Full Name
                                            </th>
                                            <th>
                                                Email
                                            </th>
                                            <th>
                                                Phone
                                            </th>
                                            <th>
                                                Action
                                            </th>
                                        </tr>
                                        <% foreach (var item in Model)
                                           { %>
                                        <tr>
                                            <td>
                                                <%: Html.ActionLink(item.Username, "Edit", new {Username = item.Username}) %>
                                            </td>
                                            <td>
                                                <%: item.Name%>
                                            </td>
                                            <td>
                                                <%: item.Email%>
                                            </td>
                                            <td>
                                                <%: item.Phone%>
                                            </td>
                                            <td>
                                                <% using (Html.BeginForm("Delete", "User"))
                                                   { %>
                                                <%: Html.Hidden("Username", item.Username)%>
                                                <button type="submit">
                                                    Delete</button>
                                                <% } %>
                                            </td>
                                        </tr>
                                        <% } %>
                                    </table>
                                    <p>
                                        <%: Html.ActionLink("Add new", "Create") %></p>
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
