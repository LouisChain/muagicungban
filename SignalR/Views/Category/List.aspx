<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<muagicungban.Models.Category>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% foreach (var item in Model)
       { %>
        <% Html.RenderPartial("Category", item); %>
    <% } %>
    <p><%: Html.ActionLink("Add new", "Create") %></p>

</asp:Content>
