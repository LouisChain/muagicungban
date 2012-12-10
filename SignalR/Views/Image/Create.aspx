<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<muagicungban.Models.ItemImage>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>

    <% using (Html.BeginForm("Create", "Image", FormMethod.Post, new { enctype = "multipart/form-data" }))
       {%>
        <%: Html.ValidationSummary(true)%>

        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.ItemID)%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.ItemID)%>
                <%: Html.ValidationMessageFor(model => model.ItemID)%>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.ImageName)%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.ImageName)%>
                <%: Html.ValidationMessageFor(model => model.ImageName)%>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.MimeType)%>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.MimeType)%>
                <%: Html.ValidationMessageFor(model => model.MimeType)%>
            </div>
            <div class="editor-label">Image</div>
            <div class="editor-field">
                <div>Upload new image: <input type="file" name="Image" /></div>
            </div>
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

