<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<muagicungban.Entities.ItemPlace>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Vị trí đăng sản phẩm
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        var pDH = new Array();
        var pDS = new Array();

        $(document).ready(function () {
        <% foreach (var place in (List<muagicungban.Entities.ShowablePlace>)ViewData["showable_place"])  { %>
            $("#startDate<%: place.PlaceName %>").datepicker(
                {
                    monthNames: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
                    monthNamesShort: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'dd/mm/yy'
                }
            );
        <% } %>
        });
    </script>
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
                                            <% if (TempData["error_message"] != null)
                                               { %>
                                                <%: TempData["error_message"]%>
                                            <% }
                                               else
                                               { %>
                                                <h1>Chọn vị trí đăng sản phẩm</h1>
                                                <div>
                                                    <table style="text-align:right;">
                                                        <tr style="background-color: black;color: white;text-align: center;height: 30px;">
                                                            <th>Vị trí</th>
                                                            <th style="width:25%;">Mô tả</th>
                                                            <th>Giá (VNĐ/Ngày)</th>
                                                            <th colspan="2">Ngày bắt đầu</th>
                                                            <th>Số ngày đăng</th>
                                                            <th>#</th>
                                                            
                                                        </tr>
                                                        <% int j = 0;
                                                           foreach (var place in (List<muagicungban.Entities.ShowablePlace>)ViewData["showable_place"])
                                                           { j++;%>
                                                            <tr <% if (j % 2 == 0) { %> style="background-color: #DAEBDC;" <% } %>>
                                                                <td><%: place.PlaceName%></td>
                                                                <td><%: place.PlaceDescription%></td>
                                                                <td align="center"><%: place.PricePerDay.ToString("#,###")%></td>
                                                                <td>
                                                                    <% using (Ajax.BeginForm("GetPostingDate", new AjaxOptions { UpdateTargetId = "startDate1" + place.PlaceName, OnSuccess = "getPostingDate" + place.PlaceName }))
                                                                       { %>
                                                                        <input type="hidden" name="id" value="<%: place.PlaceName%>" />
                                                                        <input style="float:left;"  type="button" value=" * " title="Ngày gần nhất có thể đăng" onclick="$(this.form).submit();" />
                                                                    <% } %>
                                                                </td>
                                                                <% using (Ajax.BeginForm("SavePostingPlace", new AjaxOptions { OnSuccess = "changeDelAction"+ place.PlaceName }))
                                                                   { %>
                                                                <td>
                                                                    <div style="display:none;" id="startDate1<%: place.PlaceName%>" ></div>
                                                                    <input style="float:left;width:80%;" type="text" id="startDate<%: place.PlaceName%>" name="start" />
                                                                    <script type="text/javascript">
                                                                        function getPostingDate<%: place.PlaceName%>() {
                                                                            document.getElementById("startDate<%: place.PlaceName%>").value = document.getElementById("startDate1<%: place.PlaceName%>").innerHTML;
                                                                        };

                                                                        function changeDelAction<%: place.PlaceName%>() {
                                                                            document.getElementById("choose<%: place.PlaceName %>").style.display = "none";
                                                                            document.getElementById("unchoose<%: place.PlaceName %>").style.display = "block";
                                                                        }

                                                                        function changeAddAction<%: place.PlaceName%>() {
                                                                            document.getElementById("choose<%: place.PlaceName %>").style.display = "block";
                                                                            document.getElementById("unchoose<%: place.PlaceName %>").style.display = "none";
                                                                        }

                                                                    </script>
                                                                    <input type="hidden" name="placename" value="<%: place.PlaceName %>" />
                                                                    <input type="hidden" name="itemid" value="<%: ViewData["ItemID"] %>" />
                                                                </td>
                                                                <td>
                                                                    <input style="text-align:center;" type="text" id="days<%: place.PlaceName %>" name="days" value = "3" />
                                                                    
                                                                </td>
                                                                <td align="left"><input id="choose<%: place.PlaceName %>" type="button" value="Đặt" name="choose" onclick="$(this.form).submit();" />
                                                                <% } %>
                                                                <% using (Ajax.BeginForm("UnSavePostingPlace", new AjaxOptions { OnSuccess = "changeAddAction" + place.PlaceName }))
                                                                   {%>
                                                                    <input type="hidden" name="del_placename" value="<%: place.PlaceName %>" />
                                                                    <input type="hidden" name="del_itemid" value="<%: ViewData["ItemID"] %>" />
                                                                    <input style="display:none;" id="unchoose<%: place.PlaceName %>" type="button" value="Hủy" onclick = "$(this.form).submit();" />
                                                                <% } %>
                                                                </td>
                                                            </tr>
                                                        <% } %>
                                                    </table>
                                                </div>
                                                <% } %>
                                                <div style="padding-top:20px; text-align:center;">
                                                    <a href="http://<%: Request.Url.Authority %>/item/shippingsupport?ItemId=<%: ViewData["ItemID"] %>"><input type="button" value="<<- Vị trí đăng sản phẩm" /></a>
                                                    <a href="http://<%: Request.Url.Authority %>/item/ConfirmAndPayment/<%: ViewData["ItemID"] %>" /><input type="button" value="Đồng ý và thanh toán ->>" /></a>
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
