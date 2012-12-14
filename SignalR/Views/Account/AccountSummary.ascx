<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<muagicungban.Models.Account>" %>

        <div class="display-label">Account</div>
        <div class="display-field"><%: Html.ActionLink(Model.ID, "Edit", new { id=Model.ID }) %></div>
        <div class="display-label">Balance</div>
        <div class="display-field"><%: Model.Balance.ToString("#,### VND") %></div>
        <% using (Html.BeginForm("Delete", "Account"))
           { %>
            <%: Html.Hidden("ID", Model.ID)%>
            <button type="submit">Delete</button>
        <% } %>
    <p>
    </p>


