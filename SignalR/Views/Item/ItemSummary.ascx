<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<muagicungban.Entities.Item>" %>
<div class="product">
        
		<a href="http://<%: Request.Url.Authority %>/item/Details/<%: Model.ItemID %>">
			<img src='http://<%: Request.Url.Authority %>/item/getImage?ItemId=<%: Model.ItemID %>' class="product_image" />
		</a>
        <div class="bidder-main" id="current-user<%: Model.ItemID %>"><%: Model.CurUser %></div>
	<div class="product_title">
		<a href="#" title="<%: Model.Title %>"><%: Model.Title %></a>
	</div>
    
	<div class="product_desc">
    <% if (Model.Description != null && Model.Description.Length > 120)
       { %>
         <%: Model.Description.Substring(0, 120)%> ...
     <% }
       else
       { %>
       <%:  Model.Description %>
     <% } %>
	</div>
	<div class="product_price">   
        <% if (Model.IsAuction)
           { %>
               <span class="product_sprice" id="show-current-price<%: Model.ItemID %>"><%: Model.CurPrice.ToString("#,### VND")%></span>
               <br>
               <div id="current-price<%: Model.ItemID %>" name="current-price<%: Model.ItemID %>" style="display:none;"><%: Model.CurPrice %></div>
               <span class="product_oprice"><%: Model.MaxPrice.ToString("#,### VND")%></span> 
               <script type="text/javascript"> 
                    currentPrice[<%: Model.ItemID %>] = document.getElementById("current-price<%: Model.ItemID %>"); 
                    currentUser[<%: Model.ItemID %>] = document.getElementById("current-user<%: Model.ItemID %>");
                    showCurrentPrice[<%: Model.ItemID %>] = document.getElementById("show-current-price<%: Model.ItemID %>");                    
               </script>
        <% 
            } %>
        <% else
           { %>
            <span class="product_sprice"><%: Model.MaxPrice.ToString("#,### VNĐ")%></span>
            <div id="show-current-price<%: Model.ItemID %>" name="show-current-price<%: Model.ItemID %>" style="display:none;"><%: Model.MaxPrice %></div>
        <% } %>
           <br />&nbsp
	</div>
    <% if (Model.IsAuction)
       { %>
        <div style="float: right;position: absolute;right: 10px;bottom: 100px;font-size: 16px;font-weight: bolder;"> 
            + <input type="text" name="increase-price<%: Model.ItemID %>" id="increase-price<%: Model.ItemID %>" style="width: 100px;height:25px;font-weight: bolder; font-size:18px;" value="1000" />
        </div>
        <script>
                increasePrice[<%: Model.ItemID %>] = document.getElementById("increase-price<%: Model.ItemID %>");
        </script>
    <% } %>
	<div class="product_box">
		<div class="product_save_percent">
        <% if (Model.StartDate <= DateTime.Now && DateTime.Now < Model.EndDate)
           { %>
		        <% if (Model.IsAuction)
             { %>
                        <div class="button feature" id="bid<%: Model.ItemID %>">ĐẤU 
                    <% }
             else
             { %>
                        <div class="button feature" id="buy<%: Model.ItemID %>">MUA
                    <% } %>
	                    </div>
        <% } %>
		</div>
		<div class="product_timeout">
			<span class="title">Remaining time</span><br />
            
			<div class="key" id="timer<%: Model.ItemID %>">
                <% if (Model.StartDate > DateTime.Now)
                   { %>
                    CHƯA MỞ
                <% }
                   if (Model.EndDate <= DateTime.Now)
                   { %>
                    KẾT THÚC
                <% } %>
            </div>
            
            <script type="text/javascript">
                <% if (Model.StartDate <= DateTime.Now && Model.EndDate > DateTime.Now) { %>
                    timer[<%: Model.ItemID %>] = new countDown("<%: (Model.EndDate - DateTime.Now).TotalSeconds %>", "timer<%: Model.ItemID %>");
                <% } %>
            </script>
		</div>
	</div>
	
</div>