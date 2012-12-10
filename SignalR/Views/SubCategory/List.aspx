<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<muagicungban.Models.SubCategory>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr><th>Main Category</th><th>Sub Category</th><th>Action</th></tr>
        
            <% foreach (var item in Model)
               { %>
               <tr>
                <td><%: item.Category.CategoryName%></td>
                <td><%: Html.ActionLink(item.Name, "Edit", new {ID = item.ID}) %></td>
                <td><% using (Html.BeginForm("Delete", "SubCategory")) %>
                    <% { %>
                        <%: Html.Hidden("ID", item.ID)%>
                        <button type="submit">Delete</button>
                    <% } %>
                </td>
                </tr>
            <% } %>
    </table>
    <p><%: Html.ActionLink("Add new", "Create") %></p>
</asp:Content>
