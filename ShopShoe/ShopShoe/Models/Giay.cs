namespace ShopShoe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Giay")]
    public partial class Giay
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Giay()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaGiay { get; set; }

        [StringLength(200)]
        public string TenGiay { get; set; }

        public int? SoLuongTon { get; set; }

        [StringLength(50)]
        public string AnhBia { get; set; }

        public string MoTa { get; set; }

        public decimal? GiaBan { get; set; }

        public int? MaThuongHieu { get; set; }

        public int? MaNSX { get; set; }

        public int? MaLoaiGiay { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        public virtual NhaSanXuat NhaSanXuat { get; set; }

        public virtual LoaiGiay LoaiGiay { get; set; }

        public virtual ThuongHieu ThuongHieu { get; set; }
    }
}
