<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<muagicungban.Models.Register>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Đăng ký
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
                dateFormat: 'dd/mm/yy',
                yearRange: '1950:2000'
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
                                        Thông tin cá nhân</h1>
                                    <p class="mandatory-fields">
                                        Thông tin có <span class="required">*</span> là bắt buộc nhập, cần phải nhập.</p>
                                    <% Html.EnableClientValidation(); %>
                                    <% using (Html.BeginForm())
                                       { %>

                                    <div class="border">
                                        <div class="subheaders-group">
                                            <br>
                                            <h2 class="subheader">
                                                Thông tin tài khoản
                                            </h2>
                                            <div class="form-field">
                                                <label for="username" class="cm-required">
                                                    Tên đăng nhập</label>
                                                <%: Html.TextBoxFor(model => model.Username, new { style = "width:250px; height:20px;" })%>
                                                <%: Html.ValidationMessageFor(model => model.Username)%>
                                                <% if (TempData["username-error"] != null)
                                                   { %>
                                                <span class="field-validation-error">
                                                    <%: TempData["username-error"]%></span>
                                                <% } %>
                                            </div>
                                            <div class="form-field">
                                                <label for="email" class="cm-required cm-email cm-trim">
                                                    Email:</label>
                                                <%: Html.TextBoxFor(model => model.Email, new { style = "width:250px; height:20px;" })%>
                                                <%: Html.ValidationMessageFor(model => model.Email)%>
                                            </div>
                                            <div class="form-field">
                                                <label for="password1" class="cm-required cm-password">
                                                    Mật khẩu:</label>
                                                <%: Html.PasswordFor(model => model.Password, new { style = "width:250px; height:20px;" })%>
                                                <%: Html.ValidationMessageFor(model => model.Password)%>
                                            </div>
                                            <div class="form-field">
                                                <label for="password2" class="cm-required cm-password">
                                                    Nhập lại mật khẩu:</label>
                                                <%: Html.PasswordFor(model => model.ConfirmPassword, new { style = "width:250px; height:20px;" })%>
                                                <%: Html.ValidationMessageFor(model => model.ConfirmPassword)%>
                                                <% if (TempData["PasswordNotMatch"] != null)
                                                   { %>
                                                <span class="field-validation-error">
                                                    <%: TempData["PasswordNotMatch"]%></span>
                                                <% } %>
                                            </div>
                                            <div class="form-field">
                                                <label for="birthday" class="cm-required">
                                                    Ngày sinh:</label>
                                                <%: Html.EditorFor(model => model.Birthday)%>
                                                <%: Html.ValidationMessageFor(model => model.Birthday)%>
                                            </div>
                                            <br>
                                            <h2 class="subheader">
                                                Thông tin liên hệ
                                            </h2>
                                            <div class="form-field">
                                                <label for="elm_15" class="cm-profile-field cm-required ">
                                                    Họ tên:</label>
                                                <%: Html.TextBoxFor(model => model.Name, new { style = "width:250px; height:20px;" })%>
                                                <%: Html.ValidationMessageFor(model => model.Name)%>
                                            </div>
                                            
                                            <div class="form-field">
                                                <label for="elm_19" class="cm-profile-field cm-required ">
                                                    Địa chỉ:</label>
                                                <%: Html.TextBoxFor(model => model.Address, new { style = "width:250px; height:20px;" })%>
                                                <%: Html.ValidationMessageFor(model => model.Address) %>
                                            </div>

                                            <div class="form-field">
                                                <label for="elm_31" class="cm-profile-field cm-required ">
                                                    Điện thoại:</label>
                                                <%: Html.TextBoxFor(model => model.Phone, new { style = "width:250px; height:20px;" })%>
                                                <%: Html.ValidationMessageFor(model => model.Phone)%>
                                            </div>
                                            <div class="form-field">
                                                <p>
                                                    <img src="/user/ShowCaptchaImage" /></p>
                                            </div>
                                            <div class="form-field">
                                                <label for="elm_41" class="cm-profile-field cm-required ">
                                                    Mã xác thực:</label>
                                                <%: Html.TextBoxFor(model => model.Captcha)%>
                                                <%: Html.ValidationMessageFor(model => model.Captcha)%>
                                                <% if (TempData["WrongCaptcha"] != null)
                                                   { %>
                                                <span class="field-validation-error">
                                                    <%: TempData["WrongCaptcha"]%></span>
                                                <%} %>
                                            </div>
                                            <div class="buttons-container center">
                                                <span class="button-submit">
                                                    <input type="submit" value="Đăng ký"></span>
                                            </div>
                                        </div>
                                    </div>
                                    <% } %>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
