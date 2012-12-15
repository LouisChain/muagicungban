<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<muagicungban.Entities.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
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
                                    <h1>
                                        Thay đổi thông tin</h1>
                                    <% Html.EnableClientValidation(); %>
                                    <% using (Html.BeginForm())
                                       {%>
                                    <%: Html.ValidationSummary(true) %>
                                    <fieldset>
                                        <legend>Thông tin người dùng</legend>
                                        <div class="form-field">
                                            <label>
                                                Tên đăng nhập</label>
                                            <%: Html.DisplayFor(model => model.Username) %>
                                        </div>
                                        <div class="form-field">
                                            <label>
                                                Mật khẩu</label>
                                            <%: Html.PasswordFor(model => model.Password) %>
                                            <%: Html.ValidationMessageFor(model => model.Password) %>
                                        </div>
                                        <div class="form-field">
                                            <label>
                                                Họ và tên</label>
                                            <%: Html.TextBoxFor(model => model.Name) %>
                                            <%: Html.ValidationMessageFor(model => model.Name) %>
                                        </div>
                                        <div class="form-field">
                                            <label>
                                                Email</label>
                                            <%: Html.TextBoxFor(model => model.Email) %>
                                            <%: Html.ValidationMessageFor(model => model.Email) %>
                                        </div>
                                        <div class="form-field">
                                            <label>
                                                Số điện thoại</label>
                                            <%: Html.TextBoxFor(model => model.Phone) %>
                                            <%: Html.ValidationMessageFor(model => model.Phone) %>
                                        </div>
                                        <div class="form-field">
                                            <label>
                                                Địa chỉ</label>
                                            <%: Html.EditorFor(model => model.Address) %>
                                            <%: Html.ValidationMessageFor(model => model.Address) %>
                                        </div>
                                        <div class="form-field">
                                            <label>
                                                Ngày sinh</label>
                                            <%: Html.EditorFor(model => model.Birthday) %>
                                            <%: Html.ValidationMessageFor(model => model.Birthday) %>
                                        </div>
                                    </fieldset>
                                    <fieldset>
                                        <legend>Quyền hạn</legend>
                                        <% List<muagicungban.Entities.UserRoles> _checkedRoles = (List<muagicungban.Entities.UserRoles>)ViewData["roleChecked"];

                                           foreach (var item in (List<muagicungban.Entities.Role>)ViewData["roleChkBox"])
                                           { %>
                                        <input type="checkbox" <% if (_checkedRoles.Any(c => c.RoleID == item.RoleID)) { %>
                                            checked="checked" <% } %> name="role" value="<%: item.RoleID %>" /><%: item.RoleName %>
                                        <% } %>
                                    </fieldset>
                                                                        <p>
                                        <span class="button-submit"><input type="submit" value="Lưu" /></span>
                                    </p>
                                    <% } %>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
