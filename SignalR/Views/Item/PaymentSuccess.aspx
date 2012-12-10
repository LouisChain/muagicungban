<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Detail.Master" Inherits="System.Web.Mvc.ViewPage<muagicungban.Entities.Item>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Vị trí đăng sản phẩm
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
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
                                            <div class="box_product_detail_padding" style="padding: 30px 30px 30px 50px; font-size: 16px;">
                                                <%: ViewData["message"] %>
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
