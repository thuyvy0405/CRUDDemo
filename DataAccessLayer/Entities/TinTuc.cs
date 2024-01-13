using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DataAccessLayer.Entities
{
    public partial class TinTuc
    {
        public int IdtinTuc { get; set; }
        [DisplayName("Tiêu đề")]
        public string TieuDe { get; set; } = null!;
        [DisplayName("Đường dẫn URL")]

        public string? Url { get; set; }
        [DisplayName("Tóm tắt")]

        public string? TomTat { get; set; }
        [DisplayName("Nội dung")]

        public string? NoiDung { get; set; }
        [DisplayName("Hình ảnh")]

        public string? HinhAnh { get; set; }
        [DisplayName("Ngày tạo")]

        public DateTime? NgayTao { get; set; }
        [DisplayName("Ngày cập nhật")]

        public DateTime? NgayUpdate { get; set; }
        [DisplayName("Lượt xem")]

        public int LuotXem { get; set; }
        [DisplayName("Trạng thái")]

        public bool TrangThai { get; set; }
        [DisplayName("Danh mục")]
        public ICollection<DanhMucTinTuc>? DanhMucTinTucs { get; set; }
    }
}
