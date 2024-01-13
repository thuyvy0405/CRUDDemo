using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    [Table("DanhMucTinTuc")]
    public class DanhMucTinTuc
    {
        [Key, Column("IddanhMucs", Order = 0)]
        public int IddanhMuc { get; set; }
        public virtual DanhMuc DanhMuc { get; set; }

        [Key, Column("IdtinTucs", Order = 1)]
        public int IdtinTuc { get; set; }
        public virtual TinTuc TinTuc { get; set; }

    }
}