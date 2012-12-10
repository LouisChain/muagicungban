using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace muagicungban.Models
{
    public class ItemPosting
    {
        public long ItemID { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn phương thức bán hàng")]
        public bool IsAuction { get; set; }

        [DisplayName("Tên")]
        [StringLength(250)]
        [Required(ErrorMessage = "Bắt buộc nhập")]
        public string Title { get; set; }

        [DisplayName("Mô tả")]
        [DataType(DataType.MultilineText)]
        [StringLength(2000)]
        public string Description { get; set; }

        [DisplayName("Giá tối đa")]
        [Required(ErrorMessage = "Bắt buộc nhập!!!")]
        public decimal MaxPrice { get; set; }

        [DisplayName("Ngày bắt đầu")]
        [DataType(DataType.DateTime, ErrorMessage="Nhập kiểu ngày tháng")]
        [Required(ErrorMessage = "Bắt buộc nhập!!!")]
        public DateTime StartDate { get; set; }

        [DisplayName("Ngày kết thúc")]
        [DataType(DataType.DateTime, ErrorMessage="Nhập kiểu ngày tháng")]
        [Required(ErrorMessage = "Bắt buộc nhập!!!")]
        public DateTime EndDate { get; set; }

        [DisplayName("Giá khởi điểm")]
        public decimal StartPrice { get; set; }


    }
}