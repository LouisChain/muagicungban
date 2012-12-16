<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<muagicungban.Entities.User>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Danh sách người dùng
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
                                                Tên đăng nhập
                                            </th>
                                            <th>
                                                Họ tên
                                            </th>
                                            <th>
                                                Email
                                            </th>
                                            <th>
                                                Số điện thoại
                                            </th>
                                            <th align="center">
                                                Khóa
                                            </th>
                                            <th align="center">
                                                Kích hoạt
                                            </th>
                                        </tr>
                                        <% int j = 0;
                                            foreach (var item in Model)
                                            {
                                                ++j;%>
                                        <tr style="height:30px;<% if (j % 2 == 0) { %> background-color:#EDF0F0; <% } %>">
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
                                            <td align="center">
                                                <% using (Ajax.BeginForm("CheckBan/"+item.Username, new AjaxOptions { }))
                                                   { %>
                                                    <input type="checkbox" name="isban" value="true" onclick="$(this.form).submit();" <% if (item.IsBan) {%>checked="checked"<% } %> />
                                                <% } %>
                                            </td>
                                            <td align="center">
                                                <% if (item.IsActive)
                                                   { %>OK<% }
                                                   else
                                                   {%>
                                                        <% using (Ajax.BeginForm("CheckActive/" + item.Username, new AjaxOptions { OnSuccess = "showActiveSuccess"}))
                                                           { %>
                                                            <input id="btnActive" type="submit" value="Kích hoạt" />
                                                        <% } %>
                                                        <div id="activemessage" style="display:none;color:Green;">OK</div>
                                                        <script>
                                                            var btnactive = document.getElementById("btnActive");
                                                            var active = document.getElementById("activemessage");
                                                            function showActiveSuccess() {
                                                                btnactive.style.display = "none";
                                                                active.style.display = "block";
                                                            }
                                                        </script>
                                                <% } %>
                                            </td>

                                        </tr>
                                        <% } %>
                                        <tr style="background-color:#B4D5B4;height:3px;">
                                            <td colspan="6"></td>
                                        </tr>
                                    </table>
                                    <p style="text-align:center;font-weight:bolder;font-size:14px;">
                                        <%: Html.ActionLink("Thêm mới", "Create") %></p>
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
