using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DataAccessLayer.Entities
{
    public partial class DanhMuc
    {
        [DisplayName("ID Dnah mục")]

        public int IddanhMuc { get; set; }
        [DisplayName("Tên danh mục")]

        public string TenDanhMuc { get; set; }
        [DisplayName("Trạng thái")]
        public bool TrangThai { get; set; }
        public ICollection<DanhMucTinTuc>? DanhMucTinTucs { get; set; }

    }
}
