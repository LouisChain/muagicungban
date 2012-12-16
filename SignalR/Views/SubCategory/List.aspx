<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<muagicungban.Models.SubCategory>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="helper-container">
        <div id="container" class="container-left">
            <div id="content">
                <div class="margin">
                    <div class="content-helper clear">
                        <div class="central-column">
                            <div>
                                <div class="mainbox-body">
                                    <div class="central-content">
                                        <div class="box_product_detail">
                                        <h1 style="padding-left:30px;">QUẢN LÝ DANH MỤC SẢN PHẨM</h1>
                                            <table style="padding-top:20px;padding-bottom:30px;padding-left:50px;font-size:16px;">
                                                <tr style="height:30px;background-color:Black;color:White;">
                                                    <th style="width:190px;">
                                                        Danh mục
                                                    </th>
                                                    <th>
                                                        Chức năng
                                                    </th>
                                                </tr>
                                                <% int j = 0;
                                                    foreach (var item in Model)
                                                    {
                                                        ++j;%>
                                                <tr id="<%: item.ID %>" <% if (j % 2 == 0) { %> style="background-color:#F1E8E8;"<% } %>>
                                                    <td>
                                                        
                                                            <a id="show<%: item.ID %>" onclick="beginEdit<%: item.ID %>();"><%: item.Name %></a>
                                                        
                                                        <div style="display:none;" id="value<%: item.ID %>"><%: item.Name %></div>
                                                        <div style="display:none;" id="edit<%: item.ID %>">
                                                        <% using (Ajax.BeginForm("edit/"+item.ID, new AjaxOptions { UpdateTargetId = "value" + item.ID, OnSuccess = "successEdit" + item.ID }))
                                                           { %>
                                                            <input type="text" name="catName" value="<%: item.Name %>" onchange="$(this.form).submit();" onmouseout="successEdit<%: item.ID %>();" />
                                                        <% } %>
                                                        </div>
                                                        <script>
                                                            var Show<%: item.ID %> = document.getElementById("show<%: item.ID %>");
                                                            var Edit<%: item.ID %> = document.getElementById("edit<%: item.ID %>");
                                                            var Value<%: item.ID %> = document.getElementById("value<%: item.ID %>");
                                                            function beginEdit<%: item.ID %>() {
                                                                Show<%: item.ID %>.style.display = "none";
                                                                Edit<%: item.ID %>.style.display = "inline";
                                                            }
                                                            function successEdit<%: item.ID %>() {
                                                                Show<%: item.ID %>.innerHTML = Value<%: item.ID %>.innerHTML;
                                                                Edit<%: item.ID %>.innerHTML = Value<%: item.ID %>.innerHTML;
                                                                Show<%: item.ID %>.style.display = "inline";
                                                                Edit<%: item.ID %>.style.display = "none";
                                                            }
                                                        </script>
                                                    </td>
                                                    <td style="text-align:center;">
                                                        <% using (Html.BeginForm("Delete", "SubCategory")) %>
                                                        <% { %>
                                                        <%: Html.Hidden("ID", item.ID)%>
                                                        <button type="submit">
                                                            Xóa</button>
                                                        <% } %>
                                                    </td>
                                                </tr>
                                                <% } %>
                                                <tr style="height:5px;">
                                                    <td colspan="2">
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <% using (Html.BeginForm("Create", "SubCategory"))
                                                       { %>
                                                    <td>
                                                        <input type="text" name="catName" />
                                                    </td>
                                                    <td style="text-align:center">
                                                        <input type="submit" value="Tạo mới" />
                                                    </td>
                                                    <% } %>
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
        </div>
    </div>
</asp:Content>
