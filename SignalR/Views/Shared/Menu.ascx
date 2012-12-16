<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%  var Profile = (muagicungban.Entities.User)ViewContext.HttpContext.Session["Profile"];
    var Roles = (List<muagicungban.Entities.UserRoles>)ViewContext.HttpContext.Session["Roles"]; %>

<ul class="top-menu dropdown">
            
<% if (ViewContext.HttpContext.User.Identity.IsAuthenticated && Roles != null)
   { %>
    <% if (Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
       { %>
            <li class="first-level" style="color:#FA6D18;"><span><a href="http://<%: Request.Url.Authority %>/user/list?page=1">Người dùng</a></span></li>
            <li class="first-level"><span><a>&nbsp</a></span></li>
    <% } %>
            <li class="first-level" style="color:#FA6D18;"><span><a href="http://<%: Request.Url.Authority %>/item/">Sản phẩm</a></span></li>
            <li class="first-level" style="color:#FA6D18;"><span><a>&nbsp</a></span></li>
            <li class="first-level" style="color:#FA6D18;"><span><a href="http://<%: Request.Url.Authority %>/order/?type=all">Đơn hàng</a></span></li>
            <li class="first-level" style="color:#FA6D18;"><span><a>&nbsp</a></span></li>
<% } %>
    <li class="first-level" style="color:#FA6D18;"><span>
        <a href="http://<%: Request.Url.Authority %>/showableplace/<% if (ViewContext.HttpContext.User.Identity.IsAuthenticated && Roles != null)
   { %>
    <% if (Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
       { %>list<% }} %>">Bảng giá đăng tin</a></span>
    </li>
</ul>

<div class="float-right" id="sign_io">
    <ul class="top-menu dropdown">

<% if (ViewContext.HttpContext.User.Identity.IsAuthenticated && Roles != null)
   { %>
        <li class="first-level">
            <span><a><%:Profile.Name.ToString() %> <% if (Roles.Any(r => r.Role.RoleName == "Seller"))
                                                      { %>(<%: Profile.Money.ToString("#,### VND")%>) <% } %></a></span>
            <ul class="dropdown-vertical-rtl">
            <li><a href="http://<%: Request.Url.Authority %>/user/profile" rel="nofollow" class="underlined">Thông tin cá nhân</a></li>
            <% if (Roles.Any(r => r.Role.RoleName == "Seller" || r.Role.RoleName == "Buyer" || r.Role.RoleName == "Bidder"))
               { %>
            <li><a href="http://<%: Request.Url.Authority %>/user/paymenthistory" rel="nofollow">Lịch sử thanh toán</a></li>
            <% } %>
            <% if (Roles.Any(r => r.Role.RoleName == "Seller"))
               { %>
            <li><a href="http://<%: Request.Url.Authority %>/user/recharge" rel="nofollow">Nạp tài khoản</a></li>
            <% } %>
            <!--li><a href="/diem-thuong" rel="nofollow">Điểm tích lũy:&nbsp;<strong>0</strong></a></li-->
            </ul>
        </li>
        <li class="first-level"><span>&nbsp;|&nbsp;</span></li>
        <li class="first-level"><span><a href="http://<%: Request.Url.Authority %>/user/logout">Đăng xuất</a></span></li>
<% } 
   else { %>
        <li class="first-level"><span id="show-login"><a id="sw_login" class="cm-combination" href="http://<%: Request.Url.Authority %>/user/logon" >Đăng nhập</a></span></li>
        <li class="first-level"><span>&nbsp;|&nbsp;</span></li>
        <li class="first-level"><span><a href="http://<%: Request.Url.Authority %>/user/Register" rel="nofollow">Đăng ký</a></span></li>

<% } %>
    </ul>
</div>