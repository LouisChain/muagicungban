<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<muagicungban.Models.LogOn>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    LogOn
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
                                    <h1 class="subheader">
                                        Thông tin đăng nhập</h1>
                                    <% Html.EnableClientValidation(); %>
                                    <% using (Html.BeginForm())
                                       {%>
                                    <%: Html.ValidationSummary(true) %>
                                    <div class="form-field">
                                        <label>
                                            Tên đăng nhập</label>
                                        <%: Html.TextBoxFor(model => model.Username, new { style = "width:250px;height:20px;" })%>
                                        <%: Html.ValidationMessageFor(model => model.Username) %>
                                    </div>
                                    <div class="form-field">
                                        <label>
                                            Mật khẩu</label>
                                        <%: Html.PasswordFor(model => model.Password, new { style = "width:250px;height:20px;" })%>
                                        <%: Html.ValidationMessageFor(model => model.Password) %>
                                    </div>
                                    <div class="form-field">
                                    <table style="font-size:16px;">
                                    <tr>
                                    <td style="width:140px;">
                                    <%: Html.CheckBoxFor(model => model.RememberMe) %>
                                        Ghi nhớ
                                    <%: Html.ValidationMessageFor(model => model.RememberMe) %>
                                    </td>
                                    <td style="color:Green;font-weight:bold;">
                                        <%: Html.ActionLink("Quên mật khẩu", "ForgetPassword") %>
                                    </td>
                                    </tr>
                                    </table>
                                    </div>
                                    <div class="form-field">

                                            <span class="button-submit">
                                                <input type="submit" value="Đăng nhập" /></span>
                                    </div>

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
