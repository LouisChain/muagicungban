<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<muagicungban.Entities.ShowablePlace>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
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
                                            <h1 style="padding-left: 30px;">
                                                BẢNG GIÁ ĐĂNG TIN</h1>
                                            <table style="font-size:18px;padding-top:30px;padding-left:30px;">
                                                <tr>
                                                    <th style="width:100px;">
                                                        Tên vị trí
                                                    </th>
                                                    <th style="width:200px;;">
                                                        Mô tả
                                                    </th>
                                                    <th>
                                                        Giá/Ngày
                                                    </th>
                                                </tr>
                                                <% foreach (var item in Model)
                                                   { %>
                                                <tr>
                                                    <td>
                                                        <%: item.PlaceName %>
                                                    </td>
                                                    <td>
                                                        <%: item.PlaceDescription %>
                                                    </td>
                                                    <td>
                                                        <%: item.PricePerDay.ToString("#,### VNĐ") %>
                                                    </td>
                                                </tr>
                                                <% } %>
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
