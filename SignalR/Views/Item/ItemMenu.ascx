<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%  var Profile = (muagicungban.Entities.User)ViewContext.HttpContext.Session["Profile"];
    var Roles = (List<muagicungban.Entities.UserRoles>)ViewContext.HttpContext.Session["Roles"]; %>

<div class="nav_2" style="height: 96px;">
    <ul>
<% if (ViewContext.HttpContext.User.Identity.IsAuthenticated && Roles != null)
   { %>

        <div class="nu bor_bt" style="font-size: 16px;padding-left:5px;">
            <% if (Roles.Any(r => r.Role.RoleName == "Seller"))
               { %>
            <li style="font-size: 14px; padding: 6px 3px 4px; min-width: auto;"><a href="http://<%: Request.Url.Authority %>/item/create"
                style="width: 190px; min-width: 0; padding: 2px 2px; <% if ((string)ViewData["type"] =="sell") { %>background-color: #1ECA17;
                <% } %>">ĐĂNG SẢN PHẨM MỚI <span class="total"></span></a></li>
            <% } %>

            <% if (Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
               { %>
            <li style="font-size: 14px; padding: 6px 3px 4px; min-width: auto;"><a href="http://<%: Request.Url.Authority %>/item/checking"
                style="width: 190px; min-width: 0; padding: 2px 2px; <% if ((string)ViewData["type"] =="buy") { %>background-color: #1ECA17;
                <% } %>">SẢN PHẨM CHỜ DUYỆT <span class="total">(<%: (int)ViewData["Uncheck"]%>)</span></a></li>
            <li style="font-size: 14px; padding: 6px 3px 4px; min-width: auto;"><a href="http://<%: Request.Url.Authority %>/item/list/all?page=1"
                style="width: 200px; min-width: 0; padding: 2px 2px; <% if ((string)ViewData["type"] =="buy") { %>background-color: #1ECA17;
                <% } %>">DANH SÁCH SẢN PHẨM <span class="total">(<%: (int)ViewData["List"]%>)</span></a></li>
            <li style="font-size: 14px; padding: 6px 3px 4px; min-width: auto;"><a href="http://<%: Request.Url.Authority %>/subcategory/list"
                style="width: 190px; min-width: 0; padding: 2px 2px; <% if ((string)ViewData["type"] =="buy") { %>background-color: #1ECA17;
                <% } %>">QUẢN LÝ DANH MỤC <span class="total">(<%: ((List<muagicungban.Models.SubCategory>)ViewData["categories"]).Count%>)</span></a></li>

            <% }
               else
               {
                %>
            <li style="font-size: 14px; padding: 6px 3px 4px; min-width: auto;"><a href="http://<%: Request.Url.Authority %>/item/list/all?page=1"
                style="width: 190px; min-width: 0; padding: 2px 2px; <% if ((string)ViewData["type"] =="buy") { %>background-color: #1ECA17;
                <% } %>">SẢN PHẨM CUẢ TÔI <span class="total"></span></a></li>
            <% } %>
            <div class="cb">
            </div>
        </div>
        <div class="nu bg bor_bt" style="height:31px;">
            <li style="width: 75px; min-width: 0;">
                <p>
                    PHIÊN ĐẤU</p>
            </li>
            <div id="status" <% if ((string)ViewData["type"] == "all") { %> style="display: none;"
                <% } %>>
                <li style="font-size: 12px; padding: 6px 3px 4px; min-width: auto;"><a href="http://<%: Request.Url.Authority %>/item/session/win"
                    style="width: 120px; min-width: 0; padding: 2px 2px; <% if ((string)ViewData["status"] =="1") { %>background-color: #1ECA17;
                    <% } %>">ĐÃ CHIẾN THẮNG
                    <span class="total">(<%: (int)ViewContext.HttpContext.Session["Win"] %>)</span></a></li>
                <li style="font-size: 12px; padding: 6px 3px 4px; min-width: auto;"><a href="http://<%: Request.Url.Authority %>/item/session/join"
                    style="width: 120px; min-width: 0; padding: 2px 2px; <% if ((string)ViewData["status"] =="2") { %>background-color: #1ECA17;
                    <% } %>">CÓ THAM GIA <span class="total">(<%: (int)ViewContext.HttpContext.Session["Join"] %>)</span></a></li>

            </div>
            <div class="cb">
            </div>
        </div>
<% } %>
        <div class="nu bg bor_bt" style="padding-left:5px;">
            <li style="font-size: 14px; padding: 6px 3px 4px; min-width: auto;"><a href="http://<%: Request.Url.Authority %>/item/view/ended?page=1"
                style="width: 190px; min-width: 0; padding: 2px 2px; <% if ((string)ViewData["type"] =="buy") { %>background-color: #1ECA17;
                <% } %>">ĐÃ KẾT THÚC <span class="total">(<%: (int)ViewData["EndedNums"]%>)</span></a></li>
            <li style="font-size: 14px; padding: 6px 3px 4px; min-width: auto;"><a href="http://<%: Request.Url.Authority %>/item/view/selling?page=1"
                style="width: 190px; min-width: 0; padding: 2px 2px; <% if ((string)ViewData["type"] =="buy") { %>background-color: #1ECA17;
                <% } %>">ĐANG DIỄN RA <span class="total">(<%: (int)ViewData["SellingNums"]%>)</span></a></li>
            <li style="font-size: 14px; padding: 6px 3px 4px; min-width: auto;"><a href="http://<%: Request.Url.Authority %>/item/view/future?page=1"
                style="width: 190px; min-width: 0; padding: 2px 2px; <% if ((string)ViewData["type"] =="buy") { %>background-color: #1ECA17;
                <% } %>">SẮP MỞ <span class="total">(<%: (int)ViewData["FutureNums"]%>)</span></a></li>
            <div class="cb"></div>
        </div>
    </ul>
    <div class="cb">
    </div>
</div>