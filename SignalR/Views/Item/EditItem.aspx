<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<muagicungban.Entities.Item>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EditItem
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% var roles = (List<muagicungban.Entities.UserRoles>)ViewData["roles"];
       var categories = (List<muagicungban.Models.SubCategory>)ViewData["category"]; 
    %>
    <div class="helper-container">
        <div id="container" class="container-left">
            <div id="content">
                <div class="margin">
                    <div class="content-helper clear">
                        <div class="central-column">
                            <% Html.RenderPartial("ItemMenu"); %>
                            <div>
                                <div class="mainbox-body">
                                    <div class="central-content">
                                        <div class="box_product_detail">
                                            <div class="box_product_detail_padding" style="padding: 30px 30px 30px 50px; font-size: 16px;">
                                                <h2>
                                                    Chi tiết kiểm duyệt</h2>
                                                <table>
                                                    <tr>
                                                        <td style="width: 200px;">
                                                            <div class="display-label">
                                                                Mã sản phẩm</div>
                                                        </td>
                                                        <td>
                                                            <div class="display-field">
                                                                <%: Model.ItemID %></div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="display-label">
                                                                Tên sản phẩm</div>
                                                        </td>
                                                        <td>
                                                            <div class="display-field">
                                                                <%: Model.Title %></div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="display-label">
                                                                Mô tả</div>
                                                        </td>
                                                        <td>
                                                            <div class="display-field">
                                                                <%: Model.Description %></div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="display-label">
                                                                Chủ sở hữu</div>
                                                        </td>
                                                        <td>
                                                            <div class="display-field">
                                                                <%: Model.OwnerID %></div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="display-label">
                                                                Danh mục</div>
                                                        </td>
                                                        <td>
                                                            <div class="display-field">
                                                                <% if (roles != null && roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
                                                                   {%>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <% using (Ajax.BeginForm("SetCategory/" + Model.ItemID, new AjaxOptions { UpdateTargetId = "categorymessage", OnSuccess = "displayMessage" }))
                                                                               { %>
                                                                            <select name="categoryID" onchange="$(this.form).submit();">
                                                                                <option>---------</option>
                                                                                <% foreach (var item in categories)
                                                                                   { %>
                                                                                <option value="<%: item.ID %>" <% if (item.ID == Model.SubCategoryID) { %> selected = "selected" <% } %>>  
                                                                                    <%: item.Name%></option>
                                                                                <% } %>
                                                                            
                                                                            </select>
                                                                            <% } %>
                                                                        </td>
                                                                        <td>
                                                                            <span id="categorymessage" style="display: none; color: Green; font-weight: bold;">
                                                                            </span>
                                                                        </td>
                                                                    </tr>
                                                                </table>

                                                                <script>
                                                                    var category_message = document.getElementById("categorymessage");
                                                                    function displayMessage() {
                                                                        category_message.style.display = "inline";

                                                                    }
                                                                </script>
                                                                <% }
                                                                   else
                                                                   {%>
                                                                <%: Model.SubCategoryID%>
                                                                <% } %>
                                                            </div>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <div class="display-label">
                                                                Mức giá tối đa</div>
                                                        </td>
                                                        <td>
                                                            <div class="display-field">
                                                                <%: Model.MaxPrice.ToString("#,### VND") %></div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="display-label">
                                                                Ngày bắt đầu</div>
                                                        </td>
                                                        <td>
                                                            <div class="display-field">
                                                                <%: Model.StartDate.ToString("dd/MM/yyyy hh:mm") %></div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="display-label">
                                                                Ngày kết thúc</div>
                                                        </td>
                                                        <td>
                                                            <div class="display-field">
                                                                <%: Model.EndDate.ToString("dd/MM/yyyy hh:mm") %></div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="display-label">
                                                                Giá bắt đầu</div>
                                                        </td>
                                                        <td>
                                                            <div class="display-field">
                                                                <%: Model.StartPrice.ToString("#,### VND") %></div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="display-label">
                                                                Đã kiểm duyệt</div>
                                                        </td>
                                                        <td>
                                                            <div class="display-field">
                                                                <% if (roles != null && roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
                                                                   {%>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <% using (Ajax.BeginForm("Check/" + Model.ItemID, new AjaxOptions { UpdateTargetId = "checkmessage", OnSuccess = "displayCheckMessage" }))
                                                                               { %>
                                                                                <input type="checkbox" name="check" value="true" onclick = "$(this.form).submit();" <% if(Model.IsChecked) { %> checked="checked" <% } %> />
                                                                            <% } %>
                                                                        </td>
                                                                        <td>
                                                                            <span id="checkmessage" style="display: none; color: Green; font-weight: bold;">
                                                                            </span>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                
                                                                <script>
                                                                    var check_message = document.getElementById("checkmessage");
                                                                    function displayCheckMessage() {
                                                                        check_message.style.display = "inline";

                                                                    }
                                                                </script>
                                                                <% }
                                                                   else
                                                                   {%>
                                                                <%: Model.IsChecked %>
                                                                <% } %>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="display-label">
                                                                Đã kích hoạt</div>
                                                        </td>
                                                        <td>
                                                            <div class="display-field">
                                                                <% if (roles != null && roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
                                                                   {%>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <% using (Ajax.BeginForm("Active/" + Model.ItemID, new AjaxOptions { UpdateTargetId = "activemessage", OnSuccess = "displayActiveMessage" }))
                                                                               { %>
                                                                                <input type="checkbox" name="active" value="true" onclick = "$(this.form).submit();" <% if (Model.IsActive) { %> checked = "checked" <% } %> />
                                                                            <% } %>
                                                                        </td>
                                                                        <td>
                                                                            <span id="activemessage" style="display: none; color: Green; font-weight: bold;">
                                                                            </span>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                
                                                                <script>
                                                                    var active_message = document.getElementById("activemessage");
                                                                    function displayActiveMessage() {
                                                                        active_message.style.display = "inline";

                                                                    }
                                                                </script>
                                                                <% }
                                                                   else
                                                                   {%>
                                                                <%: Model.SubCategoryID%>
                                                                <% } %>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="display-label">
                                                                Ngày tạo</div>
                                                        </td>
                                                        <td>
                                                            <div class="display-field">
                                                                <%: String.Format("{0:g}", Model.CreateDate) %></div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="display-label">
                                                                Ảnh sản phẩm</div>
                                                        </td>
                                                        <td>
                                                            <div class="display-field">
                                                                <img id="det_img_1618606641" src='http://<%: Request.Url.Authority %>/item/getImage?ItemId=<%: Model.ItemID %>'
                                                                    width="300" alt="" border="0"></div>
                                                        </td>
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
    </div>
</asp:Content>
