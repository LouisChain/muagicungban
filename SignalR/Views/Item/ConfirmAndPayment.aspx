<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<muagicungban.Entities.Item>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Vị trí đăng sản phẩm
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="helper-container">
        <div id="container" class="container-left">
            <div id="content">
                <div class="margin">
                    <div class="content-helper clear">
                        <div class="central-column">
                            <div class="mainbox-container">
                                <div class="mainbox-body">
                                    <div class="central-content">

                                        <div class="box_product_detail">
                                            <div class="box_product_detail_padding" style="padding: 30px 30px 30px 50px; font-size: 16px;">
                                            <h1>THÔNG TIN VỀ SẢN PHẨM</h1>
                                            <div class="box_info_product_detail" style="width:50%;">
                                            
                                            <div>Tên sản phẩm</div>
                                            <div style="font-weight:bolder;padding-bottom:15px;"><%: Html.DisplayFor(model => model.Title)%></div>
                                            <div >Mô tả</div>
                                            <div style="font-weight:bolder;padding-bottom:15px;"><%: Html.DisplayFor(model => model.Description)%></div>
                                            <% if (Model.IsAuction)
                                               { %>
                                            <div>Giá khởi điểm</div>
                                            <div style="font-weight:bolder;padding-bottom:15px;"><%: Model.StartPrice.ToString("#,### VNĐ") %></div>
                                            <% } %>
                                            <div>Giá tối đa</div>
                                            <div style="font-weight:bolder;padding-bottom:15px;"><%: Model.MaxPrice.ToString("#,### VNĐ") %></div>
                                            <div>Ngày bắt đầu</div>
                                            <div style="font-weight:bolder;padding-bottom:15px;"><%: Html.DisplayFor(model => model.StartDate)%></div>
                                            <div>Ngày kết thúc</div>
                                            <div style="font-weight:bolder;padding-bottom:15px;"><%: Html.DisplayFor(model => model.EndDate)%></div>
                                            <div>Phương thức bán hàng</div>
                                            <div style="font-weight:bolder;padding-bottom:15px;"><% if (Model.IsAuction)
                                                                                { %>Đấu giá<% }
                                                                                else
                                                                                { %>Bán<% } %></div>
                                            </div>
                                            </div>
                                             <div class="box_img_product_detail" style="position:absolute;right:20%;">
                                                    <div class="box_stand_star">
                                                        <a class="cr" href="">
                                                            ẢNH SẢN PHẨM</a>
                                                        <div class="cb">
                                                        </div>
                                                    </div>
                                                    <div class="pic_product_detail">
                                                        <span class="pic_product_detail">
                                                            <div class="slider-wrapper theme-default">
                                                                <img id="det_img_1618606641" src='http://<%: Request.Url.Authority %>/item/getImage?ItemId=<%: Model.ItemID %>'
                                                                    width="300" alt="" border="0">
                                                            </div>
                                                        </span>
                                                    </div>
                                                </div>
                                        </div>

                                        <div class="box_product_detail box_product_feature" >
                                            <div class="box_product_feature_inner">
                                            </div>
                                            <div class="cb">
                                            </div>
                                            <div style="padding: 30px 30px 30px 50px; font-size:16px;">
                                            <h1>
                                                    THÔNG TIN GIAO HÀNG</h1>

                                                <table >
                                                    <tr style="background-color: #070706;color:white;text-align:center;">
                                                        <th style="width:100px;">
                                                            Quốc gia
                                                        </th>
                                                        <th style="width:150px;">
                                                            Tỉnh thành
                                                        </th>
                                                        <th style="width:200px;">
                                                            Quận/Huyện
                                                        </th>
                                                        <th style="width:150px;">
                                                            Chi phí (VNĐ)
                                                        </th>
                                                        <th style="width:200px;">
                                                            Mô tả
                                                        </th>
                                                        <th style="width:100px;">
                                                            #
                                                        </th>
                                                    </tr>
                                                    <% int j = 0;
                                                       foreach (var shipment in Model.Shipments)
                                                       { j++; %>
                                                    <tr <% if (j % 2 == 0) { %> style="background-color: #CBEBBD;" <% } %>>
                                                        <td align="center">
                                                            <%: shipment.AreaPart.CountryArea.Country.Name %>
                                                        </td>
                                                        <td align="center">
                                                            <%: shipment.AreaPart.CountryArea.AreaName %>
                                                        </td>
                                                        <td align="center">
                                                            <%: shipment.AreaPart.Name %>
                                                        </td>
                                                        <td align="center">
                                                            <%: shipment.Price.ToString("#,###") %>
                                                        </td>
                                                        <td align="center">
                                                            <%: shipment.Description %>
                                                        </td>
                                                        <td align="center">
                                                            
                                                        </td>
                                                    </tr>
                                                    <% } %>
                                                </table>
                                            </div>
                                        </div>

                                        <div class="box_product_detail">
                                            <div class="box_product_detail_padding" style="padding: 30px 30px 30px 50px; font-size: 16px;">

                                                <h1>THÔNG TIN VỀ VỊ TRÍ ĐĂNG SẢN PHẨM</h1>
                                                <div>
                                                    <table style="text-align:right; width:100%;">
                                                        <tr style="background-color: black;color: white;text-align: center;height: 30px;">
                                                            <th>Vị trí</th>
                                                            <th>Ngày bắt đầu</th>
                                                            <th>Ngày kết thúc</th>
                                                            <th>Giá (VNĐ/Ngày)</th>
                                                            <th>Thành tiền</th>
                                                       </tr>
                                                       <% 
                                                           decimal sum = 0;
                                                           foreach (var item in Model.ItemPlaces)
                                                          {
                                                              decimal money = item.Place.PricePerDay * (decimal)((item.EndDate - item.StartDate).TotalDays);
                                                              sum += money;
                                                       %>
                                                          <tr style="text-align:center;">
                                                                <td><%: item.PlaceName %></td>
                                                                <td><%: item.StartDate.ToString("dd/MM/yyyy hh:mm") %></td>
                                                                <td><%: item.EndDate.ToString("dd/MM/yyyy hh:mm") %></td>
                                                                <td><%: item.Place.PricePerDay.ToString("#,###") %></td>
                                                                <td><%: money.ToString("#,###") %></td>
                                                          </tr>
                                                       <% } %>
                                                       <tr style="font-weight:bolder;text-align:center;">
                                                                <td colspan="4" align="right">Tổng cộng</td><td><%: sum.ToString("#,### VNĐ") %></td>
                                                       </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>

                                        <div style="padding-top:20px; text-align:right; font-size:18px;">
                                            <a href="http://<%: Request.Url.Authority %>/item/PostingArea/<%: ViewData["ItemID"] %>"><input type="button" value="<<- Trở lại thông tin giao hàng" /></a>
                                            <a href="http://<%: Request.Url.Authority %>/item/AcceptPayment/<%: ViewData["ItemID"] %>" /><input type="button" value="Chấp nhập và thanh toán ->>" /></a>
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
