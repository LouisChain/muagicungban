<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<muagicungban.Entities.AccountRecharge>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    RechargePayment
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
                                                <div style="font-size:18px;padding-top:15px;">
                                                    <table>
                                                        <tr>
                                                            <td style="width:150px;">Mã giao dịch</td>
                                                            <td style="padding-right:15px;font-weight:bold;">#<%: Model.RechargeID %></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Tài khoản</td>
                                                            <td  style="padding-right:15px;font-weight:bold;"><%: Model.Username %></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Số tiền cần nạp</td>
                                                            <td style="padding-right:15px;font-weight:bold;"><%: Model.Price.ToString("#,### VND") %></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">&nbsp</td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td><a href="<%: ViewData["nganluongURL"]%>"><image border="0" src="https://www.nganluong.vn/data/images/buttons/11.gif"></image></a></td>
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
    </div>
</asp:Content>
