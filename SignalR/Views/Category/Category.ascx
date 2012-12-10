<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<muagicungban.Models.Category>" %>

    <fieldset>
        <legend>Fields</legend>
        <div class="display-label">Name</div>
        <div class="display-field"><%: Html.ActionLink(Model.CategoryName, "Edit", new {id = Model.CategoryID}) %></div>
        <% using (Html.BeginForm("Delete","Category")) 
           { %>
            <%: Html.Hidden("ID", Model.CategoryID) %>
            <button type="submit">Delete</button>
        <% } %>
    </fieldset>


