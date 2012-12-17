<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<muagicungban.Entities.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Thêm người dùng
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
        $("#Birthday").datepicker(
            {
                monthNames: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
                monthNamesShort: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
                changeMonth: true,
                changeYear: true,
                dateFormat: 'mm/dd/yy',
                yearRange: '1960:2000'
            }
        );
    });
    </script>
    <div id="content">
        <div class="margin">
            <div class="content-helper clear">
                <div class="central-column">
                    <div class="central-content">
                        <div class="mainbox-container">
                            <div class="mainbox-body">
                                <div id="content_general">
                                    <h1>
                                        Thêm người dùng mới</h1>
                                    <% using (Html.BeginForm())
                                       {%>
                                    <%: Html.ValidationSummary(true) %>
                                    <fieldset>
                                        <legend>Thông tin người dùng</legend>
                                        <div class="form-field">
                                            <label>
                                                Tên đăng nhập</label>
                                            <%: Html.TextBoxFor(model => model.Username) %>
                                            <%: Html.ValidationMessageFor(model => model.Username) %>
                                            <% if (TempData["username-error"] != null)
                                                   { %>
                                                <span class="field-validation-error" style="color:Red;">
                                                    <%: TempData["username-error"]%></span>
                                                <% } %>
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
                                            <% if (TempData["email-error"] != null)
                                                   { %>
                                                <span class="field-validation-error" style="color:Red;">
                                                    <%: TempData["email-error"]%></span>
                                                <% } %>
                                        </div>
                                        <div class="form-field">
                                            <label>
                                                Số điện thoại</label>
                                            <%: Html.TextBoxFor(model => model.Phone) %>
                                            <%: Html.ValidationMessageFor(model => model.Phone) %>
                                            <% if (TempData["phone-error"] != null)
                                                   { %>
                                                <span class="field-validation-error" style="color:Red;">
                                                    <%: TempData["phone-error"]%></span>
                                                <% } %>
                                        </div>
                                        <div class="form-field">
                                            <label>
                                                Địa chỉ</label>
                                            <%: Html.TextBoxFor(model => model.Address) %>
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
                                        <% foreach (var item in (List<muagicungban.Entities.Role>)ViewData["roleChkBox"])
                                           { %>
                                        <input type="checkbox" name="role" value="<%: item.RoleID %>" <% if (item.RoleName == "Seller" || item.RoleName == "Buyer" || item.RoleName == "Bidder") { %> checked="checked" <% } %> /><%: item.RoleName %>
                                        <% } %>
                                    </fieldset>
                                    <p>
                                        <span class="button-submit"><input type="submit" value="Thêm mới" /></span>
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
