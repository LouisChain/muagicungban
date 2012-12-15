<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Recharge
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
                                                    Nạp tiền vào tài khoản</h2>
                                                <div style="color:Red;">
                                                    <% if (TempData["error-message"] != null)
                                                       { %>
                                                        <%: TempData["error-message"]%><br />
                                                        Thực hiện giao dịch mới sẽ hủy giao dịch cũ<br />
                                                        Hoặc click <span style="color:Blue;"><%: Html.ActionLink("vào đây", "RechargePayment", new { id = (long)TempData["id"] })%></span> để thanh toán giao dịch trước đó
                                                    <% } %>
                                                </div>
                                                <% using(Html.BeginForm()) { %>
                                                <div style="font-size:18px;padding-top:15px;">
                                                    <label>Số tiền</label>
                                                    <input type="text" name="money" />
                                                    <input type="submit" value="Thanh toán" />
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
