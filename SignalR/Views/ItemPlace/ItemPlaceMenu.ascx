<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%  var Profile = (muagicungban.Entities.User)ViewContext.HttpContext.Session["Profile"];
    var Roles = (List<muagicungban.Entities.UserRoles>)ViewContext.HttpContext.Session["Roles"]; %>

<div class="nav_2" style="height: 40px;">
    <ul>
<% if (ViewContext.HttpContext.User.Identity.IsAuthenticated && Roles != null)
   { %>

        <div class="nu bor_bt" style="font-size: 16px;padding-left:5px;">
            

            <% if (Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
               { %>
            <li style="font-size: 14px; padding: 6px 3px 4px; min-width: auto;"><a href="http://<%: Request.Url.Authority %>/itemplace/index/past"
                style="width: 190px; min-width: 0; padding: 2px 2px; <% if ((string)ViewData["type"] =="buy") { %>background-color: #1ECA17;
                <% } %>">ĐÃ QUA <span class="total"></span></a></li>
            <li style="font-size: 14px; padding: 6px 3px 4px; min-width: auto;"><a href="http://<%: Request.Url.Authority %>/itemplace/index/current"
                style="width: 200px; min-width: 0; padding: 2px 2px; <% if ((string)ViewData["type"] =="buy") { %>background-color: #1ECA17;
                <% } %>">BÂY GIỜ <span class="total"></span></a></li>
            <li style="font-size: 14px; padding: 6px 3px 4px; min-width: auto;"><a href="http://<%: Request.Url.Authority %>/itemplace/index/future"
                style="width: 190px; min-width: 0; padding: 2px 2px; <% if ((string)ViewData["type"] =="buy") { %>background-color: #1ECA17;
                <% } %>">CHƯA TỚI <span class="total"></span></a></li>

            <% }
   }%>
    </ul>
    <div class="cb">
    </div>
</div>