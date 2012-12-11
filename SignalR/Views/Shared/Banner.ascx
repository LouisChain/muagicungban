<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div class="logo-image">
    <a href="http://<%: Request.Url.Authority %>/">
        <img src="http://www.hotdeal.vn/skins/hotdeal/customer/images/logo-hotdeal.png" width="182" height="74" 
        border="0" alt="HotDeal.vn: Mua hàng theo nhóm, cùng mua chung để có giá ưu đãi"></a>
</div>
<%--<div class="select_city">
    <span>Chọn thành phố</span>
    <form action="/index.php?dispatch=city.change" method="post" id="header-selectcity-form"
    class="validator">
    <input id="_input" type="text" class="selectbox" autocomplete="off" readonly="" tabindex="0"><div
        id="_container" class="selectbox-wrapper" style="display: none; width: 108px;">
        <ul>
            <li id="_input_440">Hà Nội</li>
            <li id="_input_437" class="selected">Hồ Chí Minh</li>
        </ul>
    </div>
    <select name="city_id" class="cSelect" style="display: none;">
        <option value="440">Hà Nội</option>
        <option value="437" selected="selected">Hồ Chí Minh</option>
    </select>
    </form>
</div>--%>

<div class="select_city">
    <form action="http://<%: Request.Url.Authority %>/item/SearchIndex" method="post" id="header-selectcity-form" class="validator">
    <input type="text" name="key" onkeypress="return PerformClick(event, 'btnSearch');" 
            onfocus="if(this.value == 'Nhập từ khóa tìm kiếm...') this.value='';" 
            onblur="if(this.value=='') this.value='Nhập từ khóa tìm kiếm...';" 
            value="Nhập từ khóa tìm kiếm..."class="inputbox" / 
            style = "background:no-repeat;padding:10px 96px 13px 10px;border:none;width:350px; background-color:#FFF">
    <select name="catagory" style="padding:10px 10px 10px;width:150px; background-color:#FFF; left:610">
        <option>Tất cả các loại</option>
        <option>Đồng hồ, trang sức</option>
        <option>Đồ cổ</option>
        <option>Đồ chơi, sở thích</option>
        <option>Sách, văn hóa phẩm và nghệ thuật</option>
        <option>Thiết bị âm thanh</option>
        <option>Nhạc cụ</option>
        <option>Đồ điện tử</option>
        <option>Đồ trẻ em</option>
        <option>Sách</option>
        <option>Tem</option>
        <option>Công nghiệp</option>
        <option>Máy ảnh, quay phim, phụ kiện</option>
        <option>Điện thoại di động, IPAD, PDA</option>
        <option>Giày dép, quần áo, phụ kiện</option>
        <option>tiền xu và tiền giấy</option>
        <option>Bộ sưu tập</option>
        <option>Máy tính, mạng</option>
        <option>Thủ công mỹ nghệ</option>
        <option>Búp bê, gấu bông</option>
        <option>Đĩa DVD và phim</option>
        <option>Đồ chăm sóc thú cưng</option>
        <option>Đồ gốm sứ, thủy tinh</option>
        <option>Đồ thể thao</option>
        <option>Sự kiện thể thao và đồ cổ động</option>
        <option>Du lịch</option>
        <option>Trò chơi video</option>
        <option>Sự kiện giải trí</option>
	</select>
    <button onclick="submit" style="padding: 7px 10px 13px; width:100px; background-color:#FFF">Tìm kiếm</button>
    </form>
</div>




<script type="text/javascript">
    $('.cSelect').selectbox();
    $('.cSelect').change(function () {
        $("#header-selectcity-form").submit();
    });
</script>
<%--<div class="register_email">
    <form action="/ho-chi-minh/" method="post" name="subscribe_form_footer">
    <input type="hidden" name="redirect_url" value="index.php">
    <input type="hidden" name="newsletter_format" value="2">
    <input type="hidden" name="dispatch" value="newsletters.add_subscriber">
    <input type="hidden" name="mailing_lists[2]" value="1">
    <span>Đăng ký nhận email thông báo giảm giá </span>
    <br>
    <div class="input-wrapper">
        <input type="text" name="hint_subscribe_email" id="subscr_email_footer" size="20"
            value="Nhập địa chỉ email" class="inputbox cm-hint"><input type="submit" class="button"
                value="Gửi"></div>
    </form>
</div>--%>
