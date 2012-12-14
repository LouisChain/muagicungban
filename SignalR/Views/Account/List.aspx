<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<muagicungban.Models.Account>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% foreach (var acc in Model)
       { %>
        <% Html.RenderPartial("AccountSummary", acc);%>
    <% } %>
    <p><%: Html.ActionLink("Add new account", "Create") %></p>

</asp:Content>
