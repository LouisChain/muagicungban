<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ForgetPassword
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
                                            <div class="box_product_detail_padding" style="padding: 30px 30px 30px 50px; font-size: 16px;">
                                                <h2>
                                                    Nhập địa chỉ email</h2>
                                                <div style="color:Red;">
                                                    <% if (TempData["error-message"] != null)
                                                       { %>
                                                        <%: TempData["error-message"]%><br />
                                                    <% } %>
                                                </div>
                                                <% using(Html.BeginForm()) { %>
                                                <div style="font-size:18px;padding-top:15px;">
                                                    <label>Địa chỉ email</label>
                                                    <input type="text" name="email" />
                                                    <input type="submit" value="Phục hồi mật khẩu" />
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
            </div>
        </div>
    </div>

</asp:Content>
