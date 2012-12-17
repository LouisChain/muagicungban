<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<muagicungban.Models.ItemPosting>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function showStuff(id) {
            document.getElementById(id).style.display = 'block';
        }

        function hideStuff(id) {
            document.getElementById(id).style.display = 'none';
        }

        $(document).ready(function () {
            $("#StartDate").datepicker(
                {
                    monthNames: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
                    monthNamesShort: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'mm/dd/yy'
                }
            );

            $("#EndDate").datepicker(
                {
                    monthNames: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
                    monthNamesShort: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'mm/dd/yy'
                }
            );
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
                                            <div class="box_product_detail_padding" style="padding: 30px 30px 30px 50px; font-size:16px;">
                                                <h2>
                                                    Đăng sản phẩm</h2>
                                                <% Html.EnableClientValidation(); %>
                                                <% using (Html.BeginForm("Create", "Item", FormMethod.Post, new { enctype = "multipart/form-data" }))
                                                   {%>
                                                <%: Html.ValidationSummary(true)%>

                                                    <div class="editor-label">
                                                        <%: Html.LabelFor(model => model.Title)%>
                                                    </div>
                                                    <div class="editor-field" style="width:400px;">
                                                        <%: Html.TextBoxFor(model => model.Title, new { style = "width:400px;" })%>
                                                        <%: Html.ValidationMessageFor(model => model.Title)%>
                                                    </div>
                                                    <div class="editor-label">
                                                        <%: Html.LabelFor(model => model.Description)%>
                                                    </div>
                                                    <div class="editor-field" style="width:400px; height:100px;">
                                                        <%: Html.TextAreaFor(model => model.Description, new { style = "width:400px; height:100px;" })%>
                                                        <%: Html.ValidationMessageFor(model => model.Description)%>
                                                    </div>
                                                        <div class="editor-label" style="padding-top:15px;">Phương thức bán</div>
                                                        <div class="editor-field" style="padding-top:10px;padding-left:15px;">
                                                        <%: Html.RadioButtonFor(model => model.IsAuction, true, new { onclick = "showStuff('auction');" })%>
                                                        Chỉ đấu giá
                                                        <%: Html.RadioButtonFor(model => model.IsAuction, false, new { onclick = "hideStuff('auction');" })%>
                                                        Chỉ bán
                                                        <%: Html.ValidationMessageFor(model => model.IsAuction) %>
                                                    </div>
                                                    <div>
                                                    <div class="editor-label" style="padding-top:15px; padding-bottom:15px;font-weight:bolder;">
                                                        Thông tin giá cả
                                                    </div>
                                                        <div class="editor-label">
                                                            <%: Html.LabelFor(model => model.MaxPrice) %>
                                                        </div>
                                                        <div class="editor-field">
                                                            <%: Html.TextBoxFor(model => model.MaxPrice)%>
                                                            <%: Html.ValidationMessageFor(model => model.MaxPrice)%>
                                                        </div>
                                                        <div class="editor-label">
                                                            <%: Html.LabelFor(model => model.StartDate)%>
                                                        </div>
                                                        <div class="editor-field">
                                                            <%: Html.EditorFor(model => model.StartDate)%>
                                                            <%: Html.ValidationMessageFor(model => model.StartDate)%>
                                                        </div>
                                                        <div class="editor-label">
                                                            <%: Html.LabelFor(model => model.EndDate)%>
                                                        </div>
                                                        <div class="editor-field">
                                                            <%: Html.TextBoxFor(model => model.EndDate)%>
                                                            <%: Html.ValidationMessageFor(model => model.EndDate)%>
                                                        </div>
                                                        <div id="auction" style="display: none;">
                                                            <div class="editor-label">
                                                                Giá bắt đầu
                                                            </div>
                                                            <div class="editor-field">
                                                                <input type="text" id="StartPrice" name="StartPrice" value=0 />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="editor-label" style="padding-top:15px;">
                                                        Upload hình
                                                    </div>
                                                    <div class="editor-field">
                                                        <input type="file" name="Image" />
                                                    </div>
                                                    <p>
                                                        <input type="submit" value="Thêm thông tin giao hàng ->>"  style="position:relative;top:15px;left:35%;" />
                                                    </p>
                                                
                                                <% } %>
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
